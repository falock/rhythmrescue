using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public List<GameObject> npcPrefabList = new List<GameObject>();
    public GameObject npc;

    private NPCPersonality npcScript;

    public List<string> npcPersonalityList = new List<string>();
    public List<string> npcFavItemList = new List<string>();
    public List<string> npcNameList = new List<string>();
    public List<string> npcSpeciesList = new List<string>();

    public string npcPersonality;
    public string npcFavItem;
    public string npcName;
    public string npcSpecies;

    private int f = -3;
    private static int g = 1;
    public List<ScriptableObject> sObjects = new List<ScriptableObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Character Species
        npcSpeciesList.Add("Dog");
        npcSpeciesList.Add("Hippo");

        // Character Personalities
        npcPersonalityList.Add("Brooding");
        npcPersonalityList.Add("Snarky");
        npcPersonalityList.Add("Excitable");

        // Character Favourite Items
        npcFavItemList.Add("ChessSet");
        npcFavItemList.Add("Bench");
        npcFavItemList.Add("Football");
        npcFavItemList.Add("Fire");

        // Character Names
        npcNameList.Add("Yasmin");
        npcNameList.Add("Cohen");
        npcNameList.Add("Bubbles");
        npcNameList.Add("Miranda");
        npcNameList.Add("Selena");
        npcNameList.Add("Lily");
    }

    public void SpawnNPCs()
    {
        for (int i = 0; i < 4; i++)
        {
            // Choose Sprite AND Species
            var number = Random.Range(0, npcPrefabList.Count);

            // SPAWN ENEMY
            npc = Instantiate(npcPrefabList[number], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            npcScript = npc.GetComponent<NPCPersonality>();

            npcSpecies = npcSpeciesList[number];
            npcScript.npcSpecies = npcSpecies;

            // Choose Favourite Item
            npcFavItem = npcFavItemList[Random.Range(0, npcFavItemList.Count)];
            npcScript.npcFavItem = npcFavItem;

            // Choose Character Name
            npcName = npcNameList[Random.Range(0, npcNameList.Count)];
            npcScript.npcName = npcName;
            npc.name = "npc" + g;

            // Choose Personality
            npcPersonality = npcPersonalityList[Random.Range(0, npcPersonalityList.Count)];
            npcScript.npcPersonality = npcPersonality;
            g++;
        }
    }

    public void SpawnNPCEnemies()
    {
        for (int j = 0; j < 4; j++)
        {
            // Choose Sprite AND Species
            var number = Random.Range(0, npcPrefabList.Count);

            // SPAWN ENEMY
            npc = Instantiate(npcPrefabList[number], new Vector3(f, 15.3f, 0), Quaternion.Euler(-60, 0, 0), this.gameObject.transform);
            npcScript = npc.GetComponent<NPCPersonality>();

            npcSpecies = npcSpeciesList[number];
            npcScript.npcSpecies = npcSpecies;

            // Choose Favourite Item
            npcFavItem = npcFavItemList[Random.Range(0, npcFavItemList.Count)];
            npcScript.npcFavItem = npcFavItem;

            // Choose Character Name
            npcName = npcNameList[Random.Range(0, npcNameList.Count)];
            npcScript.npcName = npcName;
            npc.name = "npc" + g;

            // Choose Personality
            npcPersonality = npcPersonalityList[Random.Range(0, npcPersonalityList.Count)];
            npcScript.npcPersonality = npcPersonality;

            npc.gameObject.tag = "Enemy";

            f = f + 2;
            g++;
        }
    }

    public void SaveNPC()
    {
        SystemSave.SaveNPC(npcScript);
    }

    public void LoadNPC()
    {
        PlayerData data = SystemSave.LoadPlayer();

        npcScript.npcPersonality = data.npcPersonality;
        npcScript.npcFavItem = data.npcFavItem;
        npcScript.npcName = data.npcName;
        npcScript.npcSpecies = data.npcSpecies;
    }

}
