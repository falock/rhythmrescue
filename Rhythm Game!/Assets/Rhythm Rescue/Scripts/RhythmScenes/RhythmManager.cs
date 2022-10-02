using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class RhythmManager : MonoBehaviour
{
    [Header("Assign")]
    //public TRIALConductor conductor;
    public GameObject conductorObject;
    // The song for this level, attached to the GameManager object, to determine when the results screen shows
    //public AudioSource theMusic;
    public TryingToSpawnPrefab npcSpawner;
    //public event Action recruitNPCOption;

    [Header("NUMBER OF THIS LEVEL")]
    public int levelNumber;

    [Header("Bools")]
    public bool songHasEnded = false;
    // Has the song started playing? 
    public bool startPlaying;
    // Is the song currently playing?
    public bool isPlaying;

    public static RhythmManager instance;

    [Header("Player Info")]
    public int currentScore;
    public float teamHealth = 500;
    [NonSerialized] public float originalTeamHealth = 500;

    [Header("Note Scores")]
    // note scores
    [NonSerialized] public int scorePerNote = 100;
    [NonSerialized] public int scorePerGoodNote = 125;
    [NonSerialized] public int scorePerPerfectNote = 150;
    [NonSerialized] public int scorePerMissedNote = 50;

    // note amount
    public int noteAmount;

    [Header("Results Screen Info")]
    // score stats
    private int totalNotes;
    private int normalHits;
    private int goodHits;
    private int perfectHits;
    private int missedHits;
    private int badHits;
    private float percentHit;

    [Header("Continue Game GameObject")]
    public GameObject rhythmGameItems;
    public GameObject npcs;

    [Header("NPC Enemy Parents")]
    public GameObject sp1;
    public GameObject sp2;
    public GameObject sp3;
    public GameObject sp4;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        InventoryManager.current.scoreText.text = 0.ToString();
        InventoryManager.current.health.GetComponent<Slider>().maxValue = teamHealth;
        InventoryManager.current.health.GetComponent<Slider>().value = teamHealth;
        InventoryManager.current.score.GetComponent<Slider>().value = 0;

        startPlaying = true;
        InventoryManager.current.responseText.text = " ";

       // theMusic = GetComponent<AudioSource>();
        npcSpawner.SpawnNPCEnemies();
        Debug.Log("after spawn NPC Enemies");
        InventoryManager.current.SpawnTeamMembersRhythm();
        rhythmGameItems.SetActive(true);

        // Assigning Results Screen Stuff
    }

    public void UpdateScore()
    {
        InventoryManager.current.score.GetComponent<Slider>().maxValue = noteAmount * scorePerPerfectNote;
        InventoryManager.current.score.GetComponent<Slider>().value = 0;
        InventoryManager.current.scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        totalNotes = FindObjectsOfType<MusicNote>().Length;
        if (!isPlaying && Input.anyKeyDown)
        {
            conductorObject.SetActive(true);
            isPlaying = true;
        }


    }

    public void ShowResults()
    {
        InventoryManager.current.normalsText.text = "Good - " + normalHits;
        InventoryManager.current.goodsText.text = "Great - " + goodHits;
        InventoryManager.current.perfectsText.text = "Perfect - " + perfectHits;
        InventoryManager.current.badsText.text = "Bad - " + badHits;
        InventoryManager.current.missesText.text = "Miss - " + missedHits;

        int totalHits = normalHits + goodHits + perfectHits;
        percentHit = (totalHits / totalNotes * 100f);
        XPManager.instance.IncreaseExperience(currentScore);

        float highestScore = noteAmount * scorePerPerfectNote;
        Debug.Log("note amount = " + noteAmount + ". score per p note = " + scorePerPerfectNote + ". that times together = " + (noteAmount * scorePerPerfectNote) + " " + highestScore);
        InventoryManager.current.playerScoreDisplay.GetComponent<ShowRhythmPower>().rhythmPower = currentScore / highestScore;
        Debug.Log("rhythm power: " + currentScore / highestScore + "current score = " + currentScore);

        InventoryManager.current.percentHitText.text = "Percent Hit: " + percentHit.ToString("F1") + ("%");

        string rankVal = "F";

        if (percentHit > 40)
        {
            rankVal = "D";

            if (percentHit > 55)
            {
                rankVal = "C";

                if (percentHit > 70)
                {
                    rankVal = "B";

                    if (percentHit > 85)
                    {
                        rankVal = "A";

                        if (percentHit > 95)
                        {
                            rankVal = "S";
                        }
                    }
                }
            }
        }

        InventoryManager.current.rankText.text = rankVal;
        InventoryManager.current.finalScoreText.text = currentScore.ToString();
    }
    

    public void NoteHit()
    {
        // currentScore += scorePerNote * currentMultiplier;
        InventoryManager.current.scoreText.text = currentScore.ToString();
        InventoryManager.current.score.GetComponent<Slider>().value = currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote;
        NoteHit();
        InventoryManager.current.responseText.text = "Okay!";
        // normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote;
        NoteHit();
        InventoryManager.current.responseText.text = "Good!";
        goodHits++;
    }

    public void PerfectHit()
    {
        Debug.Log("i'm here!");
        currentScore += scorePerPerfectNote;
        NoteHit();
        InventoryManager.current.responseText.text = "Perfect!";
        perfectHits++;
    }

    public void NoteMissed()
    {
        teamHealth = teamHealth - scorePerMissedNote;
        InventoryManager.current.health.GetComponent<Slider>().value = teamHealth;

        /* TextMeshProUGUI tempTextBox = Instantiate(responseText, new Vector3(332, -121, 0), Quaternion.identity) as TextMeshProUGUI;
        // tempTextBox.transform.SetParent(Canvas.transform, false);
        tempTextBox.transform.SetParent(canvas.transform, false);
        */
        InventoryManager.current.responseText.text = "Missed!";
        missedHits++;
    }

    public void BadHit()
    {
        badHits++;
        InventoryManager.current.responseText.text = "Bad!";
    }

    public void HoldNoteHit()
    {
        
    }

    public void NextPage()
    {
        // if (percentHit > 10 && teamHealth >= 0)
        Debug.Log("here");
        rhythmGameItems.SetActive(false);
        InventoryManager.current.rhythmUI.SetActive(false);
        InventoryManager.current.resultsScreen.SetActive(false);
        InventoryManager.current.recruitScreen.SetActive(true);
        InventoryManager.current.tryAgainScreen.SetActive(false);

        sp1.transform.position = new Vector3(-6.2f, 5, -1.14f);
        sp2.transform.position = new Vector3(-2.1f, 5, -1.14f);
        sp3.transform.position = new Vector3(2.1f, 5, -1.14f);
        sp4.transform.position = new Vector3(6, 5, -1.14f);

        // }
        // else
        // {
        //InventoryManager.current.tryAgainScreen.SetActive(true);
        //}
    }
    
}
