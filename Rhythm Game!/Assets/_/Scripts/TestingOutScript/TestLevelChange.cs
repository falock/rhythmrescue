using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TestLevelChange : MonoBehaviour
{
    public void LoadScene2()
    {
        SceneManager.LoadScene("Test Scene 2");
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene("Test Scene 1");
    }
}
