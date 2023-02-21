using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using static UnityEditor.Experimental.GraphView.GraphView;
using Unity.VisualScripting;

public class ScoreManager : MonoBehaviour
{
    //Opsætning til at kunne holde med spillerens score og highscore
    public static ScoreManager instance;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI highscoreText;
    [SerializeField]
    public string hvadGame;

    int score = 0;
    int highscore = 0;
    private List<string> gotList;
    [SerializeField]
    private List<ASnack> allMiniGames;

    private void Awake()
    {
        instance = this;
    }

    //Hvilket kode spilleret skal køre, når det starter
    void Start()
    {
        //Finde spillerens højeste score:
        //highscore = PlayerPrefs.GetInt("highscore", 0);

        //finder spillerens højeste score for selve spillet;
        SaveClass.LoadFromFile("MiniGames", out gotList);

        if (gotList == null)
            gotList = new List<string> { $"{hvadGame},{score}" };

        foreach (string gameScore in gotList)
        {
            List<string> theSplitGames = gameScore.Split(",").ToList();

            allMiniGames.Add(new ASnack { snackType = theSplitGames[0], amaunt = int.Parse(theSplitGames[1]) });
        }

        //setter den gemte score som highscore
        try
        {
            FindScoreOnList(out ASnack theGame);
            highscore = theGame.amaunt;
        }
        catch { }

        scoreText.text = "Score: " + score.ToString();
        highscoreText.text = "Highscore: " + highscore.ToString();
    }

	//Tilføje til spillerens score
    public void AddPoint()
    {
        score += 1;
        scoreText.text = "Score: " + score.ToString();

        //Hvis spillerens score er større end deres tidligere highscore skal scoren skrives som den nye highscore og gemmes
        if (highscore < score)
        {
            FindScoreOnList(out ASnack theGame);
            theGame.amaunt = score;

            SaveScore();
        }

        //PlayerPrefs.SetInt("highscore", score);
    }

    private ASnack FindScoreOnList(out ASnack theGame)
    {
        foreach (ASnack gameScore in allMiniGames)
        {
            if (gameScore.snackType == hvadGame)
            {
                return theGame = gameScore;
            }
        }

        return theGame = null;
    }

    public void SaveScore()
    {
        List<string> saveG = new List<string>();

        foreach (ASnack gameScore in allMiniGames)
        {
            saveG.Add($"{gameScore.snackType},{gameScore.amaunt}");
        }

        SaveClass.WriteToFile("MiniGames", saveG, false);
    }
}
