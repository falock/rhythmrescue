using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListHolder : MonoBehaviour
{
    public ListHolder listHolder;
    public FriendsListCanvas friendsListCanvas;

    public List<string> gUIDs;
    public List<string> npcPersonalityList = new List<string>();
    public List<string> npcFavItemList = new List<string>();
    public List<string> npcNameList = new List<string>();
    public List<string> npcSpeciesList = new List<string>();

    private void Awake()
    {
        if (listHolder == null)
        {
            DontDestroyOnLoad(this.gameObject);
            listHolder = this;
        }
        else if (listHolder != this)
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        friendsListCanvas = FindObjectOfType<FriendsListCanvas>();
        friendsListCanvas.member1Name.text = "my favourtie time";
    }

    public void DisplayFriendsList(int number)
    {
        friendsListCanvas.member1Name.text = npcNameList[number];
        friendsListCanvas.member1Species.text = npcSpeciesList[number];
        friendsListCanvas.member1Personality.text = npcPersonalityList[number];
        friendsListCanvas.member1FaveItem.text = npcFavItemList[number];
    }
}


