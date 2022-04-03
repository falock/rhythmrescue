using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendList : MonoBehaviour
{
    //[SerializeField] public List<_NPC>  npcs;
   // public List<int> npcIDs = new List<int>();
    [SerializeField] public Transform friendParent;
    [SerializeField] public List<FriendSlot> friendSlots = new List<FriendSlot>();
    public InventoryManager inventoryManager;
    public GameObject friendSlot;
    public FriendSlot friendSlotRef;
    public MoveGrid moveGrid;

    private void Start()
    {
        moveGrid = friendParent.GetComponent<MoveGrid>();
    }
    /*
    private void Update()
    {
        if (friendParent != null)
        {
            for (int i = 0; i < friendParent.childCount; i++)
            {
                if (friendSlots[i] = null)
                {
                    friendSlots.Remove(friendSlots[i]);
                }

                if (!friendSlots.Contains(friendParent.GetChild(i).GetComponent<FriendSlot>()))
                {
                    friendSlots.Add(friendParent.GetChild(i).GetComponent<FriendSlot>());
                }
            }
        }
    }

            /*
        int i = 0;
        for (; i < friendSlots.Count; i++)
        {
            //friendSlots[i]. = npcs[i];
            friendSlots[i].friendSlotNumber = i;
        }

        for (; i < friendSlots.Count; i++)
        {
            if(friendSlots[i] == null)
            {
                friendSlots.Remove(friendSlots[i]);
            }
        }
        */

    public void LoadInFriends()
    {
        if(friendSlots.Count > 0)
        {
            for (int i = 0; i < friendSlots.Count; i++)
            {
                friendSlots[i].GetFriend(i);
            }
        }
    }

    public void RefreshUI()
    {
        Debug.Log("friendparent childcount: " + friendParent.childCount);

        if (friendParent != null)
        {
            friendSlots.Clear();
            for (int i = 1; i < friendParent.childCount; i++)
            {
                friendSlots.Add(friendParent.GetChild(i - 1).GetComponent<FriendSlot>());

            }
        }
    }

    public bool AddNPC(_NPC npc)
    {
        //npcs.Add(npc);
        friendSlotRef = friendSlot.GetComponent<FriendSlot>();
        friendSlotRef.nickname = npc.nickname;
        friendSlotRef.species = npc.species;
        friendSlotRef.personality = npc.personality;
        friendSlotRef.icon = npc.icon;
        friendSlotRef.prefab = npc.prefabName;
        friendSlotRef.hasNPC = true;
        friendSlotRef.hasBeenTalkedTo = npc.hasBeenTalkedTo;
        Instantiate(friendSlot, friendParent.transform);
        RefreshUI();
        //OnValidate();
        /*
        for (int i = 0; i < friendSlots.Length; i++)
        {
            if (!friendSlots[i].hasNPC)
            {
                friendSlots[i].nickname = npc.nickname;
                friendSlots[i].species = npc.species;
                friendSlots[i].personality = npc.personality;
                friendSlots[i].icon = npc.icon;
                friendSlots[i].prefab = npc.prefabName;
                friendSlots[i].hasNPC = true;
            }
        }
        */
        return true;
    }

    public void RemoveNPC(_NPC npc)
    {
        //npcs.Contains(npc);
        //npcs.Remove(npc);
    }

    public void SaveFriends()
    {

        for (int i = 0; i < friendSlots.Count; i++)
        {
            if (friendSlots[i] != null)
            {
                friendSlots[i].SaveFriend(i);
            }
            else
            {
                Debug.Log("is null!");
            }
        }
    }

    public void CheckTalkedToBool()
    {

        for (int i = 0; i < friendSlots.Count; i++)
        {
            friendSlots[i].GetTalkedToBool();
        }
    }

    public void RefreshGrid()
    {
        Debug.Log("refresh UI");
        moveGrid.Refresh();
        RefreshUI();
        Debug.Log("out of refresh UI");
    }
}