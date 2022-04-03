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

    private void Start()
    {
        confirmSelection.SetActive(false);
        choosePlayerPage.SetActive(true);
        chooseTeamPage.SetActive(false);

        playerInt = 0;
        teamInt = 0;

        playerParent.transform.GetChild(playerInt).position = playerSelection.transform.Find("Image").position;
    }
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

    public void ChoosePlayer()
    {
        if (!String.IsNullOrEmpty(inputField.text))
        {
            playerName = inputField.text;
            playerSpecies = playerParent.transform.GetChild(playerInt).GetComponentInChildren<TextMeshProUGUI>().text;
            confirmSelection.SetActive(true);
            child = confirmSelection.transform.GetChild(0).gameObject;
            child.GetComponent<TextMeshProUGUI>().text = "You're a " + playerSpecies + " called " + "\"" + playerName + "\"?";
        }
        else if (String.IsNullOrEmpty(inputField.text))
        {
            nicknameNullWarning.SetActive(true);
            nicknameNullWarning.GetComponentInChildren<TextMeshProUGUI>().text = "Please enter a Nickname!";
        }
    }

    public void ConfirmPlayer()
    {
        PlayerPrefs.SetString("playerCharacter", playerParent.transform.GetChild(playerInt).name);
        PlayerPrefs.SetString("playerNickname", inputField.text);
        // back
        choosePlayerPage.GetComponent<Animator>().Play("PlayerPage");
        chooseTeamPage.SetActive(true);
        // forward
        chooseTeamPage.GetComponent<Animator>().Play("TeamPageLeft");
        confirmSelection.SetActive(false);
        //choosePlayerPage.SetActive(false);
        teamParent.transform.GetChild(teamInt).position = teamSelection.transform.Find("Image").position;
    }

    public void BackToPlayerPage()
    {
        chooseTeamPage.GetComponent<Animator>().Play("TeamPageRight");
        choosePlayerPage.GetComponent<Animator>().Play("PlayerPageBack");

    }

    public void ChooseTeam()
    {
        if (teamNickname.Count < 4)
        {
            if (!String.IsNullOrEmpty(teamInputField.text))
            {
                teamNickname.Add(teamInputField.text);
                dropdownChild = teamDropDown.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                teamPersonality.Add(dropdownChild.text);
                selectedTeam = teamParent.transform.GetChild(teamInt).gameObject;
                teamPrefabName.Add(selectedTeam.name);
                teamSpecies.Add(teamParent.transform.GetChild(teamInt).GetComponentInChildren<TextMeshProUGUI>().text);
                iconInt.Add(teamInt);

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
               
                teamDropDown.value = 0;
                teamInputField.text = "";
            }
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
        }
        else
        {
            nicknameNullWarning.SetActive(true);
            nicknameNullWarning.GetComponentInChildren<TextMeshProUGUI>().text = "You need Four Friends in your Team!";
        }
        
    }

    public void BackToTeamPage()
    {
        // move out confirm selection
        confirmSelectionPage.GetComponent<Animator>().Play("TeamPageRight");
        // move in team page
        chooseTeamPage.GetComponent<Animator>().Play("PlayerPageBack");
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