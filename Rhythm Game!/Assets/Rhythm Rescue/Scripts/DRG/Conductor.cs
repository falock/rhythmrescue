using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class Conductor : MonoBehaviour
{
	[SerializeField] public GameObject tapNotePrefab;
	[SerializeField] public GameObject holdNotePrefab;

	// Tempo of the song.
	public float songTempo;

	// The positionY of music notes.
	public float posX;

	// The start positionY of notes.
	public float startLineY;

	// The finish line (the positionX where players hit) of the notes.
	public float finishLineY;

	// The positionX where the note should be destroyed.
	public float removeLineY;

	// Some audio file might contain an empty interval at the start. We will substract this empty offset to calculate the actual position of the song.
	public float songOffset;

	// The position offest of toleration. (If the players hit slightly inaccurate for the music note, we tolerate them and count it as a successful hit.)
	public float tolerationOffset;

	// The beat-locations of all music notes in the song should be entered in this array in Editor.
	// See the image: http://shinerightstudio.com/posts/music-syncing-in-rhythm-games/pic1.png
	[NonSerialized] public float[] track;
	[NonSerialized] public float[] arrayNoteLength;
	// the lane of the conductor, as inputted in the unity inspector, where the notes will fall
	public float laneOfThisConductor = 0;
	// the value given for the lane in the text file
	private float laneOfDashPart;

	// text file to array
	[SerializeField] public TextAsset notesFile;
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

	// This plays the song.
	public AudioSource songAudioSource;

	// This plays the beat.
	public AudioSource beatAudioSource;

	// Next index for the array "track".
	private int indexOfNextNote;
	
	// Queue, keep references of the MusicNodes which currently on screen.
	[NonSerialized] public Queue<MusicNote> notesOnScreen;

	private bool songStarted = false;

	public GameObject secondConductor;

	float startPos;
	public float endPos;
	[NonSerialized] public int g = 0;

	// Note object. Currently not using
	public class NoteInfo
	{
		public float start;
		public float end;
		public float length;

		public NoteInfo(float start, float end)
		{
			this.start = start;
			this.end = end;
			this.length = end - start;
		}
	}

	void Start()
	{

		// Initialize some variables.
		secondConductor.SetActive(true);
		notesOnScreen = new Queue<MusicNote>();
		indexOfNextNote = 0;
		secPerBeat = 60f / songTempo;
		// Debug.Log("index of length start" + indexOfLength);

		// split text file by each line
		notesLines = notesFile.text.Replace("\r", "").Split('\n');

		List<float> noteStart = new List<float>();
		List<float> noteEnd = new List<float>();
		List<float> noteLength = new List<float>();

		// track1 = new float[l];
		// may need to change conditional to -1


		for (int i = 0; i < notesLines.Length; i++)
		{
			// notesLines[i].Replace("/s", "");
			// split the note lines at each comma
			//string ouputString = inputString.Replace(" ", "");)
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
				endPos = startPos + floatLength;
			    if (j > 0)
                {
					Debug.Log("j is greater than 0|");
                }
				// only adds values to the list that are relevant to the lane the instance of the script is used for
				if (laneOfDashPart == laneOfThisConductor)
				{
					noteStart.Add(startPos);
					noteEnd.Add(endPos);
					noteLength.Add(floatLength);
					// Debug.Log("float length " + floatLength);
				}
				// NoteInfo noteInfo = new NoteInfo(start, end);
			}
		}

		// turn the list into a track array so notes can be read
		track = noteStart.ToArray();
		arrayNoteLength = noteLength.ToArray();
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
	}

	void Update()
	{
		// Check key press.
		if (Input.GetKeyDown(KeyCode.Space))
		{
			// PlayerInputted();
		}

		if (!songStarted) return;

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
			{
				// Instantiate a new music note. (Search "Object Pooling" for more information if you wish to minimize the delay when instantiating game objects.)
				// We don't care about the position and rotation because we will set them later in MusicNote.Initialize(...).
				MusicNote musicNote = ((GameObject)Instantiate(tapNotePrefab, Vector2.zero, Quaternion.identity)).GetComponent<MusicNote>();
				musicNote.spriteRenderer = tapNotePrefab.GetComponent<SpriteRenderer>();
				musicNote.spriteRenderer.drawMode = SpriteDrawMode.Sliced;
				musicNote.spriteRenderer.size = new Vector2(1, arrayNoteLength[g]);
				Debug.Log(arrayNoteLength[g]);
				//musicNote.Initialize(this, startLineY, finishLineY, removeLineY, posX, track[indexOfNextNote]);
				// The note is push into the queue for reference.
				notesOnScreen.Enqueue(musicNote);
				Debug.Log("array note length " + indexOfNextNote + arrayNoteLength[g]);
;				// Update the next index.
				indexOfNextNote++;
				g++;
			}
			/* else if (arrayNoteLength[g] > 1)
            {
				//long note
				MusicNote musicNote = ((GameObject)Instantiate(holdNotePrefab, Vector2.zero, Quaternion.identity)).GetComponent<MusicNote>();
				// musicNote.transform.localScale = new Vector3(1, arrayNoteLength[g], 0);
			    musicNote.spriteRenderer.drawMode = SpriteDrawMode.Sliced;
				musicNote.spriteRenderer.size = new Vector2(1, arrayNoteLength[g]);
				musicNote.Initialize(this, startLineY, finishLineY, removeLineY, posX, track[indexOfNextNote]);

			    // The note is push into the queue for reference.
			    notesOnScreen.Enqueue(musicNote);

      			// Update the next index.
		    	indexOfNextNote++;
			    g++;
			    // Debug.Log("index of length update" + indexOfLength);
				
			}
			*/
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
		}
		// Note that the music note is eventually removed by itself (the Update() function in MusicNote) when it reaches the removeLine.
	}

	}