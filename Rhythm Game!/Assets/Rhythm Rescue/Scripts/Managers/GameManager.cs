using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Create a temporary reference to the current scene.
    public static GameManager instance;
    public string sceneName;
    public PlayerController playerController;

    void Awake()
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

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log(level);
        Scene currentScene = SceneManager.GetActiveScene();
        // Retrieve the name of this scene.
        sceneName = currentScene.name;
        Debug.Log(sceneName);
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
