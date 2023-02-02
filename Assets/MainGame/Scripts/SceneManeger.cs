using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManeger : MonoBehaviour
{
    [SerializeField]
    private GameObject standartUI;
    [SerializeField]
    private List<GameObject> sporgeSkema;

    public static GameObject instance;
    private GameObject thePet;
    [HideInInspector]
    public int valgtSprog;

    // Start is called before the first frame update
    void Awake()
    {
        //dette objekt bliver ikke fjernet når en ny scenemaneger
        DontDestroyOnLoad(this.gameObject);

        //hvis der alderade er et canvas destroy dette canvas
        if (instance == null)
        {
            instance = this.gameObject;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    private void Start()
    {
        thePet = GameObject.FindGameObjectWithTag("Player");
        Instantiate(sporgeSkema[valgtSprog], GetComponent<Transform>());
    }

    public void MiniGameUI (bool minigame)
    {
        //fjern UI information når man går ind i minigame
        standartUI.SetActive(minigame);
        if (minigame)
            thePet.SetActive(true);
        else
            thePet.SetActive(false);
    }

    //loader en ny scene
    public void NewScene(string scenen)
    {
        SceneManager.LoadScene(scenen);
    }
}
