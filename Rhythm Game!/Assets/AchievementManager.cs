using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private GameObject achievementParentObject;
    public List<string> achievementNamesList = new List<string>();
    public List<Achievement> achievementList = new List<Achievement>();

    public static AchievementManager current;

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

        // add achievements to lists 
        for (int i = 0; i < achievementParentObject.transform.childCount; i++)
        {
            achievementNamesList.Add(achievementParentObject.transform.GetChild(i).name);
            achievementList.Add(achievementParentObject.transform.GetChild(i).GetComponent<Achievement>());
        }
    }

    // called when player has earned a point towards an achievement
    public void IncreaseAchievement(string name, int amount)
    {
        int index = achievementNamesList.IndexOf(name); // find achievement with string
        Debug.Log("achievementNamesList: " + achievementNamesList.IndexOf(name) + " " + achievementNamesList[index] + " " + achievementList[index]);

        if (index >= 0)
        {
            if(!achievementList[index].isAchieved) // make sure it hasn't been achieved yet
            {
                achievementList[index].IncreaseAchievement(amount);
            }
        }
    }

    /*
    Reference to achievement strings and meanings:
    ReplayLevel     -- player has replayed a level
    NewFriendToTeam -- swap an OG team member out with a new friend -- IMPLEMENTED
    5Friends        -- made 5 friends                               -- IMPLEMENTED
    Ace             -- get an A on a level
    TalkTeam5       -- player has talked to a team member 5 times
    PassAll         -- pass all levels
    10Friends       -- make 10 friends                              -- IMPLEMENTED
    AceAll          -- get an A on all levels


    Possible new achievements:
    GetAcquainted   -- talk to all OG team members at least once
    */
}
