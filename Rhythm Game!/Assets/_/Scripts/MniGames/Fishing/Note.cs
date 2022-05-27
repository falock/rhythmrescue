using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float secPerBeat;
	[Tooltip("Y position where the note is spawned")] public float startX;
	[Tooltip("Y position where the note is to be hit")] public float endX;
	[Tooltip("Y position where the note is removed")] public float removeLineX;
	[Tooltip("What beat of the song this note is assigned")] public float beat;
	public FishingConductor conductor;
	public bool canBePressed;
	private float nextX;
	public AudioSource audioSource;
	public bool hasBeenPressed;
	public bool failed;

	public void Initialize(FishingConductor conductor, float startX, float endX, float removeLineX, float posY, float beat, AudioClip audioClip)
	{
		this.conductor = conductor;
		this.startX = startX;
		this.endX = endX;
		this.beat = beat;
		this.removeLineX = removeLineX;

		// Set to initial position.
		transform.position = new Vector2(startX, posY);

		secPerBeat = conductor.secPerBeat;
		audioSource = this.gameObject.GetComponent<AudioSource>();

		audioSource.clip = audioClip;

	}

	void Update()
	{
		// We update the position of the note according to the position of the song.
		// (Think of this as "resetting" instead of "updating" the position of the note each frame according to the position of the song.)
		// See this image: http://shinerightstudio.com/posts/music-syncing-in-rhythm-games/pic3.png (Note that the direction is reversed.)
		transform.position = new Vector2(startX + (endX - startX) * (1f - (beat - conductor.songPosition / conductor.secPerBeat) / conductor.BeatsShownOnScreen), transform.position.y);
		nextX = startX + (endX - startX) * (1f - (beat - conductor.songPosition / conductor.secPerBeat) / conductor.BeatsShownOnScreen);

		// Remove itself when out of the screen (remove line).
		if (transform.position.y < removeLineX)
		{
			//Destroy(gameObject);
		}

		if (Input.anyKeyDown)
		{
			if (canBePressed && !hasBeenPressed)
			{
				// fish animates being caught
				hasBeenPressed = true;
				audioSource.Play();
				// score
			}
		}

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Activator")
		{
			canBePressed = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Activator" && gameObject.activeSelf)
		{
			canBePressed = false;
			if(!hasBeenPressed)
            {
				failed = true;
            }
		}
	}
}
