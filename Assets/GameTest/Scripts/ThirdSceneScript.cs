using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Doublsb.Dialog;
using Unity.VisualScripting;

public class ThirdSceneScript : MonoBehaviour
{
    public DialogManager DialogManager;
    [SerializeField] private GameObject _panel, _inside, _outSide;

    private void Awake()
    {
        Debug.Log("Awake method started");

        if (DialogManager == null)
        {
            Debug.LogError("DialogManager is not assigned in the Inspector!");
            return;
        }

        if (_panel == null || _inside == null || _outSide == null)
        {
            Debug.LogError("One or more GameObjects (_panel, _inside, _outSide) are not assigned in the Inspector!");
            return;
        }

        var dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData("/emote:Normal/Hi, /size:init/Night came and Samuel realized it.", "Narrator"));

        dialogTexts.Add(new DialogData("/emote:Nervous/Hi, /size:init/Oh look at the time. It's too late, my mother is going to worry.I should be leaving.", "Samuel"));
        dialogTexts.Add(new DialogData("/emote:Sad/Hi, /size:init/Yes, no problem. My parents should be arriving right now..", "Nathan"));
        
        //Change background
        var goOutside = new DialogData("/emote:Normal/You hear the car arrive at the garage.", "Narrador");
        goOutside.Callback = () => ChangeOutside();
        dialogTexts.Add(goOutside); 
        dialogTexts.Add(new DialogData("/emote:Normal/Nathan's parents enter the house and see Samuel", "Narrador"));

        //Change background
        var goInside = new DialogData("/emote:Normal/You hear the car arrive at the garage.", "Narrador");
        goInside.Callback = () => ChangeInside();
        dialogTexts.Add(goInside);
        dialogTexts.Add(new DialogData("/emote:Normal/Nathan's parents enter the house and see Samuel", "Narrador"));

        dialogTexts.Add(new DialogData("/emote:Surprise/Who are you? and what are you doing in my house?", "Angelica"));
        dialogTexts.Add(new DialogData("/emote:Confident/Oh, it must be from the family that just moved next door to us.", "Luke"));
        dialogTexts.Add(new DialogData("/emote:Happy/This is Samuel, the neighbors' son and my new friend.", "Nathan"));
        dialogTexts.Add(new DialogData("/emote:Happy/Hello new neighbor, we are Angelica and Luke, Nathan's parents.", "Angelica"));

        dialogTexts.Add(new DialogData("/emote:Angry/Nathan, we were talking about leaving things all dirty and not doing your homework?", "Angelica"));
        dialogTexts.Add(new DialogData("/emote:Thinking/....", "Nathan's Dad"));
        dialogTexts.Add(new DialogData("/emote:Angry/Seriously, mom, do you have to bring up that topic right now?", "Nathan"));

        dialogTexts.Add(new DialogData("/emote:Normal/From what I see they had a good time, which is the important thing. Come back whenever you want, we are delighted.", "Luke"));
        dialogTexts.Add(new DialogData("/emote:Normal/Yes, I had a great time. I was able to learn more about the neighborhood thanks to Nathan.", "Samuel"));
        dialogTexts.Add(new DialogData("/emote:Happy/Well, I have to go. They are waiting for me at home. See you Mr. and Mrs. Sommers. See you Nathan.", "Samuel"));
        dialogTexts.Add(new DialogData("/emote:Happy/Well, I have to go. They are waiting for me at home. See you Mr. and Mrs. Sommers. See you Nathan.", "Samuel"));

        Debug.Log("Showing dialog texts");
        DialogManager.Show(dialogTexts);

        var _endPanel = new DialogData("/emote:Normal/End of the novel", "Narrador");
        _endPanel.Callback = () => EndGame();
        dialogTexts.Add(_endPanel);
    }

    public void EndGame()
    {
        Debug.Log("EndGame called");
        _panel.SetActive(true);
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
