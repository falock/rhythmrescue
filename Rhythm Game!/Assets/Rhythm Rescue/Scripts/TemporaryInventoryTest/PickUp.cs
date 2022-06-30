using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PickUp : MonoBehaviour , IPointerClickHandler
{
    private Inventory inventory;
    public GameObject itemButton;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        itemButton.GetComponent<Image>().sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.slots[i].transform, false);
                break;
            }
        }
    }

    // when press recruit button, brings up screen asking, 'add to party?'
    public void AddToParty()
    {
        // bring up party screen and asks who to replace. 
    }
    public void AddToFriendsList()
    {
        // if answer is no, add to friends list here by serialising 
    }
}
