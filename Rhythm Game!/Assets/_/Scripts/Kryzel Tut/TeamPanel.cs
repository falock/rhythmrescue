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

private void Start()
    {

    }
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
        /*
        for (int j = 0; j < CampObjectManager.current.npcSpawnLocation.Count; j++)
        {
            for (int k = 0; k < CampObjectManager.current.npcSpawnLocation[j].transform.childCount; k++)
            {
                pos.Add(CampObjectManager.current.npcSpawnLocation[j].transform.GetChild(k).transform);
            }
        }
        */

        for (int i = 0; i < teamSlots.Length; i++)
        {
            var choice = Random.Range(0, 2);
            if (choice == 0)
            {
                var spawn = CampObjectManager.current.Interactable();
                teamSlots[i].SpawnThisNPCCamp(spawn, null);
            }
            else
            {
                var spawn = CampObjectManager.current.JustPosition();
                teamSlots[i].SpawnThisNPCCamp(null, spawn);
            }
        }

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
