using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager current;
    public TextAsset inkJSONAsset;
    public Button buttonPrefab;
    public _NPC currentNPC;
    private NPCController npcController;

    [Header("Dialogue UI")]
    [SerializeField] private Image dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Story currentStory;
    [SerializeField] private GameObject namePrefab;
    public GameObject visualCue;

    [Header("Choices UI")]
    [SerializeField] private Button[] choices;
    private TextMeshProUGUI[] choicesText;

    [Header("Personality Texts")]
    [SerializeField] private TextAsset classClown;
    [SerializeField] private TextAsset brave;
    [SerializeField] private TextAsset caring;
    [SerializeField] private TextAsset shy;
    [SerializeField] private TextAsset rebel;

    [SerializeField] public bool dialogueIsPlaying;
    public bool visualCueIsActive = false;

    private void Awake()
    {
        if (current != null)
        {
            Debug.LogWarning("More than one dialogue manager in scene");
        }
        current = this;
    }

    public void SelectConversation(_NPC npc)
    {
        Debug.Log("starting dialogue");
        switch (npc.personality)
        {
            case "Class Clown":
                currentStory = new Story (classClown.text);
                break;
            case "Brave":
                currentStory = new Story(brave.text);
                break;
            case "Caring":
                currentStory = new Story(caring.text);
                break;
            case "Rebel":
                currentStory = new Story(rebel.text);
                break;
            case "Shy":
                currentStory = new Story(shy.text);
                break;
            default:
                currentStory = new Story(classClown.text);
                break;
        }
        if (currentStory.variablesState["Name"] != null)
        {
            currentStory.variablesState["Name"] = "UnityPlayerName";
        }
        namePrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = npc.nickname;
        currentNPC = npc;
        npcController = currentNPC.gameObject.GetComponent<NPCController>();
        EnterDialogue();
        //runInkConvo.StartStory(npc);
        //dialogueManager.StartStory();
    }

    public void TurnOnVisualCue()
    {
        if(visualCue = null)
        {
            visualCue = this.transform.GetChild(0).gameObject;
            visualCueIsActive = true;
            visualCue.SetActive(true);
        }
        else
        {
            visualCueIsActive = true;
            visualCue.SetActive(true);
        }
    }

    public void TurnOffVisualCue()
    {
        if (visualCue = null)
        {
            visualCue = this.transform.GetChild(0).gameObject;
            visualCueIsActive = false;
            visualCue.SetActive(false);
        }
        else
        {
            visualCueIsActive = false;
            visualCue.SetActive(false);
        }
    }

    public static DialogueManager GetCurrent()
    {
        return current;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.enabled = false;

        // Get all choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int i = 0;
        foreach (Button choice in choices)
        {
            choicesText[i] = choice.GetComponentInChildren<TextMeshProUGUI>();
            i++;
        }
    }

    private void Update()
    {
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }

        // handle continuing the next line of dialogue when submit is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }
    }

    public void EnterDialogue()
    {
        dialogueIsPlaying = true;
        dialoguePanel.enabled = true;
        namePrefab.SetActive(true);
        ContinueStory();
        InventoryManager.current.OpenJournal.SetActive(false);
        Debug.Log("Entering dialogue");
    }

    private void ExitDialogueMode()
    {
        npcController.ChooseExpression("neutral");
        dialogueIsPlaying = false;
        dialoguePanel.enabled = false;
        namePrefab.SetActive(false);
        dialogueText.text = "";
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        //InventoryManager.current.OpenJournal.SetActive(true);

        
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();

            List<string> tags = currentStory.currentTags;

            if(tags.Count > 0)
            {
                npcController.ChooseExpression(tags[0]);
            }

            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // make sure UI can support choices
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices than UI support");
        }

        int j = 0;
        // enable and initialise the choices up to the amount of current choices from story
        foreach (Choice choice in currentChoices)
        {
            choices[j].gameObject.SetActive(true);
            choicesText[j].text = j+1 + ". " + choice.text;
            /*choices[j].onClick.AddListener(delegate
            {
                ChooseStoryChoice(choice);
            });
            */

            j++;
        }

        // go through remaining UI supports and hide them
        for (int i = j; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        //StartCoroutine(SelectFirstChoice());

    }

    public void MakeChoice(int choiceIndex)
    {
        Debug.Log(choices[0]);
        Debug.Log(currentStory.currentChoices[0]);
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    /*
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }
    */
}

