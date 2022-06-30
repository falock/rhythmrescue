using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public GameObject item;
    public int ID;
    public string type;
    public string description;
    public Text text;
    public bool empty;
    public Sprite icon;

    private LevelManager theLevelManager;

    // Use this for initialization
    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateSlot()
    {
        this.GetComponent<Image>().sprite = icon;
        this.GetComponentInChildren<Text>().text = "" + theLevelManager.currentCount;

    }

}