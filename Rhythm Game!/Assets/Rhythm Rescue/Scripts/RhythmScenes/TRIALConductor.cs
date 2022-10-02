using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class TRIALConductor : MonoBehaviour
{
	[Header("Note Information")]

	[SerializeField] public GameObject tapNotePrefab;
	[SerializeField] public GameObject holdNotePrefab;
	// [SerializeField] public GameObject holdNotePrefab;

	private float lane1PosX = -3;
	private float lane2PosX = -1;
	private float lane3PosX = 1;
	private float lane4PosX = 3;

	private float lane1PosXEnd = -3;
	private float lane2PosXEnd = -1;
	private float lane3PosXEnd = 1;
	private float lane4PosXEnd = 3;

	[Tooltip("What are the X positions of where the notes should spawn? How many lanes are there?")]
	[SerializeField] private float lanePosX;
	[SerializeField] public bool firstBossLevel;

	// The start positionY of notes.
	public float startLineY;
	// The finish line (the positionX where players hit) of the notes.
	public float finishLineY;
	// The positionX where the note should be destroyed.
	public float removeLineY;

	[Header("Song Information")]
	// Tempo of the song.
	public float songTempo;
	// Some audio file might contain an empty interval at the start. We will substract this empty offset to calculate the actual position of the song.
	public float songOffset;

	// The position offest of toleration. (If the players hit slightly inaccurate for the music note, we tolerate them and count it as a successful hit.)
	public float tolerationOffset;

	// The beat-locations of all music notes in the song should be entered in this array in Editor.
	// See the image: http://shinerightstudio.com/posts/music-syncing-in-rhythm-games/pic1.png
	[NonSerialized] public float[] track;
	[NonSerialized] public float[] arrayNoteLength;
	[NonSerialized] public float[] lane;

	// the value given for the lane in the text file
	private float laneOfDashPart;

	// text file to array
	private string[] notesLines;
	private string[] csvParts;
	private string[] individualValues;

	// to access length of note outside of this script
	[NonSerialized] public float floatLength;

	// How many seconds each beat last. This is calculated in start by (60 / BPM).
	[NonSerialized] public float secPerBeat;

	// Current song position. (We don't want to show this in Editor, hence the "NonSerialized")
	[NonSerialized] public float songPosition;

	//Current song position, in beats
	public float songPositionInBeats;

	// To record the time passed of the audio engine in the last frame. We use this to calculate the position of the song.
	[NonSerialized] public float dspSongTime;

	// How many beats are contained on the screen. (Imagine this as "how many beats per bar" on music sheets.)
	public float BeatsShownOnScreen = 8f;

	[SerializeField] public TextAsset beatmapTxt;
	// This plays the song.
	public AudioSource songAudioSource;

	// This plays the beat.
	public AudioSource beatAudioSource;

	// Next index for the array "track".
	private int indexOfNextNote;

	private List<GameObject> musicNoteList = new List<GameObject>();

	// Queue, keep references of the MusicNotes which currently on screen.
	[NonSerialized] public Queue<MusicNote> notesOnScreen;

	private bool songStarted = false;
	private bool songEnded = false;
	private bool alreadyActivatedResultsScreen = false;

	// public GameObject secondConductor;

	float startPos;
	float endPos;
	[NonSerialized] public int g = 0;

	void Start()
	{
		// int m_comboIndex = 0;
		var anything = new List<float>();

		// return (m_comboIndex + 1) % anything.Count;
		// Initialize some variables.
		// secondConductor.SetActive(true);
		notesOnScreen = new Queue<MusicNote>();
		indexOfNextNote = 0;
		secPerBeat = 60f / songTempo;
		// Debug.Log("index of length start" + indexOfLength);

		// split text file by each line
		notesLines = beatmapTxt.text.Replace("\r", "").Split('\n');

		// lane 1
		List<float> lane1NoteStart = new List<float>();
		List<float> lane1NoteLane = new List<float>();
		List<float> lane1NoteLength = new List<float>();
		// track1 = new float[l];
		// may need to change conditional to -1

		for (int i = 0; i < notesLines.Length; i++)
		{
			// notesLines[i].Replace("/s", "");
			// split the note lines at each comma
			// string ouputString = inputString.Trim(" ", "");)
			csvParts = notesLines[i].Split(',');
			// loops through the length of each comma separated values

			for (int j = 0; j < csvParts.Length; j++)
			{

				// splits the CSVs by a dash, to get the start and end value of each note
				individualValues = csvParts[j].Split('-');
				// turn the string note positions into float values
				// float start = float.Parse(notePositions[0]);
				startPos = float.Parse(individualValues[0]);
				floatLength = float.Parse(individualValues[1]);
				laneOfDashPart = float.Parse(individualValues[2]);
				// might not need endPos if making length from the length value?
				endPos = startPos + floatLength;
				// only adds values to the list that are relevant to the lane the instance of the script is used for

				lane1NoteStart.Add(startPos);
				lane1NoteLength.Add(floatLength);
				lane1NoteLane.Add(laneOfDashPart);
				// Debug.Log("float length " + floatLength);
			}
		}

		// turn the list into a track array so notes can be read
		track = lane1NoteStart.ToArray();
		arrayNoteLength = lane1NoteLength.ToArray();
		lane = lane1NoteLane.ToArray();
		// Debug.Log(arrayNoteLength[0] + "," + arrayNoteLength[1] + "," + arrayNoteLength[3]);
		// calculation of the note length


		// Start the song if it isn't started yet.
		if (!songStarted)
		{
			songStarted = true;
			StartSong();
			return;
		}

		// Play the beat sound.
		beatAudioSource.Play();

		if (notesOnScreen.Count > 0)
		{
			// Get the front note.
			MusicNote frontNote = notesOnScreen.Peek();

			// Distance from the note to the finish line.
			float offset = Mathf.Abs(frontNote.gameObject.transform.position.y - finishLineY);
			// Debug.Log(offset);

			// Music note hit.
			if (offset >= tolerationOffset)
			{
				// Change color to green to indicate a "HIT".
				//frontNote.ChangeColor(true);

				// statusText.text = "HIT!";

				// Remove the reference. (Now the next note moves to the front of the queue.)
				notesOnScreen.Dequeue();
			}
			// transform.position = Vector2.Lerp(0, -5,
			// (beatsShownInAdvance - (2 - songPosInBeats)) / beatsShownInAdvance);
		}
	}

	void StartSong()
	{
		// Use AudioSettings.dspTime to get the accurate time passed for the audio engine.
		dspSongTime = (float)AudioSettings.dspTime;

		// Play song.
		songAudioSource.Play();
		Debug.Log("track length" + track.Length);
		RhythmManager.instance.noteAmount = track.Length;
		RhythmManager.instance.UpdateScore();
	}

	void Update()
	{
		if (!songStarted || songEnded) return;

		// Calculate songposition. (Time passed - time passed last frame).
		songPosition = (float)(AudioSettings.dspTime - dspSongTime - songOffset);

		//determine how many beats since the song started
		songPositionInBeats = songPosition / secPerBeat;

		// Check if we need to instantiate a new note. (We obtain the current beat of the song by (songposition / secondsPerBeat).)
		// See the image for note spawning (note that the direction is reversed):
		// http://shinerightstudio.com/posts/music-syncing-in-rhythm-games/pic2.png
		float beatToShow = songPosition / secPerBeat + BeatsShownOnScreen;

		// Check if there are still notes in the track, and check if the next note is within the bounds we intend to show on screen.
		if (indexOfNextNote < track.Length && track[indexOfNextNote] < beatToShow)
		{
			// Instantiate a new music note. (Search "Object Pooling" for more information if you wish to minimize the delay when instantiating game objects.)
			// We don't care about the position and rotation because we will set them later in MusicNote.Initialize(...).
			MusicNote musicNote = ((GameObject)Instantiate(tapNotePrefab, Vector2.zero, Quaternion.identity)).GetComponent<MusicNote>();

			musicNote.length = arrayNoteLength[g];
			float startX = 0;

			Debug.Log("startX: " + startX);
			if (lane[g] == 1)
			{
				if (!firstBossLevel) startX = lane1PosX;
				else startX = lanePosX;
				musicNote.Initialize(this, startLineY, finishLineY, removeLineY, startX, lane1PosXEnd, track[indexOfNextNote]);
				Debug.Log("lane 1");
			}
			else if (lane[g] == 2)
			{
				if (!firstBossLevel) startX = lane2PosX;
				else startX = lanePosX;
				musicNote.Initialize(this, startLineY, finishLineY, removeLineY, startX, lane2PosXEnd, track[indexOfNextNote]);
				Debug.Log("lane 2");
			}
			else if (lane[g] == 3)
			{
				if (!firstBossLevel) startX = lane3PosX;
				else startX = lanePosX;
				musicNote.Initialize(this, startLineY, finishLineY, removeLineY, startX, lane3PosXEnd, track[indexOfNextNote]);
				Debug.Log("lane 3");
			}
			else if (lane[g] == 4)
			{
				if (!firstBossLevel) startX = lane4PosX;
				else startX = lanePosX;
				musicNote.Initialize(this, startLineY, finishLineY, removeLineY, startX, lane4PosXEnd, track[indexOfNextNote]);
				Debug.Log("lane 4");
			}
			else
			{
				Debug.LogError("incorrect lane value");
			}

			// The note is push into the queue for reference.
			notesOnScreen.Enqueue(musicNote);
			musicNoteList.Add(musicNote.gameObject);
			// Update the next index.
			indexOfNextNote++;
			g++;
		}


		// Loop the queue to check if any of them reaches the finish line. 
		if (notesOnScreen.Count > 0)
		{
			MusicNote currNote = notesOnScreen.Peek();

			if (currNote.transform.position.y <= finishLineY + tolerationOffset)
			{
				// Change color to red to indicate a miss.
				//currNote.ChangeColor(false);

				notesOnScreen.Dequeue();
			}

			// Note that the music note is eventually removed by itself (the Update() function in MusicNote) when it reaches the removeLine.
		}

		if(!songAudioSource.isPlaying && songStarted && !InventoryManager.current.resultsScreen.activeInHierarchy || RhythmManager.instance.teamHealth <= 0)
        {
			if (!alreadyActivatedResultsScreen)
			{
				RhythmManager.instance.songHasEnded = true;
				RhythmManager.instance.rhythmGameItems.transform.GetChild(0).gameObject.SetActive(false);
				RhythmManager.instance.ShowResults();
				InventoryManager.current.rhythmUI.SetActive(false);
				InventoryManager.current.resultsScreen.SetActive(true);
				songEnded = true;
				StartCoroutine(FadeMusic());
				alreadyActivatedResultsScreen = true;
                for (int i = 0; i < musicNoteList.Count - 1; i++)
                {
					if(musicNoteList[i].activeInHierarchy)
                    {
						Destroy(musicNoteList[i]);
                    }
                }
				Debug.Log("destroyed!");
			}
		}

	}

	IEnumerator FadeMusic()
    {
		var fadeTime = 2;
		float t = fadeTime;
		while (t > 0)
		{
			yield return null;
			t -= Time.deltaTime;
			songAudioSource.volume = t / fadeTime;
		}
		yield break;
	}
}