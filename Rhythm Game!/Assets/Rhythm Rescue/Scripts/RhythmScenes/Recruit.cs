using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Recruit : MonoBehaviour
{
    private GameObject[] recruitDisplay = new GameObject[4];
    private TextMeshProUGUI[] species = new TextMeshProUGUI[4];
    private TextMeshProUGUI[] personality = new TextMeshProUGUI[4];
    private bool hasStarted;
    // Start is called before the first frame update

    private void OnEnable()
    {
        if (hasStarted)
        {
            species[0].text = RhythmManager.instance.sp1.transform.GetChild(0).GetComponent<_NPC>().species.ToString();
            personality[0].text = RhythmManager.instance.sp1.transform.GetChild(0).GetComponent<_NPC>().personality;

            species[1].text = RhythmManager.instance.sp2.transform.GetChild(0).GetComponent<_NPC>().species.ToString();
            personality[1].text = RhythmManager.instance.sp2.transform.GetChild(0).GetComponent<_NPC>().personality;

            species[2].text = RhythmManager.instance.sp3.transform.GetChild(0).GetComponent<_NPC>().species.ToString();
            personality[2].text = RhythmManager.instance.sp3.transform.GetChild(0).GetComponent<_NPC>().personality;

            species[3].text = RhythmManager.instance.sp4.transform.GetChild(0).GetComponent<_NPC>().species.ToString();
            personality[3].text = RhythmManager.instance.sp4.transform.GetChild(0).GetComponent<_NPC>().personality;
        }
    }

    void Start()
    {
        Debug.Log(recruitDisplay.Length);
        for (int i = 0; i < recruitDisplay.Length; i++)
        {
            recruitDisplay[i] = this.transform.GetChild(i).gameObject;
            species[i] = recruitDisplay[i].transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            personality[i] = recruitDisplay[i].transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        }

        species[0].text = RhythmManager.instance.sp1.transform.GetChild(0).GetComponent<_NPC>().species.ToString();
        personality[0].text = RhythmManager.instance.sp1.transform.GetChild(0).GetComponent<_NPC>().personality;

        species[1].text = RhythmManager.instance.sp2.transform.GetChild(0).GetComponent<_NPC>().species.ToString();
        personality[1].text = RhythmManager.instance.sp2.transform.GetChild(0).GetComponent<_NPC>().personality;

        species[2].text = RhythmManager.instance.sp3.transform.GetChild(0).GetComponent<_NPC>().species.ToString();
        personality[2].text = RhythmManager.instance.sp3.transform.GetChild(0).GetComponent<_NPC>().personality;

        species[3].text = RhythmManager.instance.sp4.transform.GetChild(0).GetComponent<_NPC>().species.ToString();
        personality[3].text = RhythmManager.instance.sp4.transform.GetChild(0).GetComponent<_NPC>().personality;

        hasStarted = true;
    }

    public void SelectFriend(int i)
    {
        if(i == 0)
        {
            InventoryManager.current.AddToFriendList(RhythmManager.instance.sp1.transform.GetChild(0).GetComponent<_NPC>());
        }
        else if (i == 1)
        {
            InventoryManager.current.AddToFriendList(RhythmManager.instance.sp2.transform.GetChild(0).GetComponent<_NPC>());
        }
        else if (i == 2)
        {
            InventoryManager.current.AddToFriendList(RhythmManager.instance.sp3.transform.GetChild(0).GetComponent<_NPC>());
        }
        else if (i == 3)
        {
            InventoryManager.current.AddToFriendList(RhythmManager.instance.sp4.transform.GetChild(0).GetComponent<_NPC>());
        }
    }
}
