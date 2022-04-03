using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCSpecies
{
    Dog,
    Cat,
    Rabbit,
    Mouse,
    Cow,
}

public enum npcPersonality
{
    Friendly,
    Mean,
    Meh,
}

public enum NPCEquippable
{
    Yes,
    No,
}

[CreateAssetMenu]
public class NPCInfo : ScriptableObject
{
    public NPCSpecies npcSpecies;
    public npcPersonality npcPersonality;
    public NPCEquippable npcEquippable;
    public Sprite npcSprite;
}
