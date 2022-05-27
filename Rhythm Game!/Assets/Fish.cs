using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
	public float secPerBeat;
	[Tooltip("Y position where the note is spawned")] public float startX;
	[Tooltip("Y position where the note is to be hit")] public float endX;
	[Tooltip("Y position where the note is removed")] public float removeLineX;
	[Tooltip("What beat of the song this note is assigned")] public float beat;
	public FishingConductor conductor;
	public bool canBePressed;
	private float nextX;
	public AudioClip[] sounds;
	private List<GameObject> musicNoteList = new List<GameObject>();
	private bool pause;

	public void Initialize(FishingConductor conductor, float startX, float endX, float removeLineX, float posY, float beat, List<GameObject> notes)
	{
		this.conductor = conductor;
		this.startX = startX;
		this.endX = endX;
		this.beat = beat;
		this.removeLineX = -removeLineX;
		this.musicNoteList = notes;

		// Set to initial position.
		transform.position = new Vector2(startX, posY);

		secPerBeat = conductor.secPerBeat;
	}

	void Update()
	{
		// We update the position of the note according to the position of the song.
		// (Think of this as "resetting" instead of "updating" the position of the note each frame according to the position of the song.)
		// See this image: http://shinerightstudio.com/posts/music-syncing-in-rhythm-games/pic3.png (Note that the direction is reversed.)

		if (transform.position.x == endX && !CheckNotes())
		{
			pause = true;
		}
        else
        {
			pause = false;
        }

		if(CheckIfSucceeded())
        {
			FishingManager.current.CatchFish(this);
			Destroy(gameObject);
        }

		if (pause) return;
		transform.position = new Vector2(startX + (endX - startX) * (1f - (beat - conductor.songPosition / conductor.secPerBeat) / conductor.BeatsShownOnScreen), transform.position.y);
		nextX = startX + (endX - startX) * (1f - (beat - conductor.songPosition / conductor.secPerBeat) / conductor.BeatsShownOnScreen);

		// Remove itself when out of the screen (remove line).
		if (transform.position.x < removeLineX)
		{
			Debug.Log(true + "transform: " + transform.position.x + ". removeLineX: " + removeLineX);
			Destroy(gameObject);
		}

	}

	private bool CheckNotes()
    {
		for (int i = 0; i < musicNoteList.Count; ++i)
		{
			if (musicNoteList[i].GetComponent<Note>().failed == false)
			{
				// returning false means the fish stays paused
				return false;
			}
		}

		// fish continues moving
		return true;
	}

	private bool CheckIfSucceeded()
    {
		for (int i = 0; i < musicNoteList.Count; ++i)
		{
			if (musicNoteList[i].GetComponent<Note>().hasBeenPressed == false)
			{
				// returning false means the fish stays paused
				return false;
			}
		}

		// fish continues moving
		return true;
	}
}


