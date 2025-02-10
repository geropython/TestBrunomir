using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class tercerScript : MonoBehaviour
{
    public DialogManager DialogManager;
    [SerializeField] private GameObject _inside, _outSide;

    private void Awake()
    {
         if (DialogManager == null)
        {
            Debug.LogError("DialogManager is not assigned!");
            return;
        }
        var dialogTexts = new List<DialogData>();

        // Add all dialogs
        AddDialog(dialogTexts, "/emote:Nervous/Hi, Oh look at the time. It's too late, my mother is going to worry.I should be leaving.", "Samuel");
        AddDialog(dialogTexts, "/emote:Sad/Yes, no problem. My parents should be arriving right now..", "Nathan");
        
        //Change Background to see the car parking in the garage
        var goOutside = new DialogData("/emote:Normal/They hear the car arrive at the garage.", "Narrador");
        goOutside.Callback = () => ChangeOutside();
        dialogTexts.Add(goOutside); 
        
        //Change background to see the interior house
        var goInside = new DialogData("/emote:Normal/The parents open the principal door.", "Narrador");
        goInside.Callback = () => ChangeInside();
        dialogTexts.Add(goInside);
        //Mom and Samuel
        AddDialog(dialogTexts, "/emote:Normal/Nathan's parents enter the house and see Samuel", "Narrador");
        
        // AddDialog(dialogTexts, "/emote:Surprise/Who are you? and what are you doing in my house?", "Angelica");
        // AddDialog(dialogTexts, "/emote:Confident/emote:think.sound/Oh, it must be from the family that just moved next door to us.", "Luke");
        
        // AddDialog(dialogTexts, "/emote:/emote:Happy/This is Samuel, the neighbors' son and my new friend.", "Nathan");
        // AddDialog(dialogTexts, "/emote:Happy/Hello new neighbor, we are Angelica and Luke, Nathan's parents.", "Angelica");
        // AddDialog(dialogTexts, "/emote:Angry/emote:angry.sound/Nathan, we were talking about leaving things all dirty and not doing your homework?", "Angelica");
        // AddDialog(dialogTexts, "/emote:laugh.sound/emote:Thinking/....", "Nathan's Dad");


        // dialogTexts.Add(new DialogData("/emote:Thinking/....", "Nathan's Dad"));
        // dialogTexts.Add(new DialogData("/emote:Angry/Seriously, mom, do you have to bring up that topic right now?", "Nathan"));

        // dialogTexts.Add(new DialogData("/emote:Normal/From what I see they had a good time, which is the important thing. Come back whenever you want, we are delighted.", "Luke"));
        // dialogTexts.Add(new DialogData("/emote:Normal/Yes, I had a great time. I was able to learn more about the neighborhood thanks to Nathan.", "Samuel"));
        // dialogTexts.Add(new DialogData("/emote:Happy/Well, I have to go. They are waiting for me at home. See you Mr. and Mrs. Sommers. See you Nathan.", "Samuel"));
        // dialogTexts.Add(new DialogData("/emote:Happy/Well, I have to go. They are waiting for me at home. See you Mr. and Mrs. Sommers. See you Nathan.", "Samuel"));

        // Debug.Log("Showing dialog texts");

        // var _endPanel = new DialogData("/emote:Normal/End of the novel", "Narrador");
        // _endPanel.Callback = () => EndGame();
        // dialogTexts.Add(_endPanel);
        DialogManager.Show(dialogTexts);
    }
    private void AddDialog(List<DialogData> dialogTexts, string message, string character)
    {
        dialogTexts.Add(new DialogData(message, character));
    }

    public void ChangeOutside()
    {
        Debug.Log("ChangeOutside called");
        _outSide.SetActive(true);
        _inside.SetActive(false);
    }

    public void ChangeInside()
    {
        Debug.Log("ChangeInside called");
        _outSide.SetActive(false);
        _inside.SetActive(true);
    }
}
