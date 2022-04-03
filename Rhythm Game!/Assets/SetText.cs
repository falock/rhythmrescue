using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetText : MonoBehaviour
{
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = Screen.currentResolution.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
