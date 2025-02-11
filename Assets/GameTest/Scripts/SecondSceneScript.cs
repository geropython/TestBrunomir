using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Doublsb.Dialog;

public class SecondSceneScript : MonoBehaviour
{
    public DialogManager DialogManager;
    [SerializeField] private GameObject _inSide, _kitchen, _outSide;

    private string answer1, answer2, answer3;

    private void Start()
    {
        answer1 = ""; answer2 = ""; answer3 = "";
    }
    #region Awake
    private void Awake()
    {
        var dialogTexts = new List<DialogData>();
        //Narrador Empieza la historia, comienza afuera de la casa
        dialogTexts.Add(new DialogData("/emote:Normal/Nathan invited Samuel to his house for a snack. Upon entering the door, the atmosphere gave off an air of elegance and warmth.", "Narrador"));

        var goToInside = new DialogData("/emote:Normal//color:red/Thank you for accepting my invitation to my house!", "Nathan");
        goToInside.Callback = () => ChangeInside();
        dialogTexts.Add(goToInside);
        //Escena Adentro
        dialogTexts.Add(new DialogData("/emote:Surprise//color:cyan//click//sound:surprise/Your house is beautiful.", "Samuel"));
        dialogTexts.Add(new DialogData("/emote:Normal//click//sound:hmm/Nathan smiled modestly.", "Narrador"));
        
        dialogTexts.Add(new DialogData("/emote:Happy//color:red//click//sound:hmm/Thanks Samuel!", "Nathan"));
        dialogTexts.Add(new DialogData("/emote:Normal//color:red//click//sound:thinking/I was living in this house almost all my life. Childhood and adolescence.", "Nathan"));
        dialogTexts.Add(new DialogData("/emote:Normal//color:red//click//sound:sigh/I live with my mother and father.", "Nathan"));
        
        //narrador
        dialogTexts.Add(new DialogData("/emote:Normal/Suddenly a noise is heard, it's Samuel's stomach.", "Narrador"));
        dialogTexts.Add(new DialogData("/emote:Flustered//click//sound:hungry/Ops...", "Samuel"));

        dialogTexts.Add(new DialogData("/emote:Happy//color:red//click//sound:laugh/You must be hungry. I imagine you haven't eaten anything since you unpacked your things.", "Nathan"));
        dialogTexts.Add(new DialogData("/emote:Happy//color:red//click//sound:thinking/I'm going to prepare something to accompany the snack, come with me.", "Nathan"));
        dialogTexts.Add(new DialogData("/emote:Sad//color:cyan//click//sound:sigh/Ok, am going.", "Samuel"));
        
        //Cambia la escena a la cocina
        dialogTexts.Add(new DialogData("/emote:Happy//color:red/I'm going to prepare something to accompany the snack, come with me.", "Nathan"));
        var goToKitchen = new DialogData("/emote:Normal//color:red//click//sound:ring/They moved to the kitchen.", "Narrador");
        goToKitchen.Callback = () => ChangeKitchen();
        dialogTexts.Add(goToKitchen);
        
        //Minijuego
        //Pregunta 1
        var question1 = new DialogData("Can you tell me where the Sugar is if you see it from where you are?");
        question1.SelectList.Add("1", "On the right shelf is Sugar.(Sugar).");
        question1.SelectList.Add("2", "On the second shelf with a blue lid.(Salt)");
        question1.SelectList.Add("3", "I don't see it from here.");
        question1.Callback = () => CheckAnswer(1);
        dialogTexts.Add(question1);
        //Pregunta 2
        var question2 = new DialogData("Now I need help finding the cups. Can you see where the cups are?");
        question2.SelectList.Add("1", "On the first middle shelf below the glasses. (Normal Cups)");
        question2.SelectList.Add("2", "On the third middle shelf above the glasses.(Broken Cups)");
        question2.SelectList.Add("3", "From here I can't see anything.(Nothing)");
        question2.Callback = () => CheckAnswer(2);
        dialogTexts.Add(question2);
        //Pregunta 3
        var question3 = new DialogData("Finally the spoons. Look for them while I prepare the coffees.");
        question3.SelectList.Add("1", "First drawer, next to the forks.");
        question3.SelectList.Add("2", "The spoons in the sink. (they are dirty)");
        question3.SelectList.Add("3", "I didn't find them.");
        question3.Callback = () => CheckAnswer(3);
        dialogTexts.Add(question3);
        
        var _thirdScene = new DialogData("/emote:Normal//color:red//click//sound:laugh/Thank you for accepting my invitation to my house!", "Nathan");
        _thirdScene.Callback = () => ThirdScene();
        
        DialogManager.Show(dialogTexts);
    }
    #endregion

    void ChangeInside() { _inSide.SetActive(true); _outSide.SetActive(false); _kitchen.SetActive(false); }
    void ChangeKitchen(){ _kitchen.SetActive(true); _inSide.SetActive(false); _outSide.SetActive(false); }

    #region CheckAnswer
    private void CheckAnswer(int questionNumber)
    {
       string result = DialogManager.Result;
        Debug.Log("Respuesta del minijuego para la pregunta " + questionNumber + ": " + result);

        if (questionNumber == 1)
        {
            answer1 = result;
            DialogManager.Show(new DialogData("/emote:Normal/Saved answer 1.", "Nathan"));
        }
        else if (questionNumber == 2)
        {
            answer2 = result;
            DialogManager.Show(new DialogData("/emote:Normal/Saved answer 2.", "Nathan"));
        }
        else if (questionNumber == 3)
        {
            answer3 = result;
            DialogManager.Show(new DialogData("/emote:Normal/Saved answer 3.", "Nathan"));
        }

        if (AllQuestionsAnswered())
        {
            Debug.Log("Todas las preguntas han sido respondidas.");
            ContinueStory();
        }
    }
    #endregion

    private bool AllQuestionsAnswered() { return !string.IsNullOrEmpty(answer1) && !string.IsNullOrEmpty(answer2) && !string.IsNullOrEmpty(answer3);}

    #region Continue Story
    private void ContinueStory()
    {
        var dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData("/emote:Normal/Based on your decisions in the minigame, /color:red/Nathan answers...", "Nathan"));

        var responses = new Dictionary<string, string>//TODO: COMPLETE TEXTS
        {
            {"111", "/emote:Happy//color:red//click//sound:hmm/Well done, even though I live here I don't know where things are XD., Nathan"},
            {"112", "/emote:Happy//color:red//click//sound:hmm/, Nathan"},
            {"113", "/emote:Normal//color:red/Not everything is possible in life. Well anyway, thank you., Nathan"},
            {"121", "/emote:Happy//color:red//click//sound:hmm/, Nathan"},
            {"122", "/emote:Angry//color:red/Disgusting! Couldn't you have grabbed something clean?, Nathan"},
            {"123", "/emote:Sad//color:red/And now how do I stir the coffee?, Nathan"},
            {"131", "/emote:Sad//color:red/I need the cup to drink my coffee. What bad luck., Nathan"},
            {"132", "/emote:Sad//color:red/And now where do I drink my coffee?, Nathan"},
            {"133", "/emote:Sad//color:red/Only sugar is of no use to me to drink coffee., Nathan"},
            
            {"211", "/emote:Normal//color:red/But how disgusting! This is salty! I almost vomited., Nathan"},
            {"212", "/emote:Normal//color:red/., Nathan"},
            {"213", "/emote:Sad//color:red/Well, I stir with my finger. Yuck! This is salty., Nathan"},
            {"221", "/emote:Surprise//color:red/Why did I break my cup with the spoon instantly?, Nathan"},
            {"222", "/emote:Angry//color:red//click//sound:/What the hell? Are you playing a joke on me? Next time I will take revenge!, Nathan"},
            {"223", "/emote:Angry//color:red/Well, I stir with my finger. Damn! My cup broke., Nathan"},
            {"231", "/emote:Normal//color:red/Without a cup I can't drink my coffee, Damn!, Nathan"},
            {"232", "/emote:Normal//color:red/, Nathan"},
            {"233", "/emote:Normal//color:red/Almost the worst snack I ever had! Curse!, Nathan"},
            
            {"311", "/emote:Normal//color:red/The coffee is good but it lacks flavor., Nathan"},
            {"312", "/emote:Normal//color:red/Disgusting! These spoons are dirty!, Nathan"},
            {"313", "/emote:Normal//color:red/"},
            {"321", "/emote:Normal//color:red/The cup broke! I couldn't take a sip., Nathan"},
            {"322", "/emote:Normal//color:red/Oops the cup broke! What are those lumps on the spoon?, Nathan"},
            {"323", "/emote:Normal//color:red/, Nathan"},
            {"331", "/emote:Normal//color:red/Just the spoon is of no use to me. What a waste of time!, Nathan"},
            {"332", "/emote:Normal//color:red/Clearly I can't have coffee without anything. Less with the dirty spoon., Nathan"},
            {"333", "/emote:Sad//color:red/Well, you don't even know my house. I couldn't expect anything from you., Nathan"},
        };

        string key = $"{answer1}{answer2}{answer3}";
        Debug.Log("Generated key: " + key);

        if (responses.ContainsKey(key))
        {
            string[] response = responses[key].Split(',');
            dialogTexts.Add(new DialogData(response[0], response[1].Trim()));//TODO: FIX
        }
        else
        {
            dialogTexts.Add(new DialogData("/emote:Normal/Invalid Answer!"));
        }

        dialogTexts.Add(new DialogData("/emote:Normal/Great, you have completed the mini-game!", "Narrador"));
        dialogTexts.Add(new DialogData("/emote:Normal/Let's move on to the next scene!", "Narrador", ThirdScene));

        DialogManager.Show(dialogTexts);
    }
    #endregion

    private void ThirdScene(){ SceneManager.LoadScene("ThirdScene"); }
}
