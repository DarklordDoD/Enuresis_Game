using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSkift : MonoBehaviour
{
    [SerializeField]
    private string skiftSceneTil; //hvilken en scene der skal loades
    [SerializeField]
    private bool minigame; //om der bliver skiftet til minigame
    [SerializeField]
    private GameObject aktivateUI; //Hvis der skal åbnes en menu
    public bool aktivate;
    [SerializeField]
    private bool stayOpenUI;

    SceneManeger scM;
    private bool aktivateKlik;

    private void Start()
    {
        aktivateKlik = !aktivate;
    }

    //for besked om at den næste sene skal loades
    public void SkiftTil()
    {
        try
        {
            scM = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManeger>();
        }
        catch
        {
            if (aktivateUI == null)
                SceneManager.LoadScene(skiftSceneTil);
            return;
        }

        if (stayOpenUI)
            minigame = scM.standartUI[0].activeSelf;

        try
        {
            if (scM != null)
            {
                GameObject snackMenu = GameObject.Find("Snacks");

                if (snackMenu.GetComponent<SnackMenu>().menuOpen)
                    snackMenu.GetComponent<SnackMenu>().OpenMenu();
            }
        } catch { }

        try
        {
            aktivateUI.GetComponent<KledeSkab>().PlacePet();
        }
        catch { }

        if (aktivateUI == null)
        {
            scM.NewScene(skiftSceneTil);
        }
        else
        {
            aktivateUI.SetActive(aktivate);

            foreach (GameObject go in GameObject.FindGameObjectsWithTag("SceneSkift"))
                go.GetComponent<Collider2D>().enabled = aktivateKlik;
        }

        scM.MiniGameUI(minigame);
    }

    public void testKledeskab()
    {        
        if (GameObject.Find("Omkledning") != null || GameObject.FindGameObjectWithTag("Tutorial") != null)
            aktivateKlik = false;
        else
            aktivateKlik = true;

        SkiftTil();
    }
}
