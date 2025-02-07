using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Doublsb.Dialog;

public class FirstSceneScript : MonoBehaviour
{
    public DialogManager DialogManager;

    private void Awake()
    {
        var dialogTexts = new List<DialogData>();
        //Narrador
        dialogTexts.Add(new DialogData("/emote:Normal/Nathan is standing outside his house, checking her phone, when he notices Samuel, a new neighbor, carrying some boxes.", "Narrador"));
        //both talks
        dialogTexts.Add(new DialogData("/emote:Happy/Hi, /size:init/my name is Nathan.", "Nathan"));
        dialogTexts.Add(new DialogData("/emote:Happy/Hi, /size:init/my name is Samuel.", "Samuel"));
        
        //both talks
        dialogTexts.Add(new DialogData("/emote:Happy/ Hey! Just moved in?", "Nathan"));
        dialogTexts.Add(new DialogData("/emote:Happy/ Yeah! I got here a couple of days ago. Still trying to settle in.", "Samuel"));
        
        //both talks
        dialogTexts.Add(new DialogData("/emote:Normal/Welcome to the neighborhood! I live next door", "Nathan"));
        dialogTexts.Add(new DialogData("/emote:Normal/Nice to meet you! I don’t really know my way around here yet.", "Samuel"));
        
        //both talks
        dialogTexts.Add(new DialogData("/emote:Normal/Don’t worry, it’s a nice place, and people are really friendly. If you need anything, just let me know.", "Nathan"));
        dialogTexts.Add(new DialogData("/emote:Happy/Thanks! It already feels like a great place to live.", "Samuel"));
        
        //Nathan talks
        dialogTexts.Add(new DialogData("/emote:Happy/I was just about to make some coffee and snacks. Want to join me? I can tell you more about the neighborhood.", "Nathan"));
        dialogTexts.Add(new DialogData("/emote:Sorprise/That sounds great! Maybe you can give me some tips so I don’t get lost.", "Samuel"));
        //Narrador
        dialogTexts.Add(new DialogData("/emote:Normal/They both laugh and head into Nathan's house.", "Narrador"));

        DialogManager.Show(dialogTexts);
        SecondScene();
    }
    private void SecondScene() => SceneManager.LoadScene("SecondScene");
}
