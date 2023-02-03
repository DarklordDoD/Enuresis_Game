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

    [Header("Music")]
    [SerializeField]
    private List <MusicAndScene> musicForScene;
    [SerializeField]
    private MusicManager musicManager;

    public static GameObject instance;
    private GameObject thePet;
    [HideInInspector]
    public int valgtSprog;

    // Start is called before the first frame update
    void Awake()
    {
        //dette objekt bliver ikke fjernet n�r en ny scenemaneger
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

        MusicForScene(SceneManager.GetActiveScene().name);
    }

    public void MiniGameUI (bool minigame)
    {
        //fjern UI information n�r man g�r ind i minigame
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
        MusicForScene(scenen);
    }

    private void MusicForScene(string scene)
    {
        musicManager = GetComponent<MusicManager>();

        foreach (var nuScene in musicForScene)
        {
            if (nuScene.sceneMedMusic.ToString() == scene)
            {
                musicManager.PlayMusic(nuScene.music);
            }
        }
    } 
}
