using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    [Header("Visual Items - Assign in Inspector")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private Image full;
    [SerializeField] private Slider slider;

    [Header("Enter Achievement Values")]
    public string achievementName;
    [SerializeField] private int maxAchievementLevel;

    public int currentLevel; // track the player's progress
    public bool isAchieved;

    private void Awake()
    {
        Debug.Log("nameText: " + nameText + "object name: " + this.gameObject.name);
        Debug.Log("achievement name " + achievementName);
    }
    private void OnEnable()
    {
        nameText.text = achievementName;
        currentLevelText.text = currentLevel.ToString() + "/" + maxAchievementLevel.ToString();
        slider.maxValue = maxAchievementLevel;
        slider.value = currentLevel;
        if (currentLevel >= maxAchievementLevel)
        {
            isAchieved = true;
            full.enabled = true;
        }
        currentLevelText.text = currentLevel.ToString() + "/" + maxAchievementLevel.ToString();
        slider.value = currentLevel;
    }

    public void IncreaseAchievement(int amount)
    {
        Debug.Log("INSIDDE inside achievement");
        if(currentLevel >= maxAchievementLevel)
        {
            isAchieved = true;
            full.enabled = true;
        }
        currentLevel = currentLevel + amount;
        Debug.Log("currentLevel: " + currentLevel);
        currentLevelText.text = currentLevel.ToString() + "/" + maxAchievementLevel.ToString();
        slider.value = currentLevel;
    }
}
