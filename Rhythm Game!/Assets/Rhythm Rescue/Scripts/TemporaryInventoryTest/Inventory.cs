using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory;
    public bool[] isFull;
    public GameObject[] slots;

    private void Awake()
    {
        if (inventory == null)
        {
            DontDestroyOnLoad(this.gameObject);
            inventory = this;
        }
        else if (inventory != this)
        {
            Destroy(this.gameObject);
        }

        slots = GameObject.FindGameObjectsWithTag("Inventory");
    }
    /*
    public bool inventoryEnabled;
    public GameObject inventory;

    private int allSlots;
    private int enabledSlots;
    private GameObject[] slot;
    public GameObject slotHolder;

    public GameObject itemPickedUp;

    private PlayerController thePlayer;
    private LevelManager theLevelManager;

    // Singleton, call from any script
    public static Inventory instance;

    // fill in inspector
    public List<Item> items;

    // Setup this very simple singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {

        thePlayer = FindObjectOfType<PlayerController>();
        theLevelManager = FindObjectOfType<LevelManager>();
        allSlots = 16;
        slot = new GameObject[allSlots];

        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;
            if (slot[i].GetComponent<Slot>().item == null)
            {
                slot[i].GetComponent<Slot>().empty = true;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            inventoryEnabled = !inventoryEnabled;
        }

        if (inventoryEnabled == true)
        {
            inventory.SetActive(true);
            Time.timeScale = 0;
            //    thePlayer.canMove = false;
        }
        else
        {
            inventory.SetActive(false);
            Time.timeScale = 1f;
            //   thePlayer.canMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            itemPickedUp = other.gameObject;
            Item item = itemPickedUp.GetComponent<Item>();
            theLevelManager.IncreaseItemCount(itemPickedUp.GetComponent<Item>().ID, itemPickedUp.GetComponent<Item>().quantity);
            AddItem(itemPickedUp, item.ID, item.type, item.description, item.icon, theLevelManager.currentCount);
        }

    }

    void AddItem(GameObject itemObject, int itemID, string itemType, string itemDescription, Sprite itemIcon, int currentCount)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if ((slot[i].GetComponent<Slot>().empty) || (itemPickedUp.GetComponent<Item>().ID == slot[i].GetComponent<Slot>().ID))
            {
                itemObject.GetComponent<Item>().pickedUp = true;
                slot[i].GetComponent<Slot>().item = itemObject;
                slot[i].GetComponent<Slot>().icon = itemIcon;
                slot[i].GetComponent<Slot>().type = itemType;
                slot[i].GetComponent<Slot>().ID = itemID;
                slot[i].GetComponent<Slot>().description = itemDescription;

                itemObject.transform.parent = slot[i].transform;
                itemObject.SetActive(false);

                slot[i].GetComponent<Slot>().UpdateSlot();
                slot[i].GetComponent<Slot>().empty = false;

                return;
            }

        }
    }
    */
}