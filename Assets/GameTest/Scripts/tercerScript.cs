using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class tercerScript : MonoBehaviour
{
    public DialogManager DialogManager;
    [SerializeField] private GameObject  _outSide, _livingRoom;

    private void Awake()
    {
        var dialogTexts = new List<DialogData>();
        AddDialog(dialogTexts, "/emote:Normal//color:red//click//sound:laugh/Terminado el cafe vayamos a la mesa del living.", "Nathan");
        var goLiving = new DialogData("/emote:Normal/Ambos se dirigen a disfrutar de su cafe en el living room.", "Narrador");
        goLiving.Callback = () => GoLivingRoom();
        dialogTexts.Add(goLiving); 

        // Add all dialogs
        AddDialog(dialogTexts, "/emote:Nervous//color:cyan/Hi, Oh look at the time. It's too late, my mother is going to worry.I should be leaving.", "Samuel");
        AddDialog(dialogTexts, "/emote:Sad//color:red/Yes, no problem. My parents should be arriving right now..", "Nathan");
        AddDialog(dialogTexts, "/emote:Nervous//color:cyan/Hi, Oh look at the time. It's too late, my mother is going to worry.I should be leaving.", "Samuel");
        AddDialog(dialogTexts, "/emote:Sad//color:red/Yes, no problem. My parents should be arriving right now..", "Nathan");
        //Mom
        AddDialog(dialogTexts, "/emote:Normal//color:pink/Puchaina", "Mom");//TODO: MOM AND DAD
        //Change Background to see the car parking in the garage
        var goOutside = new DialogData("/emote:Normal/Nathan hear the car arrive at the garage.", "Narrador");
        goOutside.Callback = () => ChangeOutside();
        dialogTexts.Add(goOutside); 
        dialogTexts.Add(new DialogData("/emote:Normal/This is the end of this short history.", "Narrador"));

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
}
