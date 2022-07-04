using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager current;

    Dictionary<string, int> settingsCap = new Dictionary<string, int>();
    public Dictionary<string, int> settingsValues = new Dictionary<string, int>();
    private List<string> settingsStrings = new List<string>();

    public float musicVolume;
    public float sfxVolume;
    public int animationsBool;
    public int windowedBool;

    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private TextMeshProUGUI noteColourText;
    [SerializeField] private TextMeshProUGUI laneDesignText;

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

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        // initiate the settings caps
        settingsCap.Add("speed", 2);
        // 4.0 to 6.0
        settingsCap.Add("difficulty", 2);
        // easy, normal, hard
        settingsCap.Add("noteColour", 3);
        // red, blue, purple, yellow
        settingsCap.Add("laneDesign", 2);
        // lines outlne, lines for each lane, opaque, solid

        settingsStrings.Add("speed");
        settingsStrings.Add("difficulty");
        settingsStrings.Add("noteColour");
        settingsStrings.Add("laneDesign");

        settingsValues.Add("speed", 5);
        settingsValues.Add("difficulty", 1);
        settingsValues.Add("noteColour", 0);
        settingsValues.Add("laneDesign", 0);

        // load in any settings
        LoadValues();
        DisplayValues();
    }

    public void ArrowButtons(string name)
    {
        string[] subs = name.Split(' ');
        int upDown = Convert.ToInt32(subs[0]);

        Debug.Log("subs: " + subs[0] + subs[1] + ". upDown: " + upDown);

        if (settingsValues.ContainsKey(subs[1]))
        {
            if (upDown == 0 && settingsValues[subs[1]] > 1)
            {
                settingsValues[name]--;
            }
            else if (upDown == 1 && settingsValues[subs[1]] < settingsCap[subs[1]])
            {
                settingsValues[name]++;
            }
        }

        Debug.Log("about to display values");
        DisplayValues();
    }

    private void SaveValues()
    {

    }

    private void LoadValues()
    {
        // goes through each playerprefs to see if a value is set, if it isn't it goes to the default value
        for (int i = 0; i < settingsStrings.Count; i++)
        {
            if (PlayerPrefs.GetInt(settingsStrings[i]) != 0)
            {
                settingsValues[settingsStrings[i]] = PlayerPrefs.GetInt(settingsStrings[i]);
            }
        }
    }

    public void DisplayValues()
    {
        speedText.text = settingsValues["speed"].ToString();
        difficultyText.text = settingsValues["difficulty"].ToString();
        noteColourText.text = settingsValues["noteColour"].ToString();
        laneDesignText.text = settingsValues["laneDesign"].ToString();
        Debug.Log("displayd values, speed: " + settingsValues["speed"] + ". difficulty: " + settingsValues["difficulty"]);
    }

    public void Toggle()
    {

    }
}
