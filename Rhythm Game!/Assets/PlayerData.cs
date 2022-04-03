using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    public string npcPersonality;
    public string npcFavItem;
    public string npcName;
    public string npcSpecies;

    public PlayerData(NPCPersonality npc)
    {
        npcPersonality = npc.npcPersonality;
        npcFavItem = npc.npcFavItem;
        npcName = npc.npcName;
        npcSpecies = npc.npcSpecies;
    }
}
