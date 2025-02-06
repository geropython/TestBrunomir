using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Doublsb.Dialog;

public class SecondSceneScript : MonoBehaviour
{
    public DialogManager DialogManager;

    private void Awake()
    {
        var dialogTexts = new List<DialogData>();

        dialogTexts.Add(new DialogData("//Thank you for accepting my invitation to my house!", "Nathan"));

        dialogTexts.Add(new DialogData("//Thank you for accepting my invitation to my house!", "Nathan"));

        dialogTexts.Add(new DialogData("//I doesn't have to be complex nor long, just some short and simple stuff.", "Nathan"));        

        dialogTexts.Add(new DialogData("//Use my sprite, leave me on a side of the screen, make a few animations", "Nathan"));

        dialogTexts.Add(new DialogData("//And depending the actions of the mini-game change mi animation state, move me or do whatever crazy stuff you can think of!", "Nathan"));

        dialogTexts.Add(new DialogData("//After the player finishes the game, make me say something and move to the ThirdScene.", "Nathan"));
        
        DialogManager.Show(dialogTexts);
        // ThirdScene();
    }

    private void ThirdScene() => SceneManager.LoadScene("ThirdScene");
}
