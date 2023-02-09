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
    [SerializeField]
    private bool aktivate;

    SceneManeger scM;

    //for besked om at den næste sene skal loades
    public void SkiftTil()
    {
        try
        {
            scM = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManeger>();
        }
        catch
        {
            SceneManager.LoadScene(skiftSceneTil);
        }


        if (aktivateUI == null)
        {
            scM.NewScene(skiftSceneTil);
            scM.MiniGameUI(minigame);           
        }
        else
        {
            aktivateUI.SetActive(aktivate);

            foreach (GameObject go in GameObject.FindGameObjectsWithTag("SceneSkift"))
                go.GetComponent<Collider2D>().enabled = !aktivate;
        }
    }
}
