using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Doublsb.Dialog;

public class SecondSceneScript : MonoBehaviour
{
    public DialogManager DialogManager;
    [SerializeField] private GameObject _inSide, _kitchen, _outSide;

    private string answer1;
    private string answer2;
    private string answer3;

    private void Awake()
    {
        answer1 = "";
        answer2 = "";
        answer3 = "";
        StartDialog();
    }

    private void StartDialog()
    {
        var dialogTexts = new List<DialogData>();
        //Narrador Empieza la historia, comienza afuera de la casa
        dialogTexts.Add(new DialogData("/emote:Normal/Nathan invited Samuel to his house for a snack. Upon entering the door, the atmosphere gave off an air of elegance and warmth.", "Narrador"));

        var goToInside = new DialogData("/emote:Normal/Thank you for accepting my invitation to my house!", "Nathan");
        goToInside.Callback = () => ChangeInside();
        dialogTexts.Add(goToInside);

        //Escena Adentro
        dialogTexts.Add(new DialogData("/emote:Surprise/Your house is beautiful.", "Samuel"));

        dialogTexts.Add(new DialogData("/emote:Normal/Nathan smiled modestly.", "Narrador"));
        
        dialogTexts.Add(new DialogData("/emote:Happy/Thanks Samuel!", "Nathan"));
        dialogTexts.Add(new DialogData("/emote:Normal/I was living in this house almost all my life. Childhood and adolescence.", "Nathan"));
        dialogTexts.Add(new DialogData("/emote:Normal/I live with my mother and father.", "Nathan"));
        
        dialogTexts.Add(new DialogData("/emote:Normal/Suddenly a noise is heard, it's Samuel's stomach.", "Narrador"));

        dialogTexts.Add(new DialogData("/emote:Happy/You must be hungry. I imagine you haven't eaten anything since you unpacked your things.", "Nathan"));
        dialogTexts.Add(new DialogData("/emote:Happy/I'm going to prepare something to accompany the snack, come with me.", "Nathan"));

        //Cambia la escena a la cocina
        var goToKitchen = new DialogData("/emote:Happy/I'm going to prepare something to accompany the snack, come with me.", "Nathan");
        goToKitchen.Callback = () => ChangeKitchen();
        dialogTexts.Add(goToKitchen);

        //Minijuego
        var TextWithMiniJuego = new DialogData("/emote:Sad/Oh no, I can't find the sugar, the cups and the spoons. Can you help me?", "Nathan");
        TextWithMiniJuego.Callback = () => StartMiniJuego();
        dialogTexts.Add(TextWithMiniJuego);

        DialogManager.Show(dialogTexts);
    }

    void ChangeInside() {
        _inSide.SetActive(true);
        _outSide.SetActive(false);
        _kitchen.SetActive(false);
    }

    void ChangeOutSide()
    {
        _outSide.SetActive(true);
        _inSide.SetActive(false);
        _kitchen.SetActive(false);
    }

    void ChangeKitchen()
    {
        _kitchen.SetActive(true);
        _inSide.SetActive(false);
        _outSide.SetActive(false);
    }

    private void StartMiniJuego()
    {
        var dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData("/emote:Normal/You start looking for the things that are missing","Narrador"));

        DialogManager.Show(dialogTexts);
    }

    private void AskQuestions()
    {
        var dialogTexts = new List<DialogData>();

        var question1 = new DialogData("Can you tell me where the Sugar is if you see it from where you are?");
        question1.SelectList.Add("1", "On the right shelf is Sugar.(Sugar).");
        question1.SelectList.Add("2", "On the second shelf with a blue lid.(Salt)");
        question1.SelectList.Add("3", "I don't see it from here.");
        question1.Callback = () => CheckAnswer(1);

        dialogTexts.Add(question1);

        var question2 = new DialogData("Now I need help finding the cups. Can you see where the cups are?");
        question2.SelectList.Add("1", "On the first middle shelf below the glasses. (Normal Cups)");
        question2.SelectList.Add("2", "On the third middle shelf above the glasses.(Broken Cups)");
        question2.SelectList.Add("3", "From here I can't see anything.(Nothing)");
        question2.Callback = () => CheckAnswer(2);

        dialogTexts.Add(question2);

        var question3 = new DialogData("Finally the spoons. Look for them while I prepare the coffees.");
        question3.SelectList.Add("1", "First drawer, next to the forks.");
        question3.SelectList.Add("2", "The spoons in the sink. (they are dirty)");
        question3.SelectList.Add("3", "I didn't find them.");
        question3.Callback = () => CheckAnswer(3);

        dialogTexts.Add(question3);

        DialogManager.Show(dialogTexts);
    }

    //verificamos que se guarden las respuestas
    private void CheckAnswer(int questionNumber)
    {
        string result = DialogManager.Result;

        if (questionNumber == 1)
        {
            answer1 = result;
            DialogManager.Show(new DialogData("/emote:Normal/Saved answer.", "Narrador"));
        }
        else if (questionNumber == 2)
        {
            answer2 = result;
            DialogManager.Show(new DialogData("/emote:Normal/Saved answer.", "Narrador"));
        }
        else if (questionNumber == 3)
        {
            answer3 = result;
            DialogManager.Show(new DialogData("/emote:Normal/Saved answer.", "Narrador"));
        }

        if (AllQuestionsAnswered())
        {
            ContinueStory();
        }
    }

    private bool AllQuestionsAnswered() { return !string.IsNullOrEmpty(answer1) && !string.IsNullOrEmpty(answer2) && !string.IsNullOrEmpty(answer3);}

    private void ContinueStory()
    {
        var dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData("/emote:Normal/Based on your decisions in the minigame, Nathan answers...", "Narrador"));

        //Correctas: 1,1,1
        if (answer1 == "1" && answer2 == "1" && answer3 == "1") { dialogTexts.Add(new DialogData("/emote:Happy/Well done, even though I live here I don't know where things are XD.", "Nathan")); }
        //Malas: 2,2,2
        else if (answer1 == "2" && answer2 == "2" && answer3 == "2") { dialogTexts.Add(new DialogData("/emote:Ungry/What the hell? Are you playing a joke on me? Next time I will take revenge!", "Nathan")); }
        //Indiferentes: 3,3,3
        else if (answer1 == "3" && answer2 == "3" && answer3 == "3") { dialogTexts.Add(new DialogData("/emote:Sad/Well, you don't even know my house. I couldn't expect anything from you.", "Nathan")); }
  
        //Opcional 1.1: 1,1,3
        else if (answer1 == "1" && answer2 == "1" && answer3 == "3") { dialogTexts.Add(new DialogData("/emote:Normal/Not everything is possible in life. Well anyway, thank you.", "Nathan")); }
        //Opcional 1.2: 1,2,3
        else if (answer1 == "1" && answer2 == "2" && answer3 == "3") { dialogTexts.Add(new DialogData("/emote:Sad/And now how do I stir the coffee?", "Nathan")); }
        //Opcional 1.3: 1,3,2
        else if (answer1 == "1" && answer2 == "3" && answer3 == "2") { dialogTexts.Add(new DialogData("/emote:Sad/And now where do I drink my coffee?", "Nathan")); }
        //Opcional 1.4: 1,2,2
        else if (answer1 == "1" && answer2 == "2" && answer3 == "2") { dialogTexts.Add(new DialogData("/emote:Ungry/Disgusting! Couldn't you have grabbed something clean?", "Nathan")); }
        //Opcional 1.5: 1,3,3
        else if (answer1 == "1" && answer2 == "3" && answer3 == "3") { dialogTexts.Add(new DialogData("/emote:Sad/Only sugar is of no use to me to drink coffee.", "Nathan")); }
        //Opcional 1.6: 1,3,1
        else if (answer1 == "1" && answer2 == "3" && answer3 == "1") { dialogTexts.Add(new DialogData("/emote:Sad/I need the cup to drink my coffee. What bad luck.", "Nathan")); }
        
        //Opcional 2.1: 2,1,3
        else if (answer1 == "2" && answer2 == "1" && answer3 == "3") { dialogTexts.Add(new DialogData("/emote:Sad/Well, I stir with my finger. Yuck! This is salty.", "Nathan")); }
        //Opcional 2.2: 2,2,3
        else if (answer1 == "2" && answer2 == "2" && answer3 == "3") { dialogTexts.Add(new DialogData("/emote:Ungry/Well, I stir with my finger. Damn! My cup broke.", "Nathan")); }
        //Opcional 2.3: 2,2,1
        else if (answer1 == "2" && answer2 == "2" && answer3 == "1") { dialogTexts.Add(new DialogData("/emote:Surprise/Why did I break my cup with the spoon instantly?", "Nathan")); }
        //Opcional 2.4: 2,3,1
        else if (answer1 == "2" && answer2 == "3" && answer3 == "1") { dialogTexts.Add(new DialogData("/emote:Normal/Without a cup I can't drink my coffee, Damn!", "Nathan")); }
        //Opcional 2.5: 2,3,3
        else if (answer1 == "2" && answer2 == "3" && answer3 == "3") { dialogTexts.Add(new DialogData("/emote:Normal/Almost the worst snack I ever had! Curse!", "Nathan")); }
        //Opcional 2.6: 2,1,1
        else if (answer1 == "2" && answer2 == "1" && answer3 == "1") { dialogTexts.Add(new DialogData("/emote:Normal/But how disgusting! This is salty! I almost vomited.", "Nathan")); }
        
        //Opcional 3.1: 3,2,1
        else if (answer1 == "3" && answer2 == "2" && answer3 == "1") { dialogTexts.Add(new DialogData("/emote:Normal/The cup broke! I couldn't take a sip.", "Nathan")); }
        //Opcional 3.2: 3,1,2
        else if (answer1 == "3" && answer2 == "1" && answer3 == "2") { dialogTexts.Add(new DialogData("/emote:Normal/", "Nathan")); }
        //Opcional 3.3: 3,2,2
        else if (answer1 == "3" && answer2 == "2" && answer3 == "2") { dialogTexts.Add(new DialogData("/emote:Normal/", "Nathan")); }
        //Opcional 3.4: 3,1,1
        else if (answer1 == "3" && answer2 == "1" && answer3 == "1") { dialogTexts.Add(new DialogData("/emote:Normal/", "Nathan")); }
        //Opcional 3.5: 3,3,2
        else if (answer1 == "3" && answer2 == "2" && answer3 == "2") { dialogTexts.Add(new DialogData("/emote:Normal/", "Nathan")); }
        //Opcional 3.6: 3,3,1
        else if (answer1 == "3" && answer2 == "1" && answer3 == "1") { dialogTexts.Add(new DialogData("/emote:Normal/", "Nathan")); }
        else{ new DialogData("/emote:Normal/Invalid Answer!"); }

        dialogTexts.Add(new DialogData("/emote:Normal/Great, you have completed the mini-game!","Narrador"));
        dialogTexts.Add(new DialogData("/emote:Normal/Let's move on to the next scene!", "Narrador", ThirdScene));

        DialogManager.Show(dialogTexts);
    }

    private void ThirdScene()
    {
        SceneManager.LoadScene("ThirdScene");
    }
}
