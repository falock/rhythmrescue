using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    public static LevelSelectManager current;
    public List<GameObject> levels = new List<GameObject>();
    private List<Vector3> levelFloat = new List<Vector3>();
    public int lastLevel;
    public int currentPlayerLevel;
    public GameObject player;

    // Start is called before the first frame update
    void Awake()
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
        for (int i = 0; i < levels.Count; i++)
        {
            levelFloat.Add(levels[i].transform.position);
        }
        lastLevel = levelFloat.Count -1;
        player = GameObject.FindWithTag("Player");

    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("entered");
            EnterLevel();
        }
    }

    public void CheckMovementLeft(out bool canMove, out Vector3 newPosition)
    {
        //const intersection = levels.filter(element => array2.includes(player.transform.position.x));
        for (int i = 0; i < levels.Count; i++)
        {
            if (levelFloat[i] == player.transform.position)
            {
                currentPlayerLevel = i;
                break; //don't need to check the remaining ones now that we found one
            }
        }

        if (player.transform.position == levelFloat[0])
        {
            // eventually add function to change scene to the Woodland
            canMove = false;
            newPosition = player.transform.position;
        }
        else
        {
            canMove = true;
            newPosition = levelFloat[currentPlayerLevel - 1]; 
        }
    }

    public void CheckMovementRight(out bool canMove, out Vector3 newPosition)
    {
        //const intersection = levels.filter(element => array2.includes(player.transform.position.x));
        for (int i = 0; i < levels.Count; i++)
        {
            if (levelFloat[i] == player.transform.position)
            {
                currentPlayerLevel = i;
                break; //don't need to check the remaining ones now that we found one
            }
        }
        
        if (player.transform.position == levelFloat[lastLevel])
        {
            // eventually add function to change scene to the Woodland
            canMove = false;
            newPosition = player.transform.position;
        }
        else
        {
            canMove = true;
            newPosition = levelFloat[currentPlayerLevel +1];
        }
    }

    void EnterLevel()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            if (levelFloat[i] == player.transform.position)
            {
                currentPlayerLevel = i;
                break; //don't need to check the remaining ones now that we found one
            }
        }
        //SceneManager.LoadScene(currentPlayerLevel);
        SceneManager.LoadScene("Level_1");
    }

    public void StartCoroutine(int buttonNumber)
    {
        if (buttonNumber == currentPlayerLevel + 1 || buttonNumber == currentPlayerLevel - 1)
        {
            player.GetComponent<PlayerController>().PlayCoroutine(levels[buttonNumber].transform.position, buttonNumber);
        }
    }
}
