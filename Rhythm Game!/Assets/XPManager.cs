using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class XPManager : MonoBehaviour
{
    public static XPManager instance;

    // How much XP is needed to get to the next level, calculated in IncreaseExperience();
    private float xpToNextLevel;
    // store the player's full experience, used when the player has finished a level
    // and has a LOT of XP that goes over the level up threshold
    private float playerXP;
    // how much XP the player currently has
    public float shownPlayerXP;
    // store the player's level, starts at 1
    public int playerLevel = 1;
    // how fast the slider should fill
    [SerializeField] private float fillSpeed = 0.5f;
    // what progress the slider is aiming for
    public float targetProgress;
    // update to show the player how much XP they have out of the total needed to level up
    public TextMeshProUGUI xpText;
    // reference to the slider
    private Slider progressSlider;
    // reference to the "Level Up!" text
    public GameObject levelUp;

    // if the Update() should run
    private bool checkingXP;

    // Start is called before the first frame update
    void Start()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseExperience(int xp)
    {
        // Increase the playerXP by the amount of XP given at the end of a level by the Rhythm Manager
        playerXP += xp;

        // Calculate how much XP is needed to get to the next level
        xpToNextLevel = (int)Mathf.Floor(100 * playerLevel * Mathf.Pow(playerLevel, 0.5f));

        // Assign the references since the XP manager is always awake and the objects referenced might be null otherwise
        progressSlider = InventoryManager.current.playerLevelUpdate;
        xpText = progressSlider.transform.Find("XPNumber").GetComponent<TextMeshProUGUI>();
        levelUp = progressSlider.transform.Find("LevelUp").gameObject;

        // Check to see if the XP should go to the next level or if it should just increase a bit
        if (playerXP >= xpToNextLevel)
        {
            targetProgress = 1;
            checkingXP = true;
        }
        else
        {
            targetProgress = playerXP / xpToNextLevel;
            checkingXP = true;
        }
    }

    private void Update()
    {
        if (!checkingXP) return;

        if (progressSlider.value < targetProgress)
        {
            // Increase the slider value by the fill speed and update the player XP display
            progressSlider.value += fillSpeed * Time.deltaTime;
            CalculatePlayerXP();
        }
        else if (progressSlider.value == progressSlider.maxValue)
        {
            // Reset the slider value when you've levelled up
            progressSlider.value = 0;

            // Update the level and level display number
            playerLevel++;
            InventoryManager.current.playerLevelDisplay.text = playerLevel.ToString();

            // Add the Level Up! to the screen and make it animate
            levelUp.SetActive(true);
            LeanTween.scale(levelUp, new Vector3(1.1f, 1.1f, 1.1f), 1f).setLoopType(LeanTweenType.pingPong);

            // Calculate what the current shown player XP is, and update the text for it
            CalculatePlayerXP();

            // Check the experience again to make sure the player is at the right level
            IncreaseExperience(0);

            // Turn off checkingXP so Update() doesn't happen anymore
            checkingXP = false;
        }
        else if (progressSlider.value == targetProgress)
        {
            // The player slider is now done updating since it is equal to the current target progress, update the XP display and finish
            CalculatePlayerXP();
            checkingXP = false;
        }
    }

    private void CalculatePlayerXP()
    {
        shownPlayerXP = Mathf.RoundToInt(progressSlider.value * xpToNextLevel);
        xpText.text = shownPlayerXP + "/" + xpToNextLevel;
    }
}
