using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TeamPanel : MonoBehaviour
{

    [SerializeField] Transform teamSlotsParent;
    [SerializeField] public TeamSlot[] teamSlots;
    public List<int> teamMembers = new List<int>();
    public GameObject teamPrefab;
    private List<Transform> pos = new List<Transform>();
    private string[] personalityString = new string[] { "Class Clown", "Brave", "Shy", "Caring", "Rebel" };

    private CampObject campObject;
    private Transform campTransform;

    private List<Transform> tempPositions = new List<Transform>();
    // This is for if you have
    //[SerializeField] EquipmentType equipmentType;
    public List<int> npcObjectNumbers = new List<int>();
    public List<int> npcGeneralNumbers = new List<int>();

    private List<int> numbersAlreadyTakenObj = new List<int>();
    private List<int> numbersAlreadyTakenGen = new List<int>();

    public void OnValidate()
    {
        teamSlots = teamSlotsParent.GetComponentsInChildren<TeamSlot>();
    }
    public void RefreshTeam()
    {
        for (int i = 0; i < teamSlots.Length; i++)
        {
            teamSlots[i].RefreshTeamMember();
        }
    }
    public bool AddTeamMember(_NPC npc)
    {
        for (int i = 0; i < teamSlots.Length; i++)
        {
            //teamSlots[i].Npc = npc;
            teamMembers.Add(i);
            teamSlots[i].friendSlotNumber = i;
            return true;
        }
        return false;
    }

    public bool RemoveTeamMember(_NPC npc)
    {
        for (int i = 0; i < teamSlots.Length; i++)
        {
            //            if (teamSlots[i].Npc == npc)
            {
                //teamSlots[i].Npc = null;
                teamMembers.Remove(i);
                return true;
            }
        }
        return false;
    }

    public void SpawnTeamMembersCamp()
    {
        for (int i = 0; i < teamSlots.Length; i++)
        {
            var choice = Random.Range(0, 2);
            var obj = Random.Range(0, 2);
            Debug.Log("choice: " + choice + ". obj: " + obj + "contains?: " + numbersAlreadyTakenObj.Contains(obj));

            if (choice == 0 && !numbersAlreadyTakenObj.Contains(obj))
            {
                Debug.Log("into spawn");

                var spawn = CampObjectManager.current.npcSpawnLocation[obj];
                teamSlots[i].SpawnThisNPCCamp(spawn, null, true);
                numbersAlreadyTakenObj.Add(obj);
            }
            else
            {
                Debug.Log("in general");
                var general = GetGenNumber();
                var spawn = CampObjectManager.current.generalSpawnPoint[general];
                teamSlots[i].SpawnThisNPCCamp(null, spawn, false);
                Debug.Log("general: " + general);
                numbersAlreadyTakenGen.Add(general);
            }
        }

        numbersAlreadyTakenObj.Clear();
        numbersAlreadyTakenGen.Clear();
    }

    private int GetGenNumber()
    {
        int val = Random.Range(0, CampObjectManager.current.generalSpawnPoint.Count);
        while (numbersAlreadyTakenGen.Contains(val))
        {
            val = Random.Range(0, CampObjectManager.current.generalSpawnPoint.Count);
        }
        return val;
    }

    public void SpawnTeamMembersRhythm()
    {
        for (int i = 0; i < teamSlots.Length; i++)
        {
            teamSlots[i].SpawnThisNPCRhythm(i);
        }
    }

    //call this once at start of the game
    public void AddStarterMembers()
    {
        for (int i = 0; i < teamSlots.Length; i++)
        {
            teamSlots[i].nickname = PlayerPrefs.GetString("teamNickname" + i);
            // See what the personality is
            var personality = PlayerPrefs.GetString("teamPersonality" + i);
            if (personality == "Random" || personality == "[Random]")
            {
                var randomNumber = Random.Range(0, personalityString.Length);
                teamSlots[i].personality = personalityString[randomNumber];
            }
            else
            {
                teamSlots[i].personality = personality;
            }

            teamSlots[i].prefab = PlayerPrefs.GetString("teamPrefabName" + i);
            var teamSpecies = PlayerPrefs.GetString("teamSpecies" + i);
            teamSlots[i].species = (_Species)System.Enum.Parse(typeof(_Species), teamSpecies);
            teamSlots[i].hasNPC = true;
            teamSlots[i].teamSlotNumber = PlayerPrefs.GetInt("teamIcon" + i);
            teamMembers.Add(i);

            //teamPrefab = Resources.Load("NPCPrefabs/Starters" + starterPrefabName);
            ///var iconInt = PlayerPrefs.GetInt("teamIcon" + i);
            //teamSlots[i].icon = Resources.Load<Sprite>("PlayerIcons/" + iconInt);
            //teamSlots[i].icon = myFruit;
            //Debug.Log("icon int:" + iconInt.ToString());
            // teamSlots[i].npcScript = teamSlots[i].prefab.GetComponent<_NPC>();
        }
    }
}
/*
    public void SaveTeam()
    {
        for (int i = 0; i < teamSlots.Length; i++)
        {
            if(teamSlots[i] != null)
            {
                teamSlots[i].SaveThisNPC();
            }
        }
            else
        {
            Debug.Log("is null!");
        }
    }

 
}
*/
