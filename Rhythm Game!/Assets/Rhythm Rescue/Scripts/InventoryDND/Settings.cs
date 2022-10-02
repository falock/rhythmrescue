using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    // so it's accessible for the other scripts
    public static Settings current;

    // now list all the settings needed
    public float speed;
    public int difficulty;
    public int noteColour;
    public int laneDesign;
    public float musicVolume;
    public float sfxVolume;
    public int animationsBool;
    public int windowedBool;

    public string[] difficultyName;
    public string[] noteColourName;
    public string[] laneDesignName;

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

        speed = 5;
        difficulty = 2;
        noteColour = 1;
        laneDesign = 1;
        musicVolume = 100;
        sfxVolume = 100;
        animationsBool = 1;

        UpdateSettingsText();
    }

    public void Up(string variableName)
    {
        if(variableName == "speed" && speed < 7)
        {
            speed = speed + 0.1f;
        }
        else if (variableName == "difficulty" && difficulty < 3)
        {
            difficulty = difficulty + 1;
        }
        else if (variableName == "noteColour" && noteColour < 4)
        {
            noteColour = noteColour + 1;
        }
        else if (variableName == "laneDesign" && laneDesign < 3)
        {
            laneDesign = laneDesign + 1;
        }
        UpdateSettingsText();
    }

    public void Down(string variableName)
    {
        if (variableName == "speed" && speed > 1)
        {
            speed = speed - 0.1f;
        }
        else if (variableName == "difficulty" && difficulty > 1)
        {
            difficulty = difficulty - 1;
        }
        else if (variableName == "noteColour" && noteColour > 1)
        {
            noteColour = noteColour - 1;
        }
        else if (variableName == "laneDesign" && laneDesign > 1)
        {
            laneDesign = laneDesign - 1;
        }
        UpdateSettingsText();
    }

    // currently only supports two toggle buttons
    public void Toggle(string variableName)
    {
        if(variableName == "animationsBool")
        {
            if (animationsBool == 0) animationsBool = 1;
            else animationsBool = 0;
        }
        else
        {
            if (windowedBool == 0) windowedBool = 1;
            else windowedBool = 0;
        }
        //UpdateSettingsText();
    }

    public void MusicVolumeChange(Slider slider)
    {
        musicVolume = slider.value;
        UpdateSettingsText();
    }

    public void SFXVolumeChange(Slider slider)
    {
        sfxVolume = slider.value;
        UpdateSettingsText();
    }

    private void UpdateSettingsText()
    {
        //InventoryManager.current.UpdateSettingsText();
    }

}
