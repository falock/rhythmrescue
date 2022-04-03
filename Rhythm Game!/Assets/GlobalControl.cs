using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    public List<string> nickname = new List<string>();
    public List<string> personality = new List<string>();
    public List<_Species> species = new List<_Species>();
    public List<Sprite> icon = new List<Sprite>();
    public List<string> objectPrefabName = new List<string>();
    public List<int> id = new List<int>();

    public List<string> teamNickname = new List<string>();
    public List<string> teamPersonality = new List<string>();
    public List<_Species> teamSpecies = new List<_Species>();
    public List<Sprite> teamIcon = new List<Sprite>();
    public List<string> teamObjectPrefabName = new List<string>();
    public List<int> teamID = new List<int>();

    public GameObject npc;

    private _NPC npcScript;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    /*
    public void SpawnTeamMembers()
    {
        for (int i = 0; i < teamID.Count; i++)
        {
            npc = Instantiate(teamObjectPrefab[i], new Vector2(i, i), Quaternion.Euler(0, 0, 0));
            npcScript = npc.GetComponent<_NPC>();
            npcScript.nickname = teamNickname[i];
            npcScript.personality = teamPersonality[i];
        }
    }
    */
}
