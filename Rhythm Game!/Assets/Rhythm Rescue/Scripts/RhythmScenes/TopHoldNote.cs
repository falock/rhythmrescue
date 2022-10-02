using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopHoldNote : MonoBehaviour
{
	public bool inRange;
	public GameObject musicNote;
	public MusicNote musicNoteScript;
	private RhythmManager rhythmManager;

	void Start()
	{
		musicNote = transform.parent.gameObject;
		musicNoteScript = musicNote.GetComponent<MusicNote>();
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		// if this note has entered the hit button, it is in range to be hit
		if (other.tag == "Activator")
		{
			inRange = true;
			musicNoteScript.inRange = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		// if this note has passed the hit button and is still active, the note was missed
		if (other.tag == "Activator" && gameObject.activeSelf)
		{
			inRange = false;
			musicNoteScript.inRange = false;
			rhythmManager.NoteMissed();
		}
	}

	void Update()
	{
		transform.position = new Vector2(musicNote.transform.position.x, transform.position.y);
		musicNoteScript.positionY = this.transform.position.y;
	}
}
