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
    private List<float> posX = new List<float>();
    private List<float> posY = new List<float>();
    private string[] personalityString = new string[] { "Class Clown", "Brave", "Shy", "Caring", "Rebel" };
    // This is for if you have
    //[SerializeField] EquipmentType equipmentType;

private void Start()
    {
        posX.Add(-7.3f);
        posX.Add(1.12f);
        posX.Add(7.24f);
        posX.Add(5.48f);
        posX.Add(1.21f);
        posX.Add(-4.59f);

        posY.Add(1);
        posY.Add(0.16f);
        posY.Add(0.45f);
        posY.Add(-3.08f);
        posY.Add(-1.94f);
        posY.Add(-3.02f);
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
        for (int i = 0; i < teamSlots.Length; i++)
        {
            var x = posX[Random.Range(0, posX.Count -1)];
            var y = posY[Random.Range(0, posY.Count -1)];
            teamSlots[i].SpawnThisNPCCamp(x, y);
            posX.Remove(x);
            posY.Remove(y);
        }

        posX.Add(-7.3f);
        posX.Add(1.12f);
        posX.Add(7.24f);
        posX.Add(5.48f);
        posX.Add(1.21f);
        posX.Add(-4.59f);

        posY.Add(1);
        posY.Add(0.16f);
        posY.Add(0.45f);
        posY.Add(-3.08f);
        posY.Add(-1.94f);
        posY.Add(-3.02f);
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
