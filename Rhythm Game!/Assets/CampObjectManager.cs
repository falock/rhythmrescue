using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampObjectManager : MonoBehaviour
{
    // this class needs to be referenced by the inventory.
    public static CampObjectManager current;
    // it needs to keep track of all the objcts in the camp scene, and eventually allow the player to change them around
    // it needs to know which objects are currently in use, as well as know what animations need to be played when interacting with them
    // e.g. - sitting animation for the fire
    // and where the npc needs to be spawned if interacting with that object

    // what's the probability that the npc DOES spawn in an object

    /*
    Spawn four NPCS. Check what objects are in the camp, and if they are free. Relay this info to the NPC spawner, which then randomly picks
    between walking or animating near an object. If walking, then choose one of the locations for that. If interacting with an object, get a
    random position from this object, as well as its corresponding animation.

    Where should this info about each object be stored?
    Either it can be stored on the object itself as a scriptable object holding the information, which is then referenced from this script. Or
    this script holds all the info of each object and check it according to their name. A dictionary? 
    */

    public GameObject[] objectParent = new GameObject[4];
    public List<CampObject> npcSpawnLocation = new List<CampObject>();
    public List<Transform> generalSpawnPoint = new List<Transform>();

    private void Awake()
    {
        current = this;
        // Sets up the 4 Object Parents and disables them if none of their children are active
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            objectParent[i] = transform.GetChild(0).GetChild(i).gameObject;
            if (IsAnyChildActive(objectParent[i]) == false)
            {
                objectParent[i].SetActive(false);
            }
        }
        npcSpawnLocation.Clear();
        // Filters through what game objects are active, then what children are active and if to save them
        // if the game object ISN'T active, it will simply add its transform as a general spawn point
        for (int i = 0; i < objectParent.Length; i++)
        {
            if (objectParent[i].activeInHierarchy)
            {
                for (int j = 0; j < objectParent[i].transform.childCount; j++)
                {
                    var child = objectParent[i].transform.GetChild(j).gameObject;

                    if (child.activeInHierarchy == true && child.GetComponent<CampObject>().npcSpawnLocation)
                    {
                        npcSpawnLocation.Add(child.GetComponent<CampObject>());
                    }
                    else
                    {
                        Debug.Log("not eligible");
                    }
                }
            }
            else
            {
                generalSpawnPoint.Add(objectParent[i].transform);
            }
        }

        // Sets up the general spawn points
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            generalSpawnPoint.Add(transform.GetChild(1).GetChild(i));
        }
    }

    private bool IsAnyChildActive(GameObject obj)
    {
        // Iterates through all direct childs of this object
        foreach (Transform child in obj.transform)
        {
            if (child.gameObject.activeSelf) return true;
        }

        return false;
    }

    public CampObject Interactable()
    {
        Debug.Log("inside COM");
        var randomNumber = Random.Range(0, npcSpawnLocation.Count);
        Debug.Log("number: " + randomNumber);
        return npcSpawnLocation[randomNumber];
    }

    public Transform JustPosition()
    {
        Debug.Log("inside COM");
        var randomNumber = Random.Range(0, generalSpawnPoint.Count);
        Debug.Log("number: " + randomNumber);
        return generalSpawnPoint[randomNumber];
    }
}