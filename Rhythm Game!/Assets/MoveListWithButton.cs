using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveListWithButton : MonoBehaviour
{
    public GameObject[] playerCharacters;
    public int playerCharacter = 0;
    public string playerName;

    public void NextCharacter()
    {
        playerCharacters[playerCharacter].SetActive(false);
        playerCharacter = (playerCharacter + 1) % playerCharacters.Length;
        playerCharacters[playerCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        playerCharacters[playerCharacter].SetActive(false);
        playerCharacter--;
        if (playerCharacter < 0)
        {
            playerCharacter += playerCharacters.Length;
        }
        playerCharacters[playerCharacter].SetActive(true);
    }
}
