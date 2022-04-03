using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RunInkConvo : MonoBehaviour
{
    public TextAsset inkJSON;
    public Story story;

    public TMP_Text textPrefab;
    public Button buttonPrefab;
    public TMP_Text namePrefab;
    public Image image;

    private _NPC currentNPC;
    public bool hasStartedStory;

    public void StartStory(_NPC npc)
    {
        currentNPC = npc;
        story = new Story(inkJSON.text);
        if(story.variablesState["Name"] != null)
        {
            story.variablesState["Name"] = "UnityPlayerName";
        }
        namePrefab.text = npc.name;
        refreshUI();
        StartCoroutine(CameraZoomer());
        npc.gameObject.GetComponent<NPCController>().isActive = false;
        Debug.Log("calling zoomer");
        hasStartedStory = true;
    }

    void refreshUI()
    {
        eraseUI();

        Instantiate(namePrefab, this.transform);
        TMP_Text storyText = Instantiate(textPrefab) as TMP_Text;
        if (story.canContinue)
        {
            storyText.text = loadStoryChunk();
            image.enabled = true;
        }
        storyText.transform.SetParent(this.transform, false);

        foreach (Choice choice in story.currentChoices)
        {
            Button choiceButton = Instantiate(buttonPrefab) as Button;
            choiceButton.transform.SetParent(this.transform, false);

            TMP_Text choiceText = choiceButton.GetComponentInChildren<TMP_Text>();
            choiceText.text = choice.text;

            choiceButton.onClick.AddListener(delegate {
                chooseStoryChoice(choice);
            });
        }
    }

    void eraseUI()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    void chooseStoryChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        story.Continue();
        refreshUI();
    }

    string loadStoryChunk()
    {
        string text = "";
        text = story.Continue();
        return text;
    }

    public void EndStory()
    {
        if (!story.canContinue)
        {
            this.gameObject.SetActive(false);
            StartCoroutine(CameraZoomOut());
            currentNPC.gameObject.GetComponent<NPCController>().isActive = true;
            Debug.Log("story has ended");
            image.enabled = false;
        }
    }

    IEnumerator CameraZoomer()
    {
        Debug.Log("inside enum");
        float time = 2.0f;
        float steps = 120;

        for (float f = 0; f <= 1; f += time / steps)
        {
            Camera.main.orthographicSize = Mathf.Lerp(5, 2, f);

            yield return new WaitForSeconds(time / steps);
        }
    }

    IEnumerator CameraZoomOut()
    {
        Debug.Log("inside enum");
        float time = 2.0f;
        float steps = 120;

        for (float f = 0; f <= 1; f += time / steps)
        {
            Camera.main.orthographicSize = Mathf.Lerp(2, 5, f);

            yield return new WaitForSeconds(time / steps);
        }
    }
}