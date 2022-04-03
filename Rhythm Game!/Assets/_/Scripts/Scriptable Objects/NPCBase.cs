using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBase : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string faveItem;
    [SerializeField] Personality personality;

    public enum Personality
    {
        Brooding,
        Excitable,
        Snarky,
    }
}
