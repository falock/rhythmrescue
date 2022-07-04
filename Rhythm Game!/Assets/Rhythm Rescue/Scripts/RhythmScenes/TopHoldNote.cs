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
		if (other.tag == "Activator")
		{
			inRange = true;
			musicNoteScript.inRange = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Activator" && gameObject.activeSelf)
		{
			inRange = false;
			musicNoteScript.inRange = false;
			rhythmManager.NoteMissed();
		}
	}

	void Update()
	{
		musicNoteScript.positionY = this.transform.position.y;
		// Debug.LogError($"Length = {length}, startY {startY}");
	}
}
