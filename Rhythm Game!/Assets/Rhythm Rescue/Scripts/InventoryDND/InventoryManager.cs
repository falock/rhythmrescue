﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager current;
    [Header("Reference to Inventory Components")]
    public FriendList friendList;
    public TeamPanel teamPanel;
    public GameObject nicknameInput;
    public TextMeshProUGUI nicknameInputText;
    public GameObject nicknameConfirmScreen;
    public TMP_InputField inputField;

    [Header("General UI")]
    public GameObject notificationPanel;
    public TextMeshProUGUI notificationText;

    [NonSerialized] public string sceneName;
    [Header("Reference to Inventory UI")]
    public GameObject OpenJournal;
    public GameObject BackToMainMenu;
    public GameObject BackToCamp;
    public GameObject rhythmUIParent;

    [Header("Gameplay UI")]
    // UI text
    public TextMeshProUGUI scoreText;
    public GameObject score;
    public GameObject health;
    public TextMeshProUGUI responseText;

    [Header("Results Screen UI")]
    public GameObject resultsScreen;
    public GameObject firstResults;
    public GameObject secondResults;
    public TextMeshProUGUI percentHitText, normalsText, goodsText, perfectsText, badsText, missesText, rankText, finalScoreText;
    public Slider playerLevelUpdate;
    public TextMeshProUGUI playerLevelDisplay;
    public Slider playerScoreDisplay;

    [Header("Continue Game UI")]
    public GameObject recruitScreen;
    public GameObject tryAgainScreen;
    public GameObject rhythmUI;
    public bool canOpenJournal;

    [Header("Storing Player Info - Temporary")]
    public string player;
    public int lastPlayerLevel = 1;
    public FriendSlot currentFriend;

    [Header("Settings UI")]
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private TextMeshProUGUI noteColourText;
    [SerializeField] private TextMeshProUGUI laneDesignText;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Toggle animationsToggle;
    [SerializeField] private Toggle windowedToggle;

    [Header("ConversationManager")]
    public GameObject conversationManager;

    private string nickname;
    private _NPC currentNPC;
    public bool hasAddedFriend = false;

    public void Awake()
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

        //Call this to set up the UI
        OnLevelWasLoaded(SceneManager.GetActiveScene().buildIndex);

    }

    public void Start()
    {
        //inputField.onEndEdit.AddListener(SubmitName);
        nicknameInputText = nicknameInput.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void OnLevelWasLoaded(int level)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        // Retrieve the name of this scene.
        sceneName = currentScene.name;
        Debug.Log("scenename = " + sceneName);
        notificationPanel.SetActive(false);

        // how the UI should be for the Camp Scene
        if (sceneName == "Camp")
        {
            OpenJournal.SetActive(false);
            recruitScreen.SetActive(false);
            BackToMainMenu.SetActive(true);
            BackToCamp.SetActive(false);
            rhythmUIParent.SetActive(false);
            SpawnTeamMembersCamp();
            canOpenJournal = true;
            conversationManager.gameObject.SetActive(true);
        }

        // how the UI should be for the Main Screen
        else if (sceneName == "MainScreen")
        {
            OpenJournal.SetActive(false);
            BackToMainMenu.SetActive(false);
            BackToCamp.SetActive(true);
            rhythmUIParent.SetActive(false);
            SpawnPlayerMain();
            canOpenJournal = true;
            conversationManager.gameObject.SetActive(false);
        }

        // UI for the start screen
        else if (sceneName == "Start")
        {
            OpenJournal.SetActive(false);
            BackToMainMenu.SetActive(false);
            BackToCamp.SetActive(false);
            rhythmUIParent.SetActive(false);
            canOpenJournal = false;
            conversationManager.gameObject.SetActive(false);
        }

        // UI for the mini games scene
        else if (sceneName == "MiniGames")
        {
            OpenJournal.SetActive(false);
            BackToMainMenu.SetActive(false);
            // for now. eventually, make a specific pause screen for both the rhythm game and the mini games
            BackToCamp.SetActive(true);
            rhythmUIParent.SetActive(false);
            canOpenJournal = true;
            conversationManager.gameObject.SetActive(false);
            SpawnPlayerMiniGames();
        }

        // UI for the rhythm level scenes
        else
        {
            Debug.Log("Scene is ELSE");
            //conversationManager.gameObject.SetActive(false);
            OpenJournal.SetActive(false);
            BackToMainMenu.SetActive(false);
            BackToCamp.SetActive(false);
            rhythmUIParent.SetActive(true);
            rhythmUI.SetActive(true);
            resultsScreen.SetActive(false);
            resultsScreen.transform.GetChild(0).gameObject.SetActive(false);
            recruitScreen.SetActive(false);
            firstResults.SetActive(true);
            secondResults.SetActive(false);
            tryAgainScreen.SetActive(false);
            hasAddedFriend = false;
            lastPlayerLevel = level;
            canOpenJournal = false;
            //SpawnTeamMembersRhythm();
            if(RhythmManager.instance != null)
            {
                lastPlayerLevel = RhythmManager.instance.levelNumber;
            }
        }
    }

    // delete friend from friendList
    // called from X button attached to the FriendSlot
    public void DeleteFriend(FriendSlot friend)
    {
        Destroy(friend.gameObject);
    }

    // swap out a team member with a friend
    public void ReplaceTeamMember(FriendSlot friendSlot)
    {
        // keep reference to the friend we're swapping in
        currentFriend = friendSlot;
        // let the player know to select a team member
        notificationPanel.SetActive(true);
        notificationText.text = "Please select which Team Member you want to replace!";
    }

    // this is called when the player says "okay" on the notification panel
    // activates the select buttons on the team slots so a team member can be selected
    public void ConfirmToReplaceTeamMember()
    {
        for (int i = 0; i < teamPanel.teamSlots.Length; i++)
        {
            teamPanel.teamSlots[i].SelectingATeamMember(true);
            teamPanel.teamSlots[i].selectButton.enabled = true;
        }
    }

    // called when the player has selected a team member
    public void ConfirmedWhichTeamMember(int i)
    {
        // Add the Team Member to the Friend List
        //friendList.AddNPC(teamPanel.teamSlots[i].npcScript);
        friendList.friendSlotRef = friendList.friendSlot.GetComponent<FriendSlot>();
        friendList.friendSlotRef.nickname = teamPanel.teamSlots[i].nickname;
        friendList.friendSlotRef.species = teamPanel.teamSlots[i].species;
        friendList.friendSlotRef.personality = teamPanel.teamSlots[i].personality;
        friendList.friendSlotRef.icon = teamPanel.teamSlots[i].npcIcon.sprite;
        friendList.friendSlotRef.prefab = teamPanel.teamSlots[i].prefab;
        friendList.friendSlotRef.hasNPC = true;
        friendList.friendSlotRef.hasBeenTalkedTo = teamPanel.teamSlots[i].hasBeenTalkedTo;
        Instantiate(friendList.friendSlot, friendList.friendParent.transform);

        // Change the Team Member values to the new values
        teamPanel.teamSlots[i].nickname = currentFriend.nickname;
        teamPanel.teamSlots[i].personality = currentFriend.personality;
        teamPanel.teamSlots[i].prefab = currentFriend.prefab;
        teamPanel.teamSlots[i].species = currentFriend.species;
        teamPanel.teamSlots[i].hasNPC = true;
        teamPanel.teamSlots[i].teamSlotNumber = i;
        teamPanel.teamSlots[i].teamIcon = currentFriend.icon;

        // Delete the Friend from the Friend List
        Destroy(currentFriend.gameObject);

        // Refresh team panel
        teamPanel.RefreshTeam();

        // Turn off selection buttons on the team slots
        for (int j = 0; j < teamPanel.teamSlots.Length; j++)
        {
            teamPanel.teamSlots[j].SelectingATeamMember(false);
            teamPanel.teamSlots[j].selectButton.enabled = false;
        }

        AchievementManager.current.IncreaseAchievement("NewFriendToTeam", 1);
        Debug.Log("after add new rfirned to team");
    }

    /*
    // used at the start of the game 
    public void Equip(_NPC npc)
    {
        if (IsFull())
        {
            Debug.Log("team is full!");
        }
        else
        {
            teamPanel.AddTeamMember(npc);
        }
    }
    

    public void Unequip(_NPC npc)
    {
        if (teamPanel.RemoveTeamMember(npc))
        {
            friendList.AddNPC(npc);
        }
    }

    public bool IsFull()
    {
        return teamPanel.teamMembers.Count >= teamPanel.teamSlots.Length;
    }
    */
    public void AddToFriendList(_NPC npc)
    {
        //&& RhythmManager.instance.songHasEnded == true && !hasAddedFriend)
        if (!hasAddedFriend)
        {
            currentNPC = npc;
            nicknameInput.SetActive(true);
            nicknameInputText.text = "What should their nickname be?";
        }
    }

    public void GetFriends()
    {
        friendList.LoadInFriends();
        Debug.Log("gonna get friends");
    }

    public void SubmitName()
    {
        if (!String.IsNullOrEmpty(inputField.text))
        {
            nickname = inputField.text;
            nicknameConfirmScreen.SetActive(true);
            nicknameConfirmScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                = "Your new friend's nickname is \"" + inputField.text + "\".";
            nicknameInput.SetActive(false);
        }
        else
        {
            Debug.LogError("invalid nickname");
            nicknameInputText.text = "Please enter a nickname!";
        }
    }

    public void ConfirmedNickname()
    {
        nicknameConfirmScreen.SetActive(false);
        inputField.text = "";
        FinishInit(currentNPC);
    }

    public void UnconfirmedNickname()
    {
        nicknameConfirmScreen.SetActive(false);
        nicknameInput.SetActive(true);
        inputField.text = "";
        nicknameInputText.text = "What should their nickname be?";
    }

    private void FinishInit(_NPC npc)
    {
        npc.name = nickname;
        npc.nickname = nickname;
        friendList.AddNPC(npc);
        // track achievement
        Debug.Log("BEFORE increase achievement in inventory finishinit:");
        AchievementManager.current.IncreaseAchievement("5Friends", 1);
        AchievementManager.current.IncreaseAchievement("10Friends", 1);
        Debug.Log("AFTER increase achievement in inventory finishinit: ");
        //AchievementManager.current.IncreaseAchievement(5Friends, 1);
        //friendList.npcIDs.Add(npc.npcID);
        //hasAddedFriend = true;
    }

    public void SpawnTeamMembersRhythm()
    {
        var playerCharacter = Resources.Load("PlayerPrefabs/" + player);
        var playerParent = RhythmManager.instance.rhythmGameItems.transform.Find("Heroes").transform.Find(2.ToString()).transform;
        Instantiate(playerCharacter, playerParent.transform.position, playerParent.transform.rotation, playerParent);
        teamPanel.SpawnTeamMembersRhythm();
    }

    public void AddStarters()
    {
        teamPanel.AddStarterMembers();
    }
    public void SpawnTeamMembersCamp()
    {
        Debug.Log("inventory spawn this npc");
        //friendList.SpawnThisNPC();
        var playerCharacter = Resources.Load("PlayerPrefabs/" + player);
        Instantiate(playerCharacter, new Vector2(-7.77f, -1.66f), Quaternion.identity);
        teamPanel.SpawnTeamMembersCamp();
    }

    public void SpawnPlayerMain()
    {
        // Finds out which level the player last played, gets the position of that level from the Level Select Manager
        //var yeah = LevelSelectManager.current.levels[1].transform.position.x;
        player = PlayerPrefs.GetString("playerCharacter");
        var playerCharacter = Resources.Load("PlayerPrefabs/" + player);
        Debug.Log("player last level " + lastPlayerLevel + " + level managr " + LevelSelectManager.current);
        var position = LevelSelectManager.current.levels[0].transform.position;
        Instantiate(playerCharacter, new Vector2(position.x, position.y), Quaternion.identity);
    }

    public void SpawnPlayerMiniGames()
    {
        player = PlayerPrefs.GetString("playerCharacter");
        var playerCharacter = Resources.Load("PlayerPrefabs/" + player);
        var position = FishingManager.current.playerSpawn.transform.position;
        Instantiate(playerCharacter, new Vector2(position.x, position.y), Quaternion.identity);
    }

    public void NextPage()
    {
        Debug.Log("invntory next page");
        RhythmManager.instance.NextPage();
    }

    public void AnimateResultsUI()
    {

    }

    public void MainScreen()
    {
        //friendList.CheckTalkedToBool();
        SaveFriends();
        SceneManager.LoadScene("MainScreen");
        hasAddedFriend = false;
    }

    public void ToCamp()
    {
        SceneManager.LoadScene("Camp");
        //GlobalControl.Instance.SpawnTeamMembers();
    }

    private void SaveFriends()
    {
        friendList.SaveFriends();
        //teamPanel.SaveTeam();
    }

    public void StartConversation(_NPC npc)
    {
        Debug.Log("inventory dialogue");
        conversationManager.GetComponent<ConversationManager>().SelectConversation(npc);
    }

    public void UpdateGrid()
    {
//        friendList.RefreshGrid();
    }

    public void DoYouWantToSave()
    {
        notificationPanel.SetActive(true);
        notificationText.text = "Do you want to save?";
    }

    public void UpdateSettingsText()
    {
        var sc = Settings.current;
        speedText.text = Settings.current.speed.ToString();
        difficultyText.text = sc.difficultyName[sc.difficulty -1];
        noteColourText.text = sc.noteColourName[sc.noteColour - 1];
        laneDesignText.text = sc.laneDesignName[sc.laneDesign -1];
        musicVolumeSlider.value = sc.musicVolume;
        sfxVolumeSlider.value = sc.sfxVolume;
        if (sc.animationsBool == 1) animationsToggle.isOn = true;
        else if (sc.animationsBool == 0) animationsToggle.isOn = false;

        if (sc.windowedBool == 0) windowedToggle.isOn = false;
        else if (sc.windowedBool == 1) windowedToggle.isOn = true;
    }

    // Removed feature. Minigames will be added later
    /*
    public void GoToMiniGames()
    {
        SceneManager.LoadScene("MiniGames");
    }
    */
}
