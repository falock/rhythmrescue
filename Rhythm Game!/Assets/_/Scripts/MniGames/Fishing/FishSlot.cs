using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishSlot : MonoBehaviour
{
    [Header("Type Values Inspector")]
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

    private void Awake()
    {
        // needs to retrieve any saved data
        // needs to display values
        if(PlayerPrefs.GetInt("caught" + number) == 1)
        {
            caught = true;
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

        if(PlayerPrefs.GetInt("caught" + number) == 0)
        {
            PlayerPrefs.SetInt("caught" + number, 1);
        }
        else
        {
            alreadyBeenCaught = true;
        }
        var sizeCaught = Random.Range(minSize, maxSize);
        if(sizeCaught > biggestCatch)
        {
            biggestCatch = sizeCaught;
            newRecord = true;
        }
        PlayerPrefs.SetFloat("biggestCatch" + number, biggestCatch);
        FishingManager.current.DisplayCaughtFish(sizeCaught, alreadyBeenCaught, newRecord, fishName, fishImage);
    }
}
