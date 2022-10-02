using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerSelection : MonoBehaviour
{
    private GameObject child;
    public int playerInt;
    public int teamInt;
    public List<int> iconInt = new List<int>();

    [Header("Player Selection")]
    public TMP_InputField inputField;
    public GameObject playerParent;
    private string playerName;
    private string playerSpecies;
    public GameObject playerSelection;

    [Header("Team Selection")]
    public TMP_InputField teamInputField;
    public TMP_Dropdown teamDropDown;
    public GameObject teamParent;
    public List<string> teamPersonality;
    public List<string> teamNickname;
    public List<string> teamPrefabName;
    public List<string> teamSpecies;
    public GameObject[] teamIcons;
    public TextMeshProUGUI dropdownChild;
    public GameObject selectedTeam;
    public GameObject teamConfirmButton;

    [Header("Confirm Selection Screen")]
    // w
    public GameObject[] teamMemberPositions;
    public GameObject player;

    [SerializeField] GameObject teamSelection;
    public GameObject nicknameNullWarning;

    [Header("Assign UI Pages")]
    [SerializeField] private GameObject choosePlayerPage;
    [SerializeField] private GameObject chooseTeamPage;
    [SerializeField] private GameObject confirmSelection;
    [SerializeField] private GameObject confirmSelectionPage;

    [Header("Arrays")]
    [SerializeField] private GameObject[] npcs;

    [Header("New Way")]
    [SerializeField] private GameObject[] gamePositions;

    //bools to track what page the player is on
    private bool playerPageIsOpen;
    private bool teamPageIsOpen;

    // ready the scene for player selection
    private void Start()
    {
        confirmSelection.SetActive(false);
        choosePlayerPage.SetActive(true);
        chooseTeamPage.SetActive(false);

        playerInt = 0;
        teamInt = 0;

        playerParent.transform.GetChild(playerInt).position = playerSelection.transform.Find("Image").position;
        playerPageIsOpen = true;
    }

    
    public void Update()
    {
        // check if the team confirm button should be active or not (is this still needed?)
        /*
        if(teamPageIsOpen)
        {
            if(teamNickname.Count < 4)
            {
                teamConfirmButton.SetActive(false);
            }
            else if (teamNickname.Count == 4)
            {
                teamConfirmButton.SetActive(true);
            }
        }
        */

        // if the player presses return, determine what page their on and call the function for that page
        // this is so the player selection process goes quicker, no need to move mouse to each button
        // may be removed for the final game - TBD
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(playerPageIsOpen)
            {
                ChoosePlayer();
            }
            else if (teamPageIsOpen)
            {
                ChooseTeam();
            }
            else if (!playerPageIsOpen && !teamPageIsOpen)
            {
                StartGame();
            }
        }

        // keyboard shortcut - if player presses left arrow it moves character choice left
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (playerPageIsOpen)
            {
                PlayerLeft();
            }
            else if (teamPageIsOpen)
            {
                TeamLeft();
            }
        }

        // keyboard shortcut - if player presses right arrow it moves character choice right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (playerPageIsOpen)
            {
                PlayerRight();
            }
            else if (teamPageIsOpen)
            {
                TeamRight();
            }
        }
    }

    // save all team members, add these to the inventory, and start the game
    public void StartGame()
    {
        for (int i = 0; i < teamNickname.Count; i++)
        {
            PlayerPrefs.SetString("teamNickname" + i, teamNickname[i]);
            PlayerPrefs.SetString("teamPersonality" + i, teamPersonality[i]);
            PlayerPrefs.SetString("teamSpecies" + i, teamSpecies[i]);
            PlayerPrefs.SetString("teamPrefabName" + i, teamPrefabName[i]);
            PlayerPrefs.SetInt("teamIcon" + i, iconInt[i]);
        }
        InventoryManager.current.AddStarters();
        SceneManager.LoadScene("MainScreen");
    }

    // called when player chooses a character. This is to ask to confirm their choice or remind them to enter a nickname
    public void ChoosePlayer()
    {
        // if the nickname text field isn't empty, ask the player to confirm choice
        if (!String.IsNullOrEmpty(inputField.text))
        {
            playerName = inputField.text;
            playerSpecies = playerParent.transform.GetChild(playerInt).GetComponentInChildren<TextMeshProUGUI>().text;
            confirmSelection.SetActive(true);
            child = confirmSelection.transform.GetChild(0).gameObject;
            child.GetComponent<TextMeshProUGUI>().text = "You're a " + playerSpecies + " called " + "\"" + playerName + "\"?";
        }
        // if the nickname text field is empty, ask the player to enter a nickname
        else if (String.IsNullOrEmpty(inputField.text))
        {
            nicknameNullWarning.SetActive(true);
            nicknameNullWarning.GetComponentInChildren<TextMeshProUGUI>().text = "Please enter a Nickname!";
        }
    }

    // save player choice and move onto team selection
    public void ConfirmPlayer()
    {
        // save player character choice and nickname
        PlayerPrefs.SetString("playerCharacter", playerParent.transform.GetChild(playerInt).name);
        PlayerPrefs.SetString("playerNickname", inputField.text);
        // move player page away from screen
        choosePlayerPage.GetComponent<Animator>().Play("PlayerPage");
        chooseTeamPage.SetActive(true);
        // move team page into screen
        chooseTeamPage.GetComponent<Animator>().Play("TeamPageLeft");
        confirmSelection.SetActive(false);
        teamParent.transform.GetChild(teamInt).position = teamSelection.transform.Find("Image").position;
        // keep track of what page the player is on
        playerPageIsOpen = false;
        teamPageIsOpen = true;
    }

    // go back to the player selection page from the team selection page
    public void BackToPlayerPage()
    {
        chooseTeamPage.GetComponent<Animator>().Play("TeamPageRight");
        choosePlayerPage.GetComponent<Animator>().Play("PlayerPageBack");
        playerPageIsOpen = true;
        teamPageIsOpen = false;
    }

    // adds a team member
    public void ChooseTeam()
    {
        // check the player hasn't already chosen 4 team members
        if (teamNickname.Count < 4)
        {
            // if the team member's name text field isn't empty, add their details to team member lists
            if (!String.IsNullOrEmpty(teamInputField.text))
            {
                teamNickname.Add(teamInputField.text);
                dropdownChild = teamDropDown.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                teamPersonality.Add(dropdownChild.text);
                selectedTeam = teamParent.transform.GetChild(teamInt).gameObject;
                teamPrefabName.Add(selectedTeam.name);
                teamSpecies.Add(teamParent.transform.GetChild(teamInt).GetComponentInChildren<TextMeshProUGUI>().text);
                iconInt.Add(teamInt);

                // add the new team member's card to the screen
                for (int i = 0; i < teamIcons.Length; i++)
                {
                    if (teamIcons[i].activeInHierarchy == false)
                    {
                        teamIcons[i].SetActive(true);
                        teamIcons[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = teamNickname[i];
                        teamIcons[i].GetComponentInChildren<TextMeshProUGUI>().text = "\nPersonality: " + teamDropDown.GetComponentInChildren<TextMeshProUGUI>().text +
                            "\nSpecies: " + teamSpecies[i];
                        teamIcons[i].transform.GetChild(2).GetComponent<Image>().sprite = teamParent.transform.GetChild(teamInt).GetComponent<Image>().sprite;
                        teamIcons[i].transform.GetChild(2).GetComponent<Image>().SetNativeSize();
                        break;
                    }
                }

                // reset the team member selection
                teamDropDown.value = 0;
                teamInputField.text = "";
            }
            // if the team member's name text field is empty, remind the player they need to enter a nickname
            else
            {
                nicknameNullWarning.SetActive(true);
                nicknameNullWarning.GetComponentInChildren<TextMeshProUGUI>().text = "Please enter a Nickname!";
            }
        }
    }

    public void ConfirmTeam()
    {
        if (teamNickname.Count == 4)
        {
            // Move out TeamPage
            chooseTeamPage.GetComponent<Animator>().Play("PlayerPage");
            // Move In Confirm Selection Page
            confirmSelectionPage.SetActive(true);
            confirmSelectionPage.GetComponent<Animator>().Play("TeamPageLeft");
            // Set Confirm Selection Page values
            for (int i = 0; i < teamMemberPositions.Length; i++)
            {
                teamMemberPositions[i].GetComponentInChildren<TextMeshProUGUI>().text = "Personality: " + teamPersonality[i] +
                            "\nSpecies: " + teamSpecies[i];
                teamMemberPositions[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = teamNickname[i];
                teamMemberPositions[i].transform.GetChild(1).GetComponent<Image>().sprite = teamParent.transform.Find(teamPrefabName[i]).GetComponent<Image>().sprite;
            }

            player.GetComponentInChildren<TextMeshProUGUI>().text =
                            "\nSpecies: " + playerSpecies;
            player.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = playerName;
            player.transform.GetChild(1).GetComponent<Image>().sprite = playerParent.transform.Find(PlayerPrefs.GetString("playerCharacter")).GetComponent<Image>().sprite;
            teamPageIsOpen = false;
        }
        else
        {
            nicknameNullWarning.SetActive(true);
            nicknameNullWarning.GetComponentInChildren<TextMeshProUGUI>().text = "You need Four Friends in your Team!";
        }
        
    }

    public void BackToTeamPage()
    {
        //chooseTeamPage.SetActive(false);
        // move out confirm selection
        confirmSelectionPage.GetComponent<Animator>().Play("TeamPageRight");
        // move in team page
        chooseTeamPage.GetComponent<Animator>().Play("PlayerPageBack");
        teamPageIsOpen = true;
    }

    public void DeleteTeamMember(int i)
    {
        teamNickname.RemoveAt(i);
        teamPersonality.RemoveAt(i);
        teamPrefabName.RemoveAt(i);
        iconInt.RemoveAt(i);
        teamIcons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
        teamIcons[i].SetActive(false);
    }

    public void PlayerRight()
    {
        playerParent.transform.GetChild(playerInt).position = gamePositions[1].transform.position;
        if (playerInt == teamParent.transform.childCount - 1)
        {
            playerInt = 0;
        }
        else
        {
            playerInt++;
        }

        playerSelection.GetComponentInChildren<TextMeshProUGUI>().text = "Species: " + playerParent.transform.GetChild(playerInt).GetComponentInChildren<TextMeshProUGUI>().text;
        playerParent.transform.GetChild(playerInt).position = playerSelection.transform.Find("Image").position;
    }

    public void PlayerLeft()
    {
        playerParent.transform.GetChild(playerInt).position = gamePositions[1].transform.position;
        if (playerInt == 0)
        {
            playerInt = teamParent.transform.childCount - 1;
        }
        else
        {
            playerInt--;
        }

        playerSelection.GetComponentInChildren<TextMeshProUGUI>().text = "Species: " + playerParent.transform.GetChild(playerInt).GetComponentInChildren<TextMeshProUGUI>().text;
        playerParent.transform.GetChild(playerInt).position = playerSelection.transform.Find("Image").position;
    }

    public void TeamRight()
    {
        teamParent.transform.GetChild(teamInt).position = gamePositions[1].transform.position;

        if (teamInt == teamParent.transform.childCount - 1)
        {
            teamInt = 0;
        }
        else
        {
            teamInt++;
        }

        teamSelection.transform.Find("Species").GetComponent<TextMeshProUGUI>().text = "Species: " + teamParent.transform.GetChild(teamInt).GetComponentInChildren<TextMeshProUGUI>().text;
        teamParent.transform.GetChild(teamInt).position = teamSelection.transform.Find("Image").position;
    }

    public void TeamLeft()
    {
        teamParent.transform.GetChild(teamInt).position = gamePositions[1].transform.position;
        if (teamInt == 0)
        {
            teamInt = teamParent.transform.childCount - 1;
        }
        else
        {
            teamInt--;
        }

        teamSelection.transform.Find("Species").GetComponent<TextMeshProUGUI>().text = "Species: " + teamParent.transform.GetChild(teamInt).GetComponentInChildren<TextMeshProUGUI>().text;
        teamParent.transform.GetChild(teamInt).position = teamSelection.transform.Find("Image").position;
    }
}