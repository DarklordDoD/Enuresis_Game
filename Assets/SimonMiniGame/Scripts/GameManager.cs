using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public SpriteRenderer[] colours;
    public AudioSource[] buttonSounds;

    private int colourSelect;

    public float stayLit;
    private float stayLitCounter;

    public float WaitBetweenLights;
    private float WaitBetweenCounter;

    private bool shouldBeLit;
    private bool shouldBeDark;

    public List<int> activeSequence;
    private int positionInSequence;

    private bool gameActive;
    private int inputInSequence;

    public AudioSource correct;
    public AudioSource incorrect;

    [SerializeField]
    private Color pushButten;

    // Update is called once per frame
    void Update()
    {
        if (shouldBeLit)
        {
            stayLitCounter -= Time.deltaTime;

            if (stayLitCounter < 0)
            {
                colours[activeSequence[positionInSequence]].color = new Color(1f, 1f, 1f, 1f);

                //colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);
                buttonSounds[activeSequence[positionInSequence]].Stop();
                shouldBeLit = false;

                shouldBeDark = true;
                WaitBetweenCounter = WaitBetweenLights;

                positionInSequence++;
            }
        }

        if(shouldBeDark)
        {
            WaitBetweenCounter -= Time.deltaTime;

            if (positionInSequence >= activeSequence.Count)
            {
                shouldBeDark = false;
                gameActive = true;
            }

            else
            {
                if (WaitBetweenCounter < 0)
                {

                    colours[activeSequence[positionInSequence]].color = pushButten;

                    //colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 0.2f);
                    buttonSounds[activeSequence[positionInSequence]].Play();

                    if (positionInSequence < activeSequence.Count - 3)
                        stayLitCounter = stayLit / 4;
                    else
                        stayLitCounter = stayLit;

                    shouldBeLit = true;
                    shouldBeDark = false;
                }
            }
        }

        
    }


    public void StartGame()
    {
        GameObject.Find("StartGameButton").SetActive(false);

        activeSequence.Clear();

        positionInSequence = 0;
        inputInSequence = 0;

        colourSelect = Random.Range(0, colours.Length);

        activeSequence.Add(colourSelect);

        colours[activeSequence[positionInSequence]].color = pushButten;
        //colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 0.5f);
        buttonSounds[activeSequence[positionInSequence]].Play();

        stayLitCounter = stayLit;
        shouldBeLit = true;
    }

    public void ColourPressed(int whichButton)
    {

        if (gameActive)
        {


            if (activeSequence[inputInSequence] == whichButton)
            {
                //Debug.Log("Correct");
                

                inputInSequence++;

                if(inputInSequence >= activeSequence.Count)
                {
                    GetComponent<ScoreManager>().AddPoint();

                    positionInSequence = 0;
                    inputInSequence = 0;

                    colourSelect = Random.Range(0, colours.Length);

                    activeSequence.Add(colourSelect);

                    colours[activeSequence[positionInSequence]].color = pushButten;
                    //colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 0.5f);
                    buttonSounds[activeSequence[positionInSequence]].Play();

                    if (positionInSequence < activeSequence.Count - 3)
                        stayLitCounter = stayLit / 4;
                    else
                        stayLitCounter = stayLit;

                    shouldBeLit = true;

                    gameActive = false;

                    correct.Play();

                }
            }

            else
            {
                //Debug.Log("Wrong");
                
                gameActive = false;
                SceneManager.LoadScene("GameOver_Simon");
                
            }
        }
    }
}