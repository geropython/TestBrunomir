using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using UnityEngine.SceneManagement;

public class FirstSceneScript : MonoBehaviour
{
    public DialogManager DialogManager;

    private void Awake()
    {
        if (DialogManager == null)
        {
            Debug.LogError("DialogManager is not assigned!");
            return;
        }
        var dialogTexts = new List<DialogData>();

        // Add all dialogs
        AddDialog(dialogTexts, "/emote:Normal/Nathan is standing outside his house, checking her phone, when he notices Samuel, a new neighbor, carrying some boxes.", "Narrador");

        // Initial Talk
        AddDialog(dialogTexts, "/emote:Happy/Hi, my name is Nathan.", "Nathan");
        AddDialog(dialogTexts, "/emote:Happy/Hi, my name is Samuel.", "Samuel");
        AddDialog(dialogTexts, "/emote:Happy/ Hey! Just moved in?", "Nathan");
        AddDialog(dialogTexts, "/emote:Happy/ Yeah! I got here a couple of days ago. Still trying to settle in.", "Samuel");

        // House introduction
        AddDialog(dialogTexts, "/emote:Normal/Welcome to the neighborhood! I live next door", "Nathan");
        AddDialog(dialogTexts, "/emote:Normal/Nice to meet you! I don’t really know my way around here yet.", "Samuel");
        AddDialog(dialogTexts, "/emote:Normal/Don’t worry, it’s a nice place, and people are really friendly. If you need anything, just let me know.", "Nathan");
        AddDialog(dialogTexts, "/emote:Happy/Thanks! It already feels like a great place to live.", "Samuel");

        // House invitation
        AddDialog(dialogTexts, "/emote:Happy/I was just about to make some coffee and snacks. Want to join me? I can tell you more about the neighborhood.", "Nathan");
        AddDialog(dialogTexts, "/emote:Surprise/That sounds great! Maybe you can give me some tips so I don’t get lost.", "Samuel");

        // Narrador
        AddDialog(dialogTexts, "/emote:Normal/They both laugh and head into Nathan's house.", "Narrador");

        // Transition to second scene
        var _secondScene = new DialogData("/emote:Normal/Thank you for accepting my invitation to my house!", "Nathan");
        _secondScene.Callback = () => SecondScene();
        dialogTexts.Add(_secondScene);

        DialogManager.Show(dialogTexts);
    }

    private void AddDialog(List<DialogData> dialogTexts, string message, string character)
    {
        dialogTexts.Add(new DialogData(message, character));
    }

    private void SecondScene() => SceneManager.LoadScene("SecondScene");
}
