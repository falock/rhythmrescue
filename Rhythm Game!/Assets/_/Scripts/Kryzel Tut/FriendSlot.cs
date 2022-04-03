using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class FriendSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image background;
    //private InventoryManager inventoryManager;
    public int friendSlotNumber;
    [Header("NPC Info")]
    public string nickname;
    public string personality;
    public _Species species;
    public Sprite icon;
    public string prefab;
    public bool hasBeenTalkedTo;
    [Header("UI Card Displays")]
    public GameObject npcName;
    public GameObject npcPersonality;
    public GameObject npcSpecies;
    public Image npcIcon;
    [Header("Displaying NPC Info")]
    [NonSerialized] public TextMeshProUGUI npcNameText;
    [NonSerialized] public TextMeshProUGUI npcPersonalityText;
    [NonSerialized] public TextMeshProUGUI npcSpeciesText;

    [Header("Ignore")]
    [SerializeField] public GameObject npcPrefab;
    public _NPC npcScript;
    [SerializeField] private GameObject spawn;
    public bool hasNPC = false;
    [SerializeField] private Button xButton;
    [SerializeField] private Button plusButton;


    private void Awake()
    {
        npcPersonalityText = npcPersonality.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        npcNameText = npcName.GetComponent<TextMeshProUGUI>();
        npcSpeciesText = npcSpecies.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
   
    private void Start()
    {
        //xButton = this.transform.Find("XButton").GetComponent<Button>();
        xButton.onClick.AddListener(() => DeleteFriend());
        xButton.gameObject.SetActive(false);
        plusButton.gameObject.SetActive(false);
        //plusButton = this.transform.Find("PlusButton").GetComponent<Button>();
        plusButton.onClick.AddListener(() => InventoryManager.current.ReplaceTeamMember(this));
    }

    private void OnEnable()
    {
        LeanTween.scale(this.gameObject, new Vector3(0.7f, 0.7f, 0.7f), 0.01f);
        xButton.gameObject.SetActive(false);
        plusButton.gameObject.SetActive(false);
        if (hasNPC)
        {
            npcPersonalityText.text = " " + personality;
            npcNameText.text = " " + nickname;
            npcSpeciesText.text = " " + species.ToString();
            npcIcon.enabled = true;
            
            background.enabled = true;
            npcPrefab = Resources.Load("NPCPrefabs/Starters/" + prefab) as GameObject;
//            npcIcon.sprite = npcPrefab.GetComponent<_NPC>().icon;
            npcIcon.sprite = icon;
            npcName.SetActive(true);
            npcPersonality.SetActive(true);
            npcSpecies.SetActive(true);
            //npcIcon.SetNativeSize();
        }
        else
        {
            npcIcon.enabled = false;
            background.enabled = false;
            npcName.SetActive(false);
            npcPersonality.SetActive(false);
            npcSpecies.SetActive(false);
            //npcPrefab = prefab;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(this.gameObject, new Vector3(0.9f, 0.9f, 0.9f), 0.1f);
        xButton.gameObject.SetActive(true);
        plusButton.gameObject.SetActive(true);
        xButton.enabled = true;
        plusButton.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(this.gameObject, new Vector3(0.7f, 0.7f, 0.7f), 0.01f);
        xButton.gameObject.SetActive(false);
        plusButton.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //InventoryManager.current.AddTeamMember(Npc);
        //askToAddTeamMember.SetActive(true);
    }

    /*
    protected virtual void OnValidate()
    {
        if (background == null)
        {
            background = GetComponent<Image>();
        }
    }
    */
    
    public void SaveFriend(int i)
    {
        PlayerPrefs.SetString("friendNickname" + i, nickname);
        PlayerPrefs.SetString("friendPersonality" + i, personality);
        PlayerPrefs.SetString("friendPrefabName" + i, prefab);
    }

    public void GetFriend(int i)
    {
        nickname = PlayerPrefs.GetString("friendNickname" + i);
        personality = PlayerPrefs.GetString("friendPersonality" + i);
        prefab = PlayerPrefs.GetString("friendPrefabName" + i);
        Debug.Log("got friends");
    }

    public void GetTalkedToBool()
    {
        //hasBeenTalkedTo = npcScript.hasBeenTalkedTo;
    }

    public void DeleteFriend()
    {
        Destroy(this.gameObject);
        InventoryManager.current.DeleteFriend();
    }

    public void AddFriendToTeam()
    {

    }
}