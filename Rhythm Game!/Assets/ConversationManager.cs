using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    //public static ConversationManager instance;
    private RunInkConvo runInkConvo;
    public Dictionary<TextAsset, string> npcConvos = new Dictionary<TextAsset, string>();
    public TextAsset classClown;
    public TextAsset brave;
    public TextAsset caring;
    public TextAsset shy;
    public TextAsset rebel;
    //public DialogueManager dialogueManager;

    void Start()
    {
        runInkConvo = this.gameObject.GetComponent<RunInkConvo>();
    }

    public void SelectConversation(_NPC npc)
    {
        switch (npc.personality)
        {
            case "Class Clown":
                runInkConvo.inkJSON = classClown;
                break;
            case "Brave":
                runInkConvo.inkJSON = classClown;
                break;
            case "Caring":
                runInkConvo.inkJSON = classClown;
                break;
            case "Rebel":
                runInkConvo.inkJSON = classClown;
                break;
            case "Shy":
                runInkConvo.inkJSON = classClown;
                break;
            default:
                break;
        }

        //runInkConvo.StartStory(npc);
        //dialogueManager.StartStory();
    }

    public void FirstConversation(_NPC npc)
    {
        Debug.Log("first conversation!");
    }
}
