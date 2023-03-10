using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Ressourcer : MonoBehaviour
{
    [Header("Tis Resurce")]
    [SerializeField]
    private Scrollbar tisShow;

    private float tisMeter;
    private float tisShowMeter;

    [SerializeField]
    private float tisUp;
    [SerializeField]
    private float whenVandUp;
    [SerializeField]
    private float tisVandUp;

    [HideInInspector]
    public bool aktivStain;

    [Header("Vand Resurce")]
    [SerializeField]
    private Scrollbar vandShow;

    private float vandMeter;
    private float vandShowMeter;

    [SerializeField]
    private float vandNed;

    [Header("Glad Resurce")]
    [SerializeField]
    private Scrollbar gladShow;
    [SerializeField]
    private Image gladIkon;
    [SerializeField]
    private List<Sprite> gladIkonList;
    [SerializeField]
    private float whenIkonSkift;

    private float gladMeter;
    private float gladShowMeter;

    [SerializeField]
    private float whenTisNed;
    [SerializeField]
    private float gladTisNed;
    [SerializeField]
    private float whenVandNed;
    [SerializeField]
    private float gladVandNed;

    [Header("Monny")]
    [SerializeField]
    private TextMeshProUGUI monnyShow;
    public int monny;
    private int dalyMonny;
    [SerializeField]
    private int maxDalyMonny;
    [SerializeField]
    private GameObject dalyMonnyDisplay;

    [Header("Andet")]
    [SerializeField]
    private float reduceSpeed;
    private float opdateringsTid;
    [SerializeField]
    private float minBar;
    [SerializeField]
    private float maxBar;
    public DateTime dato;

    [SerializeField]
    private float meterSpeed;

    private List<string> saveR;
    [HideInInspector]
    public List<string> gotList;
    private bool loadSykse; 

    private void Awake()
    {
        //prøver at loade sidste gem
        try
        {
            LoadRecorses();
        }
        catch //(Exception e)
        {
            //Debug.Log($"No save. {e}");
            loadSykse = false; //registretr at der ikke er loadet noget
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //sætte er det gemte data ind i recursce systemet
        if (loadSykse)
        {
            tisMeter = float.Parse(gotList[1]);
            vandMeter = float.Parse(gotList[2]);
            gladMeter = float.Parse(gotList[3]);

            /*finder ud af hvor meget tid der er gådet siden der blev gemt sidst
            og konveter det hele til sekunder, som en int verdi*/
            TimeSpan lastVisit = DateTime.Today + DateTime.Now.TimeOfDay - dato;
            float lastVisitS = float.Parse(lastVisit.Seconds.ToString());   //sekunder
            lastVisitS += float.Parse(lastVisit.Minutes.ToString()) * 60;   //minutter til sekunder
            lastVisitS += float.Parse(lastVisit.Hours.ToString()) * 3600;   //timer til sekunder
            lastVisitS += float.Parse(lastVisit.Days.ToString()) * 86400;   //dage til sekunder

            //samlet tid dividert med hvor ofte recurser endre sig og konvertert til int
            int LastVisitOpdate = Convert.ToInt32(lastVisitS / reduceSpeed);
            //Debug.Log($"{lastVisit} to {lastVisitS}. tirgger {LastVisitOpdate} tims");

            //luper rescurese udregnings systemet intil systemet har cateh op
            for (int i = 0; i < LastVisitOpdate; i++)
            {
                if (tisMeter <= maxBar)
                    TisControl(); //controler tis resurcen. fylder den op over tid

                if (vandMeter >= minBar)
                    VandControl(); //controler tørst resurcen. tømmer den over tid

                if (gladMeter >= minBar)
                    GladControl(); //controler glæde resurce. tømmer den over tid
            }
        }
        else
        {
            //hvis der ikke kunne findes en gemt file set systemet til standart
            tisMeter = minBar;
            vandMeter = maxBar;
            gladMeter = maxBar;
        }

        TjekMinOgMax(tisMeter, out tisMeter);
        TjekMinOgMax(vandMeter, out vandMeter);
        TjekMinOgMax(gladMeter, out gladMeter);

        //set alle de visuelle meter
        tisShow.size = tisMeter / maxBar;
        vandShow.size = vandMeter / maxBar;
        gladShow.size = gladMeter / maxBar;

        if (loadSykse)
            if (DateTime.Today.Day - dato.Day > 0)
            {
                GetComponent<SceneManeger>().NewScene("Bedroom");

                dalyMonny = 0;

                Invoke("Ulykke", 0.1f);
            }
    }

    public void ShowMonny(int newMonny)
    {
        if (dalyMonny < maxDalyMonny)
        {
            dalyMonny += newMonny;

            int forMegetMonny = 0;

            if (dalyMonny >= maxDalyMonny)
            {
                forMegetMonny = dalyMonny - maxDalyMonny;
                dalyMonny = maxDalyMonny;
            }

            monny += newMonny - forMegetMonny;
        }

        dalyMonnyDisplay.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{dalyMonny} / {maxDalyMonny}";

        Instantiate(dalyMonnyDisplay, transform);
    }

    //sørge for at der ikke er nummer der går uden for max eller min value
    private float TjekMinOgMax(float value, out float valueOut)
    {
        valueOut = value;

        if (value > maxBar)
            valueOut = maxBar;

        if (value < 0)
            valueOut = 0;

        return valueOut;
    }

    // Update is called once per frame
    // jeg bruger update til at sende beskeder til andre fungtioner
    void FixedUpdate()
    {
        if (opdateringsTid > reduceSpeed)
        {
            opdateringsTid = 0;

            if (tisMeter < maxBar)
                TisControl(); //controler tis resurcen. fylder den op over tid              

            if (vandMeter >= minBar)
                VandControl(); //controler tørst resurcen. tømmer den over tid

            if (gladMeter >= minBar)
                GladControl(); //controler glæde resurce. tømmer den over tid
        }
        else
        {
            opdateringsTid += 1 * Time.deltaTime;
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddVand(-(maxBar / 2));

            RemuveTis(-(maxBar / 2));

            AddGlad(-(maxBar / 2));
        }

        tisShowMeter = Mathf.Lerp(tisShowMeter, tisMeter, meterSpeed * Time.deltaTime);
        tisShow.size = tisShowMeter / maxBar;

        vandShowMeter = Mathf.Lerp(vandShowMeter, vandMeter, meterSpeed * Time.deltaTime);
        vandShow.size = vandShowMeter / maxBar;

        gladShowMeter = Mathf.Lerp(gladShowMeter, gladMeter, meterSpeed * Time.deltaTime);
        gladShow.size = gladShowMeter / maxBar;

        monnyShow.text = monny.ToString();

        if (opdateringsTid > reduceSpeed)
            SaveRecorses();
    }

    private void OnApplicationQuit()
    {
        SaveRecorses();
    }

    private void Ulykke()
    {
        if (tisMeter >= maxBar)
        {
            RemuveTis(UnityEngine.Random.Range(2000, 5000));

            GameObject.FindGameObjectWithTag("Stain").GetComponent<Ulykke>().HarTisset(true);
        }

        if (GetComponent<Instilinger>().frind)
            GameObject.Find("TaleVindu").GetComponent<FrindSamtale>().Invoke("StartSamtale", 1);
    }

    private void TisControl()
    {
        tisMeter += tisUp; 

        //fylder tis recursen op hurtiger hvis vand recursen er over en bestemt mængde
        if (vandMeter >= whenVandUp)
        {
            tisMeter += tisVandUp;
        }

        //tisShow.size = tisMeter / maxBar; //viser recursen i UI
    }

    private void VandControl()
    {
        vandMeter -= vandNed;

        //vandShow.size = vandMeter / maxBar; //viser recursen i UI
    }

    private void GladControl()
    {
        //tømmer glæde recursen når tørst resurcen er under en bestemt mængde
        if (vandMeter < whenVandNed)
        {
            gladMeter -= gladVandNed;
        }

        //tømmer glæde recursen når tis recursen er over en bestemt mængde
        if (tisMeter > whenTisNed)
        {
            gladMeter -= gladTisNed;
        }

        //gladShow.size = gladMeter / maxBar; //viser recursen i UI

        GladIkonSkift();
    }

    private void GladIkonSkift()
    {
        if (gladMeter < whenIkonSkift)
            gladIkon.sprite = gladIkonList[0];
        else
            gladIkon.sprite = gladIkonList[1];
    }

    //gør det muligt at påvirke tis recursen fra andre scripts
    public void RemuveTis(float amaunt)
    {
        tisMeter -= amaunt;

        if (tisMeter < minBar)
            tisMeter = minBar;

        //tisShow.size = tisMeter / maxBar;
    }

    //gør det muligt at påvirke vand recursen fra andre scripts
    public void AddVand(float amaunt)
    {
        vandMeter += amaunt;

        if (vandMeter > maxBar)
            vandMeter = maxBar;

        //vandShow.size = vandMeter / maxBar;
    }

    //gør det muligt at påvirke glæde recursen fra andre scripts
    public void AddGlad(float amaunt)
    {
        gladMeter += amaunt;

        if (gladMeter > maxBar)
            gladMeter = maxBar;

        //gladShow.size = gladMeter / maxBar;

        GladIkonSkift();
    }

    //alle recurserne bliver sent til save maneger
    public void SaveRecorses()
    {
        dato = DateTime.Today + DateTime.Now.TimeOfDay; //finder dato og tid

        saveR = new List<string>() {"","","","","","",""}; //set list lengde

        //samle alle variabler i en liste
        saveR[0] = dato.ToString("dd-MM-yyyy HH:mm:ss");
        saveR[1] = tisMeter.ToString();
        saveR[2] = vandMeter.ToString();
        saveR[3] = gladMeter.ToString();
        saveR[4] = monny.ToString();
        saveR[5] = aktivStain.ToString();
        saveR[6] = dalyMonny.ToString();

        foreach (ASnack s in GetComponent<Snacks>().snacks)
        {
            saveR.Add($"{s.snackType},{s.amaunt},{s.giveHappy}");
        }

        SaveClass.WriteToFile("MainGame" ,saveR, false); //gem via save system
    }

    //alle recurserne bliver loadet fra save maneger
    private void LoadRecorses()
    {
        SaveClass.LoadFromFile("MainGame", out gotList);

        dato = DateTime.Parse(gotList[0]);
        aktivStain = bool.Parse(gotList[5]);
        monny = int.Parse(gotList[4]);
        dalyMonny = int.Parse(gotList[6]);

        loadSykse = true;

        GetComponent<Snacks>().LoadSnacks();
    }
}