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
    public GameObject fishCollectionUI;
    public FishSlot[] fishStoredInUI;
    
    [Header("DisplayRecentlyCaughtFish")]
    public GameObject recentlyCaughtFishDisplay;
    public TextMeshProUGUI fishName;
    public TextMeshProUGUI fishSize;
    public GameObject newFish;
    public GameObject newSizeRecord;
    public Image fishImage;

    [Header("NameLists")]
    public string[] smallFishNames;
    public string[] bigFishNames;

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

        recentlyCaughtFishDisplay.SetActive(false);

        for (int i = 0; i < fishStoredInUI.Length; i++)
        {
            fishStoredInUI[i].number = i;
        }

        List<string> smallFishNameList = new List<string>();
        List<string> bigFishNameList = new List<string>();
        // get all the names
        for (int i = 0; i < fishStoredInUI.Length; i++)
        {
            if(fishStoredInUI[i].fishCategory == 0)
            {
                smallFishNameList.Add(fishStoredInUI[i].name);
            }
            else if(fishStoredInUI[i].fishCategory > 0)
            {
                bigFishNameList.Add(fishStoredInUI[i].name);
            }
            // add medium fish eventually
        }

        smallFishNames = smallFishNameList.ToArray();
        bigFishNames = bigFishNameList.ToArray();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OpenFishDisplay();
        }

        if(fishCollectionUI.activeInHierarchy)
        {
            // need to pause the game!
            //Time.timeScale = 0;
        }
        else
        {
            //Time.timeScale = 1; 
        }
    }

    public void CatchFish(Fish fish)
    {
        Debug.Log("in Catch Fsh");
        for (int i = 0; i < fishStoredInUI.Length; i++)
        {
            if (fishStoredInUI[i].name == fish.name)
            {
                fishStoredInUI[i].CatchFish();
            }
        }
    }

    public void DisplayCaughtFish(float sizeCaught, bool firstTime, bool sizeRecord, string name, Sprite image)
    {
        recentlyCaughtFishDisplay.SetActive(true);
        fishName.text = name;
        fishSize.text = sizeCaught.ToString();
        //newFish.SetActive(firstTime);
        newSizeRecord.SetActive(sizeRecord);
        fishImage.sprite = image;
        fishImage.SetNativeSize();
        StartCoroutine(WaitToEndDisplay());
    }

    public string GetName(int fishSize)
    {
        if (fishSize == 0)
        {
            var selectRandomFishName = Random.Range(0, smallFishNames.Length);
            return smallFishNames[selectRandomFishName];
        }
        else if (fishSize > 0)
        {
            var selectRandomFishName = Random.Range(0, bigFishNames.Length);
            return bigFishNames[selectRandomFishName];
        }

        return null;
    }

    private IEnumerator WaitToEndDisplay()
    {
        yield return new WaitForSeconds(2f);
        recentlyCaughtFishDisplay.SetActive(false);
        StopAllCoroutines();
    }

    public void OpenFishDisplay()
    {
        if(!fishCollectionUI.activeSelf)
        {
            fishCollectionUI.SetActive(true);
            for (int i = 0; i < fishStoredInUI.Length; i++)
            {
                if (fishStoredInUI[i].caught) 
                {
                    fishStoredInUI[i].gameObject.SetActive(true);
                }
                else
                {
                    fishStoredInUI[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            fishCollectionUI.SetActive(false);
        }
    }
}
