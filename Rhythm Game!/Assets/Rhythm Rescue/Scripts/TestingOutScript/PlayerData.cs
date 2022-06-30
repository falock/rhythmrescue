using System.Collections.Generic;
using UnityEngine;

/*
public class PlayerData1
    
{
    
    ///...
    public List<MovedPlaformData> movablePlatformsToMove; // GUIDs and positions of movable platforms in the level to move on level load

    [System.Serializable]
    public struct MovedPlaformData
    {
        [SerializeField]
        public string guid;

        [SerializeField]
        public SerializedVector3 pos;

        public MovedPlatformData(string g, Vector3 p)
        {
            this.guid = g;
            this.pos = new SerializedVector3(p);
        }
    }

    public PlayerData1(GameManager gameManager)
    {
        ///...
        movedPlatformsToMove = new List();

        //loop through the list of dropped tiles and add their ID and position to the datafile
        foreach (PlatformController platform in gameManager.levelController.movedPlatforms)
        {
            movedPlatformsToMove.Add(new MovedPlatformData(platform.GetComponent<PersistentObject>().guid, platform.transform.position));
        }
    }
    

}
*/