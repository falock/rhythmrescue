using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCController : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;

    public float runSpeed = 1f;

    // Is the player allowed to move?
    public bool noMovement = true;

    // Is the player able to move up, down left and right, or just left and right?
    public bool fullMovement = false;

    // To check what scene we're in
    private string sceneName;

    private Transform thisTransform;

    // The movement speed of the object
    public float moveSpeed = 0.2f;

    // A minimum and maximum time delay for taking a decision, choosing a direction to move in
    public Vector2 decisionTime = new Vector2(1, 4);
    internal float decisionTimeCount = 0;

    // The possible directions that the object can move int, right, left, up, down, and zero for staying in place. I added zero twice to give a bigger chance if it happening than other directions
    internal Vector3[] moveDirections = new Vector3[] { Vector2.right, Vector2.left, Vector2.up, Vector2.down, Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero };
    internal int currentMoveDirection;

    public bool isActive = false;
    public const string camp = "Camp";
    public bool isInteractingWithCampObject;

    public Animator anim;

    [Header("Eyes")]
    public GameObject[] eyes;
    [Header("Mouths")]
    public GameObject[] mouths;
    [SerializeField] private GameObject visualCue;
    private GameObject dialogueTriggerBox;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        body = GetComponent<Rigidbody2D>();
        dialogueTriggerBox = this.transform.Find("Dialogue").gameObject;
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        sceneName = currentScene.name;
        isActive = (sceneName == camp);
        anim.SetBool("Walk", false);
        if (!isActive) anim.SetBool("Dance", true);
        //visualCue = this.transform.Find("Visual Cue").gameObject;
        visualCue.SetActive(false);

        // Cache the transform for quicker access
        thisTransform = this.transform;

        // Set a random time delay for taking a decision ( changing direction, or standing in place for a while )
        decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

        // Choose a movement direction, or stay in place
        ChooseMoveDirection();
        dialogueTriggerBox.SetActive(false);
        if (isActive)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            dialogueTriggerBox.SetActive(true);
            //walkArea =
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isInteractingWithCampObject)
        {
            anim.SetBool("Sit", true);
        }
        if (this.transform.localRotation.x != 0 || this.transform.localRotation.z != 0)
        {
            this.gameObject.transform.localRotation = Quaternion.identity;
        }

        if (!isActive) return;

        if(player != null)
        {
            if (DialogueManager.current.dialogueIsPlaying
                && player.GetComponent<PlayerController>().currentNPC == this.gameObject.GetComponent<_NPC>())
            {
                visualCue.SetActive(true);
                anim.SetBool("Talk", true);
                return;
            }
            else
            {
                visualCue.SetActive(false);
                anim.SetBool("Talk", false);
            }
        }

        // y = -180 if left
        if (isActive && !isInteractingWithCampObject)
        {
            // Move the object in the chosen direction at the set speed
            thisTransform.position += moveDirections[currentMoveDirection] * Time.deltaTime * runSpeed;

            if (decisionTimeCount > 0)
            {
                decisionTimeCount -= Time.deltaTime;
            }
            else
            {
                // Choose a random time delay for taking a decision ( changing direction, or standing in place for a while )
                decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

                // Choose a movement direction, or stay in place
                ChooseMoveDirection();
            }
        }
    }

    void ChooseMoveDirection()
    {
        // Choose whether to move sideways or up/down
        currentMoveDirection = Mathf.FloorToInt(Random.Range(0, moveDirections.Length));

        // Make sure the characer is facing the right way
        if (currentMoveDirection == 1)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.y = 180;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        else if (currentMoveDirection == 0)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.y = 0;
            transform.rotation = Quaternion.Euler(rotationVector);
        }

        // Start or stop walking animations
        if (currentMoveDirection >= 0 && currentMoveDirection <= 3)
        {
            anim.SetBool("Walk", true);
        }
        else if (currentMoveDirection >= 4)
        {
            anim.SetBool("Walk", false);
        }
    }

    public void ChooseExpression(string mood)
    {
        switch (mood)
        {
            case "neutral":
                ChangeExpression(0);
                break;
            case "happy":
                ChangeExpression(1);
                break;
            case "sad":
                ChangeExpression(2);
                break;
            default:
                break;
        }
    }

    public void ChangeExpression(int i)
    {
        // neutral 0, happy 1, sad 2, surprised (mouth) 3
        for (int j = 0; j < eyes.Length; j++)
        {
            if (j == i)
            {
                eyes[j].SetActive(true);
                mouths[i].SetActive(true);
            }
            else
            {
                eyes[j].SetActive(false);
                mouths[j].SetActive(false);
            }
        }
    }

    public void ChooseCampActivity(CampObject obj)
    {
        if(anim == null)
        {
            anim = GetComponentInChildren<Animator>();
            anim.gameObject.SetActive(true);
        }
        isInteractingWithCampObject = true;
        anim.SetBool("Sit", true);
    }
}
