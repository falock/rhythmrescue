using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeamManager : MonoBehaviour
{
    [Header("Rhythm Team")]
    // Set team members
    private static GameObject teamMembers0;
    private static GameObject teamMembers1;
    private static GameObject teamMembers2;
    private static GameObject teamMembers3;

    public static TeamManager instance;

    public GameObject[] teamMembers = new GameObject[4];

    public NPCSpawner npcSpawner;

    private bool teamMemberPrefabsAssigned = false;
    private bool enemiesNotSpawned = true;
    int xPos = -3;

    private void OnEnable()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        // eventually add this to first scene
        if (sceneName == "Camp")
        {
            npcSpawner.SpawnNPCs();

            teamMembers[0] = GameObject.Find("npc1");
            teamMembers[1] = GameObject.Find("npc2");
            teamMembers[2] = GameObject.Find("npc3");
            teamMembers[3] = GameObject.Find("npc4");


            teamMemberPrefabsAssigned = true;
        }
    }
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
            CheckScene();
    }
   
    public void CheckScene()
    {
        Debug.Log("scene changed!");
        teamMembers[0].gameObject.tag = "Player";
        teamMembers[1].gameObject.tag = "Player";
        teamMembers[2].gameObject.tag = "Player";
        teamMembers[3].gameObject.tag = "Player";

        GameObject.FindObjectOfType<NPCPersonality>().tag = "NPC";

        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "Camp")
        {
            if (!teamMemberPrefabsAssigned)
            {
                teamMembers[0] = GameObject.Find("npc1");
                teamMembers[1] = GameObject.Find("npc2");
                teamMembers[2] = GameObject.Find("npc3");
                teamMembers[3] = GameObject.Find("npc4");

                teamMembers[0].transform.position = new Vector2(-5, 0);
                teamMembers[1].transform.position = new Vector2(5, 0);
                teamMembers[2].transform.position = new Vector2(-4, -3);
                teamMembers[3].transform.position = new Vector2(4, -3);

                teamMemberPrefabsAssigned = true;
            }

            for (int i = 0; i < 4; i++)
            {
                teamMembers[i].gameObject.SetActive(true);
            }
        }
        else if (sceneName == "Level_1")
        {
            if (enemiesNotSpawned)
            {
                FindObjectOfType<NPCSpawner>().SpawnNPCEnemies();
                enemiesNotSpawned = false;

                for (int i = 0; i < 4; i++)
                {
                    teamMembers[i].gameObject.SetActive(true);
                    teamMembers[i].transform.position = new Vector2(xPos, 2.5f);
                    teamMembers[i].transform.rotation = Quaternion.Euler(-60, 0, 0);
                    xPos = xPos + 2;
                }
            }
        }
        else
        {
            // nothing
            for (int i = 0; i < 4; i++)
            {
                teamMembers[i].gameObject.SetActive(false);
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainScreen()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void ToCamp()
    {
        SceneManager.LoadScene("Camp");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level_1");
    }
}
