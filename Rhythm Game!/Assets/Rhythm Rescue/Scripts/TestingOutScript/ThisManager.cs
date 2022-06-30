using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisManager : MonoBehaviour
{
    public ListHolder listHolder;

    // Start is called before the first frame update
    void Start()
    {
        listHolder = FindObjectOfType<ListHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
