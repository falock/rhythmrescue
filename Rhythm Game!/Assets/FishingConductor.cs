using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishingConductor : MonoBehaviour
{
	[Header("Note Information")]

	[SerializeField] public GameObject notePrefab;
	[SerializeField] public GameObject fishPrefab;
	// [SerializeField] public GameObject holdNotePrefab;

	// The position Y of music notes.
	[NonSerialized] public float lanePosY = -3.82f;

	// The start positionX of notes.
	public float startLineX = 13.43f;
	// The finish line (the positionX where players hit) of the notes.
	public float finishLineX = 0.61f;
	// The positionX where the note should be destroyed.
	public float removeLineX = -13.71f;

	[Header("Song Information")]
	// Tempo of the song.
	public float songTempo;
	// Some audio file might contain an empty interval at the start. We will substract this empty offset to calculate the actual position of the song.
	public float songOffset;

	// The position offest of toleration. (If the players hit slightly inaccurate for the music note, we tolerate them and count it as a successful hit.)
	public float tolerationOffset;

	// The beat-locations of all music notes in the song should be entered in this array in Editor.
	[NonSerialized] public float[] track;
	[NonSerialized] public float[] arrayNoteLength;
	[NonSerialized] public float[] lane;
	public float[] currentNotesArray;

	// the value given for the lane in the text file
	private float laneOfDashPart;

	// text file to array
	private string[] notesLines;
	private string[] individualValues;
	public float countdown;

	List<float> currentNotes = new List<float>();

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
	public int indexOfNextNote;

	private List<GameObject> musicNoteList = new List<GameObject>();

	// Queue, keep references of the MusicNotes which currently on screen.
	[NonSerialized] public Queue<Note> notesOnScreen;

	private bool songStarted = false;
	private bool songEnded = false;
	private bool alreadyActivatedResultsScreen = false;

	public AudioClip[] sounds;

	// public GameObject secondConductor;

	[NonSerialized] public int g = 0;

	void Start()
	{
		//int m_comboIndex = 0;
		//var anything = new List<float>();

		//return (m_comboIndex + 1) % anything.Count;

		// Initialize some variables.
		notesOnScreen = new Queue<Note>();
		indexOfNextNote = 0;
		secPerBeat = 60f / songTempo;

		// split text file by each line
		notesLines = beatmapTxt.text.Replace("\r", "").Split('\n');

		//List<float> noteStart = new List<float>();
		// track1 = new float[l];
		// may need to change conditional to -1


		// turn the list into a track array so notes can be read
		//track = noteStart.ToArray();

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
			Note frontNote = notesOnScreen.Peek();

			// Distance from the note to the finish line.
			float offset = Mathf.Abs(frontNote.gameObject.transform.position.x - finishLineX);
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
		var number = Random.Range(0, 20);
		countdown = songPositionInBeats + number;

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

		if(songPositionInBeats >= countdown)
        {
			GetNextNotes();
        }

		Debug.Log("beat: " + beatToShow);

		if (track.Length > 0 && track[indexOfNextNote] < beatToShow)
		{
			// Check if there are still notes in the track, and check if the next note is within the bounds we intend to show on screen.
			//if (indexOfNextNote < track.Length && track[indexOfNextNote] < beatToShow)
			for (int i = 0; i < track.Length; i++)
			{
				// Instantiate a new music note. (Search "Object Pooling" for more information if you wish to minimize the delay when instantiating game objects.)
				// We don't care about the position and rotation because we will set them later in MusicNote.Initialize(...).
				var note = Instantiate(notePrefab, Vector2.zero, Quaternion.identity).GetComponent<Note>();

				if (note != null)
				{
					note.Initialize(this, startLineX, finishLineX, removeLineX, lanePosY, track[i]);
					notesOnScreen.Enqueue(note);
					musicNoteList.Add(note.gameObject);
				}

				StartCoroutine(WaitForSound());

				// The note is push into the queue for reference
				// Update the next index.
				indexOfNextNote++;
				g++;
			}

			var fish = Instantiate(fishPrefab, Vector2.zero, Quaternion.identity).GetComponent<Fish>();
			fish.Initialize(this, startLineX, finishLineX, removeLineX, lanePosY, track[track.Length - 1] + 2);
		}
			
		// Loop the queue to check if any of them reaches the finish line. 
		if (notesOnScreen.Count > 0)
		{
			Note currNote = notesOnScreen.Peek();

			if (currNote.transform.position.y <= finishLineX + tolerationOffset)
			{
				// Change color to red to indicate a miss.
				//currNote.ChangeColor(false);

				notesOnScreen.Dequeue();
			}

			// Note that the music note is eventually removed by itself (the Update() function in MusicNote) when it reaches the removeLine.
		}
			
	}

	private void GetNextNotes()
	{
		currentNotes.Clear();
		indexOfNextNote = 0;
		var randomNumber = Random.Range(0, notesLines.Length);
		// split the note lines at each comma
		// string ouputString = inputString.Trim(" ", "");)
		individualValues = notesLines[randomNumber].Split('-');
        for (int i = 0; i < individualValues.Length; i++)
        {
			currentNotes.Add(float.Parse(individualValues[i]) + songPositionInBeats + 8);
		}

		currentNotesArray = currentNotes.ToArray();
		// loops through the length of each comma separated values
		var newCountdown = Random.Range(0, 20);
		countdown = songPositionInBeats + newCountdown;

		track = currentNotesArray;

		//SpawnNotes();
	}

	private void SpawnNotes()
    {
		if (track.Length > 0)
		{
            // Check if there are still notes in the track, and check if the next note is within the bounds we intend to show on screen.
            //if (indexOfNextNote < track.Length && track[indexOfNextNote] < beatToShow)
            for (int i = 0; i < track.Length; i++)
            {
				// Instantiate a new music note. (Search "Object Pooling" for more information if you wish to minimize the delay when instantiating game objects.)
				// We don't care about the position and rotation because we will set them later in MusicNote.Initialize(...).
				var note = Instantiate(notePrefab, Vector2.zero, Quaternion.identity).GetComponent<Note>();

				if (note != null)
				{
					note.Initialize(this, startLineX, finishLineX, removeLineX, lanePosY, track[i]);
					notesOnScreen.Enqueue(note);
					musicNoteList.Add(note.gameObject);
				}
				
				StartCoroutine(WaitForSound());
					
				// The note is push into the queue for reference
				// Update the next index.
				indexOfNextNote++;
				g++;
			}

			var fish = Instantiate(fishPrefab, Vector2.zero, Quaternion.identity).GetComponent<Fish>();
			fish.Initialize(this, startLineX, finishLineX, removeLineX, lanePosY, track[track.Length - 1] +2);
			/*
			{
				// Instantiate a new music note. (Search "Object Pooling" for more information if you wish to minimize the delay when instantiating game objects.)
				// We don't care about the position and rotation because we will set them later in MusicNote.Initialize(...).
				var note = Instantiate(notePrefab, Vector2.zero, Quaternion.identity).GetComponent<Note>();

				if (note != null)
				{
					note.Initialize(this, startLineX, finishLineX, removeLineX, lanePosY, track[indexOfNextNote]);
					notesOnScreen.Enqueue(note);
					musicNoteList.Add(note.gameObject);
				}


				// The note is push into the queue for reference
				// Update the next index.
				indexOfNextNote++;
				g++;
			}
			*/

		}
	}

	private IEnumerator WaitForSound()
    {
        for (int i = 0; i < track.Length; i++)
        {
			var soundNumber = Random.Range(0, sounds.Length);
			beatAudioSource.clip = sounds[soundNumber];
			beatAudioSource.Play();//play your 1. Sound
			yield return new WaitForSeconds(beatAudioSource.clip.length);
		}
	}
}
