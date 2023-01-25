using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManeger : MonoBehaviour
{
    [SerializeField]
    private GameObject standartUI;
    [SerializeField]
    private GameObject sporgeSkema;

    public static GameObject instance;
    private int iS;

    // Start is called before the first frame update
    void Awake()
    {
        //dette objekt bliver ikke fjernet når en ny scenemaneger
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

    private void Start()
    {
        //if (DateTime.Today.ToString("dd/MM/yyyy") != GetComponent<Ressourcer>().dato.ToString("dd/MM/yyyy"))
            GetComponentInChildren<DataSamling>().SporgPlayer();
        /*else
            Destroy(sporgeSkema);*/
    }

    //loader en ny scene
    public void NewScene(string scenen, bool minigame)
    {
        SceneManager.LoadScene(scenen);

        //fjern UI information når man går ind i minigame
        if (minigame)
            standartUI.SetActive(false);

        else
            standartUI.SetActive(true);
    }

}
