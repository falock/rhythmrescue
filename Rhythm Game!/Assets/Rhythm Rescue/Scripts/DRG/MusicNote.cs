using System;
using UnityEngine;

public class MusicNote : MonoBehaviour
{
	[Header("Float Variables")]
	// We keep the start and end positionX to perform interpolation.
	[Tooltip("Y position where the note is spawned")] public float startY;
	[Tooltip("Y position where the note is to be hit")] public float endY;
	[Tooltip("Y position where the note is removed")] public float removeLineY;
	[Tooltip("What beat of the song this note is assigned")] public float beat;
	[SerializeField] public float length;
	private float startX;
	private float endX;
	//public Color hitColor;
	//public Color missColor;

	[Header("Assigned References")]
	public SpriteRenderer spriteRenderer;
	public BoxCollider2D boxCollider2D;
	// Keep a reference of the conductor.
	public TRIALConductor conductor;
	// public float length = 1;
	// public int indexOfNextNote;

	[Header("Note Debugging Info")]
	// Checking if the note is within range to be HIT.
	[SerializeField] private bool canBePressed = false;
	[SerializeField] private bool canHoldNoteBePressed = false;
	[SerializeField] private bool hasBeenHit = false;
	[SerializeField] private bool isHeld = false;
	public bool inRange = false;
	private bool cannotHoldNoteGetUp = false;

	private string buttonName;
	private float nextY;
	private float nextX;
	[SerializeField] public GameObject holdTop;
	[SerializeField] public GameObject holdLength;
	private bool holdNoteSpawned = false;
	private bool hasBeenHeld = false;

	public float secPerBeat;

	TopHoldNote topHoldNoteScript;

	public float positionY;

	public Animator anim;

	private ParticleSystem holdingParticleSystem;
	public void Initialize(TRIALConductor conductor, float startY, float endY, float removeLineY, float startX, float endX, float beat)
	{
		this.conductor = conductor;
		this.startY = startY;
		this.endY = endY;
		this.startX = startX;
		this.endX = endX;
		this.beat = beat;
		this.removeLineY = removeLineY;

		// Set to initial position.
		transform.position = new Vector2(startX, startY);

		if (endX == -3)
		{
			buttonName = "Button 1";
		}
		else if (endX == -1)
		{
			buttonName = "Button 2";
		}
		else if (endX == 1)
		{
			buttonName = "Button 3";
		}
		else if (endX == 3)
		{
			buttonName = "Button 4";
		}
		else
		{
			Debug.LogError("invalid note x position");
		}

		secPerBeat = conductor.secPerBeat;
	}

    private void Start()
    {
		holdingParticleSystem = RhythmManager.instance.rhythmGameItems.transform.GetChild(0).transform.Find(buttonName).GetComponent<ParticleSystem>();
	}
    void Update()
	{
		// We update the position of the note according to the position of the song.
		// (Think of this as "resetting" instead of "updating" the position of the note each frame according to the position of the song.)
		// See this image: http://shinerightstudio.com/posts/music-syncing-in-rhythm-games/pic3.png (Note that the direction is reversed.)
		if(startX != endX)
        {
			transform.position = new Vector2(startX + (endX - startX) *
				(1f - (beat - conductor.songPosition / conductor.secPerBeat) / conductor.BeatsShownOnScreen),
				startY + (endY - startY) * (1f - (beat - conductor.songPosition / conductor.secPerBeat) / conductor.BeatsShownOnScreen));
		}
        else
        {
			transform.position = new Vector2(transform.position.x, startY + (endY - startY) *
				(1f - (beat - conductor.songPosition / conductor.secPerBeat) / conductor.BeatsShownOnScreen));
		}
		nextY = startY + (endY - startY) * (1f - ((beat + length) - conductor.songPosition / conductor.secPerBeat) / conductor.BeatsShownOnScreen);
		if(startX != endX)
        {
			nextX = startX + (endX - startX) * (1f - ((beat + length) - conductor.songPosition / conductor.secPerBeat) / conductor.BeatsShownOnScreen);
		}
		Debug.DrawLine(transform.position, new Vector3(transform.position.x, nextY, transform.position.z), Color.magenta);

		// Remove itself when out of the screen (remove line).
		if (transform.position.y < removeLineY)
		{
			// gameObject.SetActive(false);
		}

		// determining how the note should function based on length
		if (length == 1)
		{
			CheckTapNote();
		}
		else if (length > 1)
		{
			//CheckHoldNote();
			// Debug.Log("hold note!");
			// if a new art needs to be spawned
			if (!holdNoteSpawned)
			{
				// instantiate top of hold note
				Vector3 newPosition = new Vector3(this.transform.position.x, nextY, this.transform.position.z);
				GameObject topHoldNote = Instantiate(holdTop, newPosition, Quaternion.identity, this.transform);

				// set length of box collider
				//boxCollider2D.size = new Vector2(boxCollider2D.size.x, nextY - transform.position.y);
				//boxCollider2D.offset = new Vector2(0, boxCollider2D.size[1] / 2);

				float distance = Vector2.Distance(this.transform.position, newPosition);
				Debug.Log(distance);

				// instantiate length of hold note
				GameObject holdArt = Instantiate(holdLength, this.transform.position, Quaternion.identity, this.transform);
				holdArt.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Sliced;
				//holdArt.GetComponent<SpriteRenderer>().size = new Vector2(0.5f, 4.3f * length);
				holdArt.GetComponent<SpriteRenderer>().size = new Vector2(0.5f, nextY - transform.position.y);

				// make sure this isn't repeated
				holdNoteSpawned = true;

				topHoldNoteScript = holdTop.GetComponent<TopHoldNote>();
			}

			// checking it has been pressed
			if (Input.GetButtonDown(buttonName))
			{
				if (canBePressed)
				{
					hasBeenHit = true;
					//CheckTapNote();
					spriteRenderer.color = Color.magenta;
				}
				else if (!canBePressed && !hasBeenHit)
				{
					hasBeenHit = false;
				}
			}

			// holding the note down
			if (hasBeenHit && !inRange && !cannotHoldNoteGetUp)
			{
				// check if the player is holding
				if (Input.GetButton(buttonName))
				{
					cannotHoldNoteGetUp = false;
				
					var particleSystemMain = holdingParticleSystem.main;
					particleSystemMain.loop = true;
					holdingParticleSystem.Play();
					//Debug.Log("has been his and is being held!");
				}
				else
				{
					//note failed
					cannotHoldNoteGetUp = true;
					var particleSystemMain = holdingParticleSystem.main;
					particleSystemMain.loop = false;
					holdingParticleSystem.Stop();
					this.gameObject.SetActive(false);
					//Debug.Log("cannotHoldNoteGetUp is true! == has been hit but is no longer held!");
				}
			}

			if (hasBeenHit && !cannotHoldNoteGetUp && inRange)
			{
				if (Input.GetButtonUp(buttonName))
				{
					var particleSystemMain = holdingParticleSystem.main;
					particleSystemMain.loop = false;
					holdingParticleSystem.Stop();
					//GameManager.instance.NoteHit();
					//Debug.Log(Mathf.Abs(transform.position.y));

					// distance from centre of button transform to get a noraml hit
					if (Mathf.Abs(positionY) > 4.1)
					{
						RhythmManager.instance.NoteMissed();
						Debug.Log("bad long note");
					}
					
					else if (Mathf.Abs(positionY) > 3.9)
					{
						RhythmManager.instance.NormalHit();
						Debug.Log("Okay long note");
					}
					
					// distance for good hit 
					else if (Mathf.Abs(positionY) > 3.7)
					{
						RhythmManager.instance.GoodHit();
						Debug.Log("Good long note");
					}
					// distance for perfect hit
					else // less than 4  
					{
						RhythmManager.instance.PerfectHit();
						Debug.Log("Perfect long note!");
					}

					this.gameObject.SetActive(false);
				}
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
			if (length == 1 || length > 1 && !hasBeenHit)
			{
				RhythmManager.instance.NoteMissed();
				if (length > 1 && !hasBeenHit)
                {
					this.gameObject.SetActive(false);
				}
			}
			else if (length > 1 && hasBeenHit)
            {
				RhythmManager.instance.NoteHit();
            }
		}
	}

	void CheckTapNote()
	{
		//(indexOfNextNote < track.Length && track[indexOfNextNote] < beatToShow)

		//Debug.Log("tap note!");
		if (Input.GetButton(buttonName))
		{
			if (canBePressed)
			{
				var particleSystemMain = holdingParticleSystem.main;
				particleSystemMain.loop = false;
				holdingParticleSystem.Stop();
				holdingParticleSystem.Play();
				
				//GameManager.instance.NoteHit();

				// distance from centre of button transform to get a noraml hit
				if (Mathf.Abs(transform.position.y) > 4.1)
				{
					RhythmManager.instance.NoteMissed();
					//Debug.Log("bad hit");
				}
				
				else if (Mathf.Abs(transform.position.y) > 3.9)
				{
					RhythmManager.instance.NormalHit();
					//Debug.Log("Okay");
				}
				
				// distance for good hit 
				else if (Mathf.Abs(transform.position.y) > 3.7)
				{
					RhythmManager.instance.GoodHit();
					//Debug.Log("Good!");
				}
				// distance for perfect hit
				else // less than 4  
				{
					RhythmManager.instance.PerfectHit();
					//Debug.Log("Perfect!");
				}
				this.gameObject.SetActive(false);
			}
		}
	}

	void CheckHoldNote()
	{
		/*
		// Debug.Log("hold note!");
		// if a new art needs to be spawned
		if (!holdNoteSpawned)
		{
			// instantiate top of hold note
			Vector3 newPosition = new Vector3(this.transform.position.x, nextY, this.transform.position.z);
			GameObject topHoldNote = Instantiate(holdTop, newPosition, Quaternion.identity, this.transform);

			// set length of box collider
			//boxCollider2D.size = new Vector2(boxCollider2D.size.x, nextY - transform.position.y);
			//boxCollider2D.offset = new Vector2(0, boxCollider2D.size[1] / 2);

			float distance = Vector2.Distance(this.transform.position, newPosition);
			Debug.Log(distance);

			// instantiate length of hold note
			GameObject holdArt = Instantiate(holdLength, this.transform.position, Quaternion.identity, this.transform);
			holdArt.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Sliced;
			//holdArt.GetComponent<SpriteRenderer>().size = new Vector2(0.5f, 4.3f * length);
			holdArt.GetComponent<SpriteRenderer>().size = new Vector2(0.5f, nextY - transform.position.y);

			// make sure this isn't repeated
			holdNoteSpawned = true;

			topHoldNoteScript = holdTop.GetComponent<TopHoldNote>();
		}

		inRange = topHoldNoteScript.inRange;

		// if the note is held down or not, for hold notes
		if (Input.GetButton(buttonName))
		{
			isHeld = true;
			Debug.Log("is held!");
		}
		else
		{
			isHeld = false;
			Debug.Log("is not held!");
		}

		// checking it has been pressed
		if (Input.GetButtonDown(buttonName))
		{
			if (canBePressed)
			{
				hasBeenHit = true;
				Debug.Log("has been hit!");
				this.spriteRenderer.color = Color.magenta;
			}
			else
            {
				hasBeenHit = false;
            }
		}

		// holding the note down
		if (hasBeenHit && !inRange)
		{
			// check if the player is holding
			if (!isHeld)
			{
				//note failed
				cannotHoldNoteGetUp = true;
				Debug.Log("cannotHoldNoteGetUp is true! == has been hit but is no longer held!");
				holdLength.GetComponent<SpriteRenderer>().color = Color.blue;
			}
			else
            {
				cannotHoldNoteGetUp = false;
				Debug.Log("has been his and is being held!");
				holdLength.GetComponent<SpriteRenderer>().color = Color.magenta;
				
            }				
		}

		if (hasBeenHit && !cannotHoldNoteGetUp && inRange)
		{
			if (Input.GetButtonUp(buttonName))
			{
				gameObject.SetActive(false);
			}
		}
		*/
	}
}

