using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using UnityEngine.SceneManagement;

public class tercerScript : MonoBehaviour
{
    public DialogManager DialogManager;
    [SerializeField] private GameObject  _outSide, _livingRoom, _endPanel;

    private void Awake()
    {
        var dialogTexts = new List<DialogData>();
        AddDialog(dialogTexts, "/emote:Normal//color:red//click//sound:laugh/After coffee, let's go to the living room table.", "Nathan");
        var goLiving = new DialogData("/emote:Normal/They both head to enjoy their coffee in the living room.", "Narrador");
        goLiving.Callback = () => GoLivingRoom();
        dialogTexts.Add(goLiving); 
        //Narrator
        AddDialog(dialogTexts, "/emote:Normal/Nathan and Samuel enjoyed a good coffee and chatted about their lives.", "Narrador");
        AddDialog(dialogTexts, "/emote:Normal/Until it was 8:30 and Samuel remembered that he had to go home.", "Narrador");

        AddDialog(dialogTexts, "/emote:Nervous//color:cyan/Hi, Oh look at the time. It's too late, my mother is going to worry.I should be leaving.", "Samuel");
        AddDialog(dialogTexts, "/emote:Sad//color:red/Yes, no problem. My parents should be arriving right now..", "Nathan");
        
        //Garage
        var goOutside = new DialogData("/emote:Normal/They hear the car arrive at the garage.", "Narrador");
        goOutside.Callback = () => ChangeOutside();
        dialogTexts.Add(goOutside);
        
        //Change Background to see the car parking in the garage
        var goLivingAgain = new DialogData("/emote:Normal//click//sound:ring/The parents open the front door.", "Narrador");
        goLivingAgain.Callback = () => GoLivingRoom();
        dialogTexts.Add(goLivingAgain); 

        //parents arrive 
        AddDialog(dialogTexts, "/emote:Surprise//color:purple//click//sound:surprise/Who are you? And what are you doing in my house?", "Mom");
        AddDialog(dialogTexts, "/emote:Confident//color:orange//click//sound:thinking/Oh, it's the new neighbor's son. How are you young man?", "Dad"); 

        //Show EndPanel
        var FinishGame = new DialogData("/emote:Normal//click//sound:ring/Here the story ends..", "Narrador");
        FinishGame.Callback = () => EndPanel();
        dialogTexts.Add(FinishGame);

        Debug.Log("Showing dialog texts");
        DialogManager.Show(dialogTexts);
    }
    private void AddDialog(List<DialogData> dialogTexts, string message, string character)
    {
        dialogTexts.Add(new DialogData(message, character));
    }
    private void GoLivingRoom()
    {
        _livingRoom.SetActive(true);
        _outSide.SetActive(false);
    }

    private void ChangeOutside()
    {
        Debug.Log("ChangeOutside called");
        _outSide.SetActive(true);
        _livingRoom.SetActive(false);
    }
    private void EndPanel()
    {
        _endPanel.SetActive(true);
    }
    public void FirstScene(){SceneManager.LoadScene("FirstScene");}
    public void SecondScene(){SceneManager.LoadScene("SecondScene");}
    public void ThirdScene(){SceneManager.LoadScene("ThirdScene");}
}
