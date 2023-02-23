using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManeger : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> standartUI;
    [SerializeField]
    private List<GameObject> sporgeSkema;

    [Header("Music")]
    [SerializeField]
    private List <MusicAndScene> musicForScene;
    private MusicManager musicManager;

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

        MusicForScene("Stue");
    }

    public void MiniGameUI (bool minigame)
    {
        //fjern UI information når man går ind i minigame
        foreach (GameObject UIElement in standartUI)
        {
            UIElement.SetActive(minigame);
        }
        
        thePet.SetActive(minigame);
    }

    //loader en ny scene
    public void NewScene(string scenen)
    {
        SceneManager.LoadScene(scenen);
        MusicForScene(scenen);
    }

    private void MusicForScene(string scene)
    {
        musicManager = GetComponent<MusicManager>();

        foreach (var nuScene in musicForScene)
        {
            if (nuScene.sceneMedMusic.ToString() == scene)
            {
                try {
                    musicManager.PlayMusicWithFade(nuScene.music, nuScene.overgange);
                }
                catch { }
            }
        }
    } 
}
