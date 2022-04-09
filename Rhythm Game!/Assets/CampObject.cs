using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampObject : MonoBehaviour
{
    // Need to know:
    // IF THE NPC CAN SPAWN AT IT. Tent cannot, but the player can still interact with it. Fire, yes
    public enum ObjectName { tent, fire, }
    public ObjectName objectName;
    [Tooltip("Can an NPC be spawned at this object?")]
    public bool npcSpawnLocation;
    [Tooltip("Can this object be interacted with?")]
    public bool interactable;
    public enum AnimationType { none, sit,}
    public AnimationType animationType;
    public List<Transform> childPos = new List<Transform>();

    private void Awake()
    {
        /*
        if (objectName == ObjectName.fire)
        {
            childPos.Add(gameObject.transform.GetChild(0));
            childPos.Add(gameObject.transform.GetChild(1));
        }
        */
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && interactable)
        {
            if (this.objectName == ObjectName.tent)
            {
                InventoryManager.current.DoYouWantToSave();
            }
        }
    }
}