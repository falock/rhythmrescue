using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class TeamSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
    private Button xButton;
    private Button plusButton;
    public int teamSlotNumber;
    public Sprite teamIcon;
    private bool selectingATeamMember;
    public Button selectButton;
    /*private void Start()
    {
        Npc.nickname = GlobalControl.Instance.teamNickname[teamSlotNumber];
        Npc.personality = GlobalControl.Instance.teamPersonality[teamSlotNumber];
        Npc.species = GlobalControl.Instance.teamSpecies[teamSlotNumber];
        Npc.icon = GlobalControl.Instance.teamIcon[teamSlotNumber];
        Npc.objectPrefab = GlobalControl.Instance.teamObjectPrefab[teamSlotNumber];
    }
    */

    //private void Start()
    //{
    //teamIcon = Resources.Load<Sprite>("PlayerIcons/" + teamSlotNumber); //npcPrefab.GetComponent<_NPC>().icon;
    //}

    private void Awake()
    {
        npcPersonalityText = npcPersonality.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        npcNameText = npcName.GetComponent<TextMeshProUGUI>();
        npcSpeciesText = npcSpecies.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        //xButton = this.transform.Find("XButton").GetComponent<Button>();
        //xButton.onClick.AddListener(() => InventoryManager.current.DeleteFriend(this));

        //plusButton = this.transform.Find("PlusButton").GetComponent<Button>();
        teamIcon = Resources.Load<Sprite>("PlayerIcons/" + teamSlotNumber); //npcPrefab.GetComponent<_NPC>().icon;
        //plusButton.onClick.AddListener(() => InventoryManager.current.ReplaceTeamMember());
        selectButton = this.gameObject.GetComponent<Button>();
    }

    private void Update()
    {
        npcIcon.sprite = teamIcon;
        if (selectingATeamMember == false)
        {
           selectButton.enabled = false;
        }
    }

    private void OnEnable()
    {
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
            npcIcon.SetNativeSize();
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
    protected virtual void OnValidate()
    {
        if (background == null)
        {
            background = GetComponent<Image>();
        }
    }
    public void SpawnThisNPCRhythm(int i)
    {
        npcPrefab = Resources.Load("NPCPrefabs/Starters/" + prefab) as GameObject;
        npcScript = npcPrefab.GetComponent<_NPC>();
        npcScript.nickname = nickname;
        npcScript.personality = personality;
        npcScript.prefabName = prefab;
        npcScript.hasBeenAdded = true;
        if (i >= 2)
        {
            i++;
        }
        var parent = RhythmManager.instance.rhythmGameItems.transform.Find("Heroes").transform.Find(i.ToString()).transform;
        GameObject npc = Instantiate(npcPrefab, parent.transform.position, Quaternion.identity, parent) as GameObject;
    }

    public void SpawnThisNPCCamp(CampObject obj, Transform pos, bool interactingWithObject)
    {
        //Instantiate(npcPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        // instantiate player
        // var npcCharacter = Resources.Load("NPCPrefabs/Starters/" + prefab);
        npcPrefab = Resources.Load("NPCPrefabs/Starters/" + prefab) as GameObject;
        Debug.Log("in teamslot SPAWNTHISNPC");
        npcScript = npcPrefab.GetComponent<_NPC>();
        npcScript.nickname = nickname;
        npcScript.personality = personality;
        npcScript.prefabName = prefab;
        npcScript.hasBeenTalkedTo = hasBeenTalkedTo;
        npcScript.hasBeenAdded = true;
        if (interactingWithObject)
        {
            var npc = Instantiate(npcPrefab, new Vector2(obj.gameObject.transform.position.x, obj.gameObject.transform.position.y), Quaternion.identity);
            npc.GetComponent<NPCController>().ChooseCampActivity(obj.animationType.ToString());
            Debug.Log("Just called to spawn while interacting" + obj);
        }
        else
        {
            var npc = Instantiate(npcPrefab, new Vector2(pos.position.x, pos.position.y), Quaternion.identity);
            npc.GetComponent<NPCController>().isInteractingWithCampObject = false;
            Debug.Log("Just called to spawn walking" + obj);
        }
    }


    public void SaveThisNPC()
    {
        //Npc.saveNPC = true;
    }

    public void SaveTeamMember()
    {
        /*
        GlobalControl.Instance.teamNickname.Add(Npc.nickname);
        GlobalControl.Instance.teamPersonality.Add(Npc.personality);
        GlobalControl.Instance.teamSpecies.Add(Npc.species);
        GlobalControl.Instance.teamIcon.Add(Npc.icon);
        GlobalControl.Instance.teamObjectPrefab.Add(Npc.objectPrefab);
        GlobalControl.Instance.teamID.Add(teamSlotNumber);
        */
    }

    public void SelectingATeamMember(bool answer)
    {
        selectingATeamMember = answer;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selectingATeamMember == true)
        {
            LeanTween.scale(this.gameObject, new Vector3(1.4f, 1.4f, 1.4f), 0.1f);
            selectButton.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(this.gameObject, new Vector3(1.29f, 1.29f, 1.29f), 0.1f);
        selectButton.enabled = false;

    }

    public void RefreshTeamMember()
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
        npcIcon.SetNativeSize();
    }
}
