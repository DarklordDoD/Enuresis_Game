using UnityEngine;

public class SceneSkift : MonoBehaviour
{
    [SerializeField]
    private string skiftSceneTil; //hvilken en scene der skal loades
    [SerializeField]
    private bool minigame; //om der bliver skiftet til minigame

    SceneManeger scM;

    private void Start()
    {
        scM = FindObjectOfType<Canvas>().GetComponent<SceneManeger>();
    }

    //for besked om at den næste sene skal loades
    public void SkiftTil()
    {
        scM.NewScene(skiftSceneTil, minigame);
    }
}
