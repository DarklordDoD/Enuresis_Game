using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManeger : MonoBehaviour
{
    [SerializeField]
    private GameObject standartUI; 

    public static GameObject instance;
    private int iS;

    // Start is called before the first frame update
    void Awake()
    {
        //dette objekt bliver ikke fjernet n�r en ny scenemaneger
        DontDestroyOnLoad(this.gameObject);

        //tjekker hvor mange canvas der er
        iS = GameObject.FindObjectsOfType<Canvas>().Length;

            //hvis der alderade er et canvas destroy dette canvas
            if (iS > 1)
        {
            Destroy(this.gameObject);
            return;
        }
        else
            instance = this.gameObject;

    }

    //loader en ny scene
    public void NewScene(string scenen, bool minigame)
    {
        SceneManager.LoadScene(scenen);

        //fjern UI information n�r man g�r ind i minigame
        if (minigame)
            standartUI.SetActive(false);

        else
            standartUI.SetActive(true);
    }

}
