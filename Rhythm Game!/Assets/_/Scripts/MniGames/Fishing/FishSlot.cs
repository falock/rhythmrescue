using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class FishSlot : MonoBehaviour
{
    [Header("Assign Values in Inspector")]
    public string fishName;
    public float minSize;
    public float maxSize;
    public string fact;
    [Header("Assign")]
    public Image fishImage;
    public TextMeshProUGUI fishNameText;
    public TextMeshProUGUI sizeRangeText;
    public TextMeshProUGUI biggestCatchText;
    public TextMeshProUGUI factText;
    [Header("Assigned in Game")]
    public bool caught;
    public float biggestCatch;
    public int number;

    public int fishCategory;

    private void Awake()
    {
        // needs to retrieve any saved data
        // needs to display values
        /*
        if(PlayerPrefs.GetInt("caught" + number) == 1)
        {
            caught = true;
            fishNameText.text = fishName;
            sizeRangeText.text = minSize.ToString() + "-" + maxSize.ToString() + "cm";
            biggestCatchText.text = PlayerPrefs.GetFloat("biggestCatch" + number).ToString() + "cm";
            factText.text = fact;
        }
        */
    }

    private void Update()
    {
        if (this.gameObject.activeInHierarchy)
        {
            Debug.Log("updatee update");
            fishNameText.text = fishName;
            sizeRangeText.text = minSize.ToString() + "-" + maxSize.ToString() + "cm";
            biggestCatchText.text = PlayerPrefs.GetFloat("biggestCatch" + number).ToString() + "cm";
            factText.text = fact;
        }
    }

    public void CatchFish()
    {
        bool alreadyBeenCaught = false;
        bool newRecord = false;
        caught = true;
        alreadyBeenCaught = true;

        var sizeCaught = Math.Round(Random.Range(minSize, maxSize), 2);
        var sizeCaughtFloat = (float)sizeCaught;
        if (sizeCaught > biggestCatch)
        {
            biggestCatch = sizeCaughtFloat;
            newRecord = true;
        }
        PlayerPrefs.SetFloat("biggestCatch" + number, biggestCatch);
        FishingManager.current.DisplayCaughtFish(sizeCaughtFloat, alreadyBeenCaught, newRecord, fishName, fishImage.sprite);
        Debug.Log("caught" + PlayerPrefs.GetInt("caught" + number));
    }
}
