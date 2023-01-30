using UnityEngine;

public class SceneSkift : MonoBehaviour
{
    [SerializeField]
    private string skiftSceneTil; //hvilken en scene der skal loades
    [SerializeField]
    private bool minigame; //om der bliver skiftet til minigame
    [SerializeField]
    private GameObject Aktivate; //Hvis der skal åbnes en menu

    SceneManeger scM;
    GameObject thePet;

    private void Start()
    {
        scM = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneManeger>();
    }

    //for besked om at den næste sene skal loades
    public void SkiftTil()
    {
        if (Aktivate == null)
        {
            scM.NewScene(skiftSceneTil);
            scM.MiniGameUI(minigame);
        }
        else
            Aktivate.SetActive(true);
    }
}
