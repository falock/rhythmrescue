using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public enum _Species
{
    Dog,
    Cat,
    Rabbit,
    Mouse,
    Hamster,

    Pig,
    Cow,
    Fox,
    Deer,
    Horse,

    Bear,
    Frog,
    Sloth,
    Wolf,
    Raccoon,

    Hippo,
    Elephant,
    Lion,
    Rhino,
    Meerkat,
}

public enum _Location
{
    Starter,
    Plains,
    Woods,
}

public class _NPC : MonoBehaviour , IPointerEnterHandler
{
    [Header("Assigned By Player On Recruitment")]
    public string nickname;

    [Header("Assigned In Prefab")]
    public _Species species;
    public _Location location;
    public Sprite icon;
    public GameObject objectPrefab;
    public string prefabName;

    [Header("Assigned Through Script")]
    public string personality;
    public int npcID;
    public bool saveNPC = false;
    public bool hasBeenTalkedTo;
    public bool hasBeenAdded;
    public bool spawnedStarters;

    /*
    public void OnPointerClick(PointerEventData eventData)
    {
        if (InventoryManager.current.recruitScreen.activeInHierarchy)
        {
            //InventoryManager.current.FriendListContainsCheck(this);
            //InventoryManager.current.TeamListContainsCheck(this);
            if (this.gameObject.tag == "Enemy")
            {
                InventoryManager.current.AddToFriendList(this);
                Debug.Log(species);
            }
        }
    }
    */
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(this.gameObject.tag == "Friend")
        {
            // Display NPC name
        }
    }
}