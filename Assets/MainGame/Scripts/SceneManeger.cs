using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManeger : MonoBehaviour
{
    public List<GameObject> standartUI;
    [SerializeField]
    private List<GameObject> sporgeSkema;

    [Header("Music")]
    [SerializeField]
    private List <MusicAndScene> musicForScene;
    private MusicManager musicManager;

    public static GameObject instance;
    private GameObject thePet;
    private GameObject thePetFrind;
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

        thePet = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
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

        try
        {
            thePet.SetActive(minigame);
        } catch { }


        //Aktiver og deaktiver Frind når man går ind og ud af minigames
        if (thePetFrind == null)
            thePetFrind = GameObject.FindGameObjectWithTag("PetFrind");

        GetComponent<Instilinger>().nuMiniGame = !minigame;

        if (thePetFrind != null)
            thePetFrind.SetActive(minigame);

        if (minigame)
            GetComponent<Instilinger>().AddFrind();
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
