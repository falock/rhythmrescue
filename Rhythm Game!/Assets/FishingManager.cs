using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FishingManager : MonoBehaviour
{
    public static FishingManager current;
    public GameObject fishingRod;
    public GameObject playerSpawn;
    public GameObject playerReference;
    public FishSlot[] fishStoredInUI;

    [Header("DisplayRecentlyCaughtFish")]
    public GameObject caughtFishDisplay;
    public TextMeshProUGUI fishName;
    public TextMeshProUGUI fishSize;
    public GameObject newFish;
    public GameObject newSizeRecord;
    public Image fishImage;

    private void Awake()
    {
        //Check if instance already exists
        if (current == null)
        {
            //if not, set instance to this
            current = this;
        }
        //If instance already exists and it's not this:
        else if (current != this)
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < fishStoredInUI.Length; i++)
        {
            fishStoredInUI[i].number = i;
        }
    }

    public void CatchFish(Fish fish)
    {
        for (int i = 0; i < fishStoredInUI.Length; i++)
        {
            if (fishStoredInUI[i].name == fish.name)
            {
                fishStoredInUI[i].CatchFish();
            }
        }
    }

    public void DisplayCaughtFish(float sizeCaught, bool firstTime, bool sizeRecord, string name, Image image)
    {
        caughtFishDisplay.SetActive(true);
        fishName.text = name;
        fishSize.text = sizeCaught.ToString();
        newFish.SetActive(firstTime);
        newSizeRecord.SetActive(sizeRecord);
        fishImage = image;
    }
}
