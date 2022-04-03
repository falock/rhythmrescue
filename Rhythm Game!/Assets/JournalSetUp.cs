using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalSetUp : MonoBehaviour
{
    [SerializeField] GameObject teamMember1;
    [SerializeField] GameObject teamMember2;
    [SerializeField] GameObject teamMember3;
    [SerializeField] GameObject teamMember4;

    private TeamManager gameManager;

    private void Start()
    {
        teamMember1 = GameObject.Find("Image1");
        teamMember2 = GameObject.Find("Image2");
        teamMember3 = GameObject.Find("Image3");
        teamMember4 = GameObject.Find("Image4");
    }
    /*
    void OnEnable()
    {
        teamMember1.GetComponent<Image>().sprite = gameManager.teamMembers[0].GetComponent<SpriteRenderer>().sprite;
        teamMember2.GetComponent<Image>().sprite = gameManager.teamMembers[1].GetComponent<SpriteRenderer>().sprite;
        teamMember3.GetComponent<Image>().sprite = gameManager.teamMembers[2].GetComponent<SpriteRenderer>().sprite;
        teamMember4.GetComponent<Image>().sprite = gameManager.teamMembers[3].GetComponent<SpriteRenderer>().sprite;
    }
    */
}
