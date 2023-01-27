using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManeger : MonoBehaviour
{
    [SerializeField]
    private GameObject standartUI;
    [SerializeField]
    private GameObject sporgeSkema;

    public static GameObject instance;

    // Start is called before the first frame update
    void Awake()
    {
        //dette objekt bliver ikke fjernet n�r en ny scenemaneger
        DontDestroyOnLoad(this.gameObject);

            //hvis der alderade er et canvas destroy dette canvas
            if (GameObject.FindGameObjectsWithTag("GameController").Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }
        else
            instance = this.gameObject;
    }

    private void Start()
    {
        try
        {
            GetComponentInChildren<DataSamling>().SporgPlayer();
        }
        catch { }
    }

    public void MiniGameUI (bool minigame)
    {
        //fjern UI information n�r man g�r ind i minigame
        standartUI.SetActive(minigame);
    }

    //loader en ny scene
    public void NewScene(string scenen)
    {
        SceneManager.LoadScene(scenen);
    }
}
