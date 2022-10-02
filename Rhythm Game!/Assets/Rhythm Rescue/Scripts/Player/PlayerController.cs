using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;
    Animator anim;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;


    public float runSpeed = 10.0f;

    // Is the player allowed to move?
    public bool noMovement = true;

    // Is the player able to move up, down left and right, or just left and right?
    public bool fullMovement = false;

    // To check what scene we're in
    private string sceneName;
    private bool inCamp;

    // if we should have vertical movement 
    private bool useVertical;
    private bool useHorizontal;
    private bool mainScreenMovement;

    private Vector3 newPosition;
    private bool canMove;
    private CharacterController characterController;

    private Vector3 lastPos;

    [Header("Dialogue")]
    public bool playerInRange;
    [NonSerialized] public _NPC currentNPC;

    public DialogueManager dialogueManager;

    private bool coIsRunning;

    // journal opening
    public bool journalIsOpen;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        body = GetComponent<Rigidbody2D>();

        // Retrieve the name of this scene.
        sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Camp")
        {
            inCamp = true;
            useVertical = true;
            useHorizontal = true;
        }
        else if (sceneName == "MainScreen")
        {
            inCamp = false;
            mainScreenMovement = true;
        }
        else
        {
            inCamp = false;
            anim.SetBool("Dance", true);
        }

        dialogueManager = DialogueManager.current;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        if (useVertical)
        {
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        if (mainScreenMovement)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                LevelSelectManager.current.CheckMovementLeft(out canMove, out newPosition);
                if (canMove)
                {
                    var rotationVector = transform.rotation.eulerAngles;
                    rotationVector.y = 180;
                    transform.rotation = Quaternion.Euler(rotationVector);
                    StartCoroutine(MoveToPosition(this.transform, new Vector2(newPosition.x, newPosition.y), 1f));
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                LevelSelectManager.current.CheckMovementRight(out canMove, out newPosition);
                if (canMove)
                {
                    var rotationVector = transform.rotation.eulerAngles;
                    rotationVector.y = 0;
                    transform.rotation = Quaternion.Euler(rotationVector);
                    StartCoroutine(MoveToPosition(this.transform, new Vector2(newPosition.x, newPosition.y), 1f));
                }
            }
        }

        // Animation
        if (transform.position != lastPos)
        {
            anim.SetBool("Walk", true);
            anim.speed = 2;
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.speed = 1;
        }

        lastPos = transform.position;

        // Dialogue

        if (playerInRange && !DialogueManager.current.dialogueIsPlaying && inCamp)
        {
            DialogueManager.current.visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.current.SelectConversation(currentNPC);
            }
        }
        else if (!playerInRange && inCamp)
        {
            DialogueManager.current.visualCue.SetActive(false);
        }
        else
        {
            DialogueManager.current.visualCue.SetActive(false);
        }

        //opening journal 

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (InventoryManager.current.canOpenJournal && !journalIsOpen)
            {
                InventoryManager.current.OpenJournal.SetActive(true);
                journalIsOpen = true;
                InventoryManager.current.GetFriends();
                InventoryManager.current.UpdateGrid();
                //InventoryManager.current.OpenJournal.GetComponent<Button>().onClick.Invoke();
            }
            else if (journalIsOpen)
            {
                InventoryManager.current.OpenJournal.SetActive(false);
                journalIsOpen = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (DialogueManager.current.dialogueIsPlaying)
        {
            return;
        }
        if (useVertical)
        {
            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
            if (horizontal != 0 && vertical != 0)
            {
                // limit movement speed diagonally, so you move at 70% speed
                horizontal *= moveLimiter;
                vertical *= moveLimiter;
            }
        }

    }

    public IEnumerator MoveToPosition(Transform transform, Vector2 position, float timeToMove)
    {
        coIsRunning = true;
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector2.Lerp(currentPos, position, t);
            yield return null;
        }
        coIsRunning = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (inCamp)
        {
            if (other.tag == "Friend")
            {
                playerInRange = true;
                currentNPC = other.gameObject.GetComponentInParent<_NPC>();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (inCamp)
        {
            if (other.tag == "Friend")
            {
                playerInRange = false;
            }
        }
    }

    public void PlayCoroutine(Vector3 position, int buttonNumber)
    {
        if (!coIsRunning)
        {
            LevelSelectManager.current.currentPlayerLevel = buttonNumber;
            StartCoroutine(MoveToPosition(this.transform, new Vector2(position.x, position.y), 1f));
        }
    }
}
