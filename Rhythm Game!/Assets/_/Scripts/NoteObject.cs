using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;

    [SerializeField] Transform target;

    public float beatTempo;
    public bool hasStarted;
    public float negativePosition;

    private BeatScroller bs;

    // Start is called before the first frame update
    void Start()
    {

    }

    /*  private void OnEnable()
    {
        bs =  GameObject.Find("BeatScroller").GetComponent<BeatScroller>();
        bs.Register(this);
    }
    */

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);

                // GameManager.instance.NoteHit();
                Debug.Log(Mathf.Abs(transform.position.y));

                // distance from centre of button transform to get a noraml hit
                if (Mathf.Abs(transform.position.y) > 3.2)
                {
                    RhythmManager.instance.NormalHit();
                    Debug.Log("Normal");
                }
                // distance for good hit 
                else if (Mathf.Abs(transform.position.y) > 2.7)
                {
                    RhythmManager.instance.GoodHit();
                    Debug.Log("Good");
                }
                // distance for perfect hit
                else
                {
                    RhythmManager.instance.PerfectHit();
                    Debug.Log("Perfect!");
                }
            }
        }



    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && gameObject.activeSelf)
        {
            canBePressed = false;
            RhythmManager.instance.NoteMissed();
        }

    }

}