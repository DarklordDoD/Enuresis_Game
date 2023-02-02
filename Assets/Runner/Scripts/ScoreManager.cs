using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
	//Opsætning til at kunne holde med spillerens score og highscore
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    int score = 0;
    int highscore = 0;


    private void Awake()
    {
        instance = this;
    }

	//Hvilket kode spilleret skal køre, når det starter
    void Start ()
    {
		//Finde spillerens højeste score:
        highscore = PlayerPrefs.GetInt("highscore", 0);
		
		//Skrive spillerens score og highscore på valgte tekst elementer i Unity:
        scoreText.text = "SCORE: " + score.ToString();
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }

	//Tilføje til spillerens score
    public void AddPoint()
    {
        score += 1;
        scoreText.text = "SCORE: " + score.ToString();
        
		//Hvis spillerens score er større end deres tidligere highscore skal scoren skrives som den nye highscore
        if (highscore < score)
        PlayerPrefs.SetInt("highscore", score);

    }
}
