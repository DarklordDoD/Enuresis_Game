using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;
using UnityEngine.UI;
using System;

public class Ressourcer : MonoBehaviour
{
    [Header("Tis Resurce")]
    [SerializeField]
    private Scrollbar tisShow;
    private float tisMeter;
    [SerializeField]
    private float tisUp;
    [SerializeField]
    private float whenVandUp;
    [SerializeField]
    private float tisVandUp;

    [Header("Vand Resurce")]
    [SerializeField]
    private Scrollbar vandShow;
    private float vandMeter;
    [SerializeField]
    private float vandNed;

    [Header("Glad Resurce")]
    [SerializeField]
    private Scrollbar gladShow;
    private float gladMeter;
    [SerializeField]
    private float whenTisNed;
    [SerializeField]
    private float gladTisNed;
    [SerializeField]
    private float whenVandNed;
    [SerializeField]
    private float gladVandNed;

    [Header("Andet")]
    [SerializeField]
    private float reduceSpeed;
    private float opdateringsTid;
    [SerializeField]
    private float minBar;
    [SerializeField]
    private float maxBar;
    public DateTime dato;

    private List<string> saveR;
    private List<string> gotList;
    private bool loadSykse;

    private void Awake()
    {
        try
        {
            LoadRecorses();
        }
        catch (Exception e)
        {
            Debug.Log($"No save. {e}");
            loadSykse = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (loadSykse)
        {
            tisMeter = float.Parse(gotList[1]);
            vandMeter = float.Parse(gotList[2]);
            gladMeter = float.Parse(gotList[3]);

            TimeSpan lastVisit = DateTime.Today + DateTime.Now.TimeOfDay - dato;
            float lastVisitS = float.Parse(lastVisit.Seconds.ToString());
            lastVisitS += float.Parse(lastVisit.Minutes.ToString()) * 60;
            lastVisitS += float.Parse(lastVisit.Hours.ToString()) * 3600;
            lastVisitS += float.Parse(lastVisit.Days.ToString()) * 86400;
            int LastVisitOpdate = Convert.ToInt32(lastVisitS / reduceSpeed);
            Debug.Log($"{lastVisit} to {lastVisitS}. tirgger {LastVisitOpdate} tims");

            for (int i = 0; i < LastVisitOpdate; i++)
            {
                if (tisMeter < maxBar)
                    TisControl(); //controler tis resurcen. fylder den op over tid

                if (vandMeter > minBar)
                    VandControl(); //controler tørst resurcen. tømmer den over tid

                if (gladMeter > minBar)
                    GladControl(); //controler glæde resurce. tømmer den over tid
            }
        }
        else
        {
            tisMeter = minBar;
            vandMeter = maxBar;
            gladMeter = maxBar;

            tisShow.size = tisMeter / maxBar;
            vandShow.size = vandMeter / maxBar;
            gladShow.size = gladMeter / maxBar;
        }    
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

            if (vandMeter > minBar)
                VandControl(); //controler tørst resurcen. tømmer den over tid

            if (gladMeter > minBar)
                GladControl(); //controler glæde resurce. tømmer den over tid

            SaveRecorses();
        }
        else
        {
            opdateringsTid += 1 * Time.deltaTime;
        }
    }

    private void TisControl()
    {
        tisMeter += tisUp; 

        //fylder tis recursen op hurtiger hvis vand recursen er over en bestemt mængde
        if (vandMeter >= whenVandUp)
        {
            tisMeter += tisVandUp;
        }

        tisShow.size = tisMeter / maxBar; //viser recursen i UI
    }

    private void VandControl()
    {
        vandMeter -= vandNed;

        vandShow.size = vandMeter / maxBar; //viser recursen i UI
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

        gladShow.size = gladMeter / maxBar; //viser recursen i UI
    }

    //gør det muligt at påvirke tis recursen fra andre scripts
    public void RemuveTis(float amaunt)
    {
        tisMeter -= amaunt;

        if (tisMeter < minBar)
            tisMeter = minBar;

        tisShow.size = tisMeter / maxBar;
    }

    //gør det muligt at påvirke vand recursen fra andre scripts
    public void AddVand(float amaunt)
    {
        vandMeter += amaunt;

        if (vandMeter > maxBar)
            vandMeter = maxBar;

        vandShow.size = vandMeter / maxBar;
    }

    //gør det muligt at påvirke glæde recursen fra andre scripts
    public void AddGlad(float amaunt)
    {
        gladMeter += amaunt;

        if (gladMeter > maxBar)
            gladMeter = maxBar;

        gladShow.size = gladMeter / maxBar;
    }

    //alle recurserne bliver sent til save maneger
    public void SaveRecorses()
    {
        dato = DateTime.Today + DateTime.Now.TimeOfDay;

        saveR = new List<string>() {"","","",""};

        saveR[0] = dato.ToString("dd/MM/yyyy HH:mm:ss");
        saveR[1] = tisMeter.ToString();
        saveR[2] = vandMeter.ToString();
        saveR[3] = gladMeter.ToString();

        SaveClass.WriteToFile("MainGame" ,saveR, false);
    }

    //alle recurserne bliver loadet fra save maneger
    private void LoadRecorses()
    {
        SaveClass.LoadFromFile("MainGame", out gotList);

        dato = DateTime.Parse(gotList[0]);
        
        loadSykse = true;
    }
}