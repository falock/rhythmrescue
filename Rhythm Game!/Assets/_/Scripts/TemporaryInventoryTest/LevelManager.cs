using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public PlayerController thePlayer;
    public int currentCount;


    public int redBerryCount;
    public int blueberryCount;
    public int cottonCount;

    // Use this for initialization
    void Start()
    {

        thePlayer = FindObjectOfType<PlayerController>();

        if (PlayerPrefs.HasKey("RedBerryCount"))
        {
            redBerryCount = PlayerPrefs.GetInt("RedBerryCount");
        }
        if (PlayerPrefs.HasKey("BlueberryCount"))
        {
            blueberryCount = PlayerPrefs.GetInt("BlueberryCount");
        }
        if (PlayerPrefs.HasKey("CottonCount"))
        {
            cottonCount = PlayerPrefs.GetInt("CottonCount");
        }

    }

    void Update()
    {

    }

    public void IncreaseItemCount(int ID, int quantity)
    {

        if (ID == 1)
        {
            redBerryCount = redBerryCount + quantity;
            currentCount = redBerryCount;
            PlayerPrefs.SetInt("RedBerryCount", redBerryCount);

        }
        if (ID == 2)
        {
            blueberryCount = blueberryCount + quantity;
            currentCount = blueberryCount;
            PlayerPrefs.SetInt("BlueberryCount", blueberryCount);
        }
        if (ID == 3)
        {
            cottonCount = cottonCount + quantity;
            currentCount = cottonCount;
            PlayerPrefs.SetInt("CottonCount", cottonCount);
        }


    }
}