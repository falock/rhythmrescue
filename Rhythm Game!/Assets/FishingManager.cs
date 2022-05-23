using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public static FishingManager current;
    public GameObject fishingRod;
    public GameObject playerSpawn;
    public GameObject playerReference;

    private void Awake()
    {
        //Check if instance already exists
        if (current == null)
        {
            //if not, set instance to this
            current = this;
        }
        //If instance already exists and it's not this:
        else if (current != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
//        playerReference = FindObjectOfType<PlayerController>().gameObject;
        // this script will handle any interaction by the player, also the player movemnt script will be disabled
        // tutorial - either choose to do practice run OR a visual tutorial box comes up to teach you
        // when you click OK, there is a countdown on beat with the song that starts playing
        // fish are spawned and move across the screen like notes. Each fish makes a sound as they pass through a boxcollider
        // the player then has to hit the button in the same pattern, which also produces a sound
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound()
    {

    }
}
