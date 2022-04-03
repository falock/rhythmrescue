using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class NPCPersonality : MonoBehaviour
{
    [Header("NPC Info")]
    public int id;
    public string npcPersonality;
    public string npcFavItem;
    public string npcName;
    public string npcSpecies;
    public Sprite npcSprite;
    public SpriteRenderer spriteRenderer;

    [Header("Show Traits")]
    public bool showingNPCTraits;
    [SerializeField] public GameObject traitsUI;
    public TextMeshProUGUI species;
    public TextMeshProUGUI personality;

    // Start is called before the first frame update
    void Start()
    {
        traitsUI.SetActive(false);
    }
    private void Update()
    {
        if (this.tag == "Player")
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }


    void OnMouseOver()
    {
        //if (GameManager.recruitScreen)
        {
            //traitsUI.SetActive(true);
            //showingNPCTraits = true;
        }

        //traitsUI.SetActive(true);
        //species.text = "Species: " + npcSpecies.ToString();
        //personality.text = "Personality: " + npcPersonality.ToString();
        //Debug.Log(personality.text);
    }

    void OnMouseExit()
    {
        if (showingNPCTraits)
        {
            //traitsUI.SetActive(false);
            //showingNPCTraits = false;
        }
    }

    public void ShowTraits()
    {
        traitsUI.SetActive(true);
        species.text = "Species: " + npcSpecies.ToString();
        personality.text = "Personality: " + npcPersonality.ToString();
    }
    /*
    private void OnEnable()

    {

        GameSaveLoad.SaveDataEvent += Save;

    }

    private void OnDisable()

    {

        GameSaveLoad.SaveDataEvent -= Save;

    }

    public void Save(GameData data)

    {

        data.objects.Add(new ObjectData(id, transform.position.x, //etc etc ))

    }
    */
}



