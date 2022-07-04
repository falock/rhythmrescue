using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public enum NPCSpeciesType
{
    Plains,
    Jungle,
    Mountains,
    NPCPrefabs,
}
public class TryingToSpawnPrefab : MonoBehaviour
{
    [Tooltip("Add the prefabs of the animals you want to be spawned in the scene here")]
    public List<GameObject> npcPrefabList = new List<GameObject>();
    [Tooltip("How many enemies should spawn in this level?")]
    public float numberOfEnemiesToSpawn;
    [NonSerialized] public GameObject npc;

    private _NPC npcScript;
    [NonSerialized] public string prefabName;
    public static int npcID = 0;
    private string npcPersonality;
    [SerializeField] private GameObject recruitTriggerBox;
//    private int f = -3;
    private int g = 1;
    private int f = -3;

    private List<string> _personality = new List<string>();

    private void Awake()
    {
        _personality.Add("Brave");
        _personality.Add("Caring");
        _personality.Add("Class Clown");
        _personality.Add("Shy");
        _personality.Add("Rebel");
    }

    /*
    public void SpawnNPCs()
    {
        for (int i = 0; i < 4; i++)
        {
            // Choose Sprite AND Species
            var number = Random.Range(0, npcPrefabList.Count);

            // SPAWN ENEMY
            npc = Instantiate(npcPrefabList[number], new Vector2(i, i), Quaternion.identity);
            npcScript = npc.GetComponent<_NPC>();
            prefabName = npcPrefabList[number].name;
            npcScript.prefabName = prefabName;

            // Choose Personality
            npcPersonality = _personality[Random.Range(0, _personality.Count)];
            npcScript.personality = npcPersonality;
            npcScript.npcID = npcID;
            npcID++;
            g++;
        }
    }
    */
    public void SpawnNPCEnemies()
    {
        for (int j = 0; j < numberOfEnemiesToSpawn; j++)
        {
            // Choose Sprite AND Species
            var number = Random.Range(0, npcPrefabList.Count);
            // SPAWN ENEMY
            npc = Instantiate(npcPrefabList[number], GameObject.Find("SP" + g).transform);
            // npc = Instantiate(npcPrefabList[number], GameObject.Find("SP" + g).transform.position, Quaternion.identity);
            //GameObject.Find("SP" + g).transform.rotation);
            npcScript = npc.GetComponent<_NPC>();
            //Instantiate(recruitTriggerBox, npc.transform.position, Quaternion.identity, npc.transform);
            // Choose Personality
            npcPersonality = _personality[Random.Range(0, _personality.Count)];
            npcScript.personality = npcPersonality;
            npc.tag = "Enemy";
            Debug.Log("enemy" + g + " local rotation: " + npc.gameObject.transform.localRotation + " | rotation: " + npc.gameObject.transform.rotation);

            f = f + 2;
            g++;
        }
    }
    
}
