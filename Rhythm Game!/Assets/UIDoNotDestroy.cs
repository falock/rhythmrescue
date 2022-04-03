using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDoNotDestroy : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
