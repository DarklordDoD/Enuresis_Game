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
    [SerializeField]
    private float opdateringsTid;
    [SerializeField]
    private float minBar;
    [SerializeField]
    private float maxBar;
    public DateTime dato;

    private void Awake()
    {
        //SaveRecorses();
        LoadRecorses();
    }

    // Start is called before the first frame update
    void Start()
    {
        tisMeter = minBar;
        vandMeter = maxBar;
        gladMeter = maxBar;

        tisShow.size = tisMeter / 100f;
        vandShow.size = vandMeter / 100f;
        gladShow.size = gladMeter / 100f;       
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
                VandControl(); //controler t�rst resurcen. t�mmer den over tid

            if (gladMeter > minBar)
                GladControl(); //controler gl�de resurce. t�mmer den over tid

        }
        else
        {
            opdateringsTid += 1 * Time.deltaTime;
        }
    }

    private void TisControl()
    {
        tisMeter += tisUp; 

        //fylder tis recursen op hurtiger hvis vand recursen er over en bestemt m�ngde
        if (vandMeter >= whenVandUp)
        {
            tisMeter += tisVandUp;
        }

        tisShow.size = tisMeter / 100f; //viser recursen i UI
    }

    private void VandControl()
    {
        vandMeter -= vandNed;

        vandShow.size = vandMeter / 100f; //viser recursen i UI
    }

    private void GladControl()
    {
        //t�mmer gl�de recursen n�r t�rst resurcen er under en bestemt m�ngde
        if (vandMeter < whenVandNed)
        {
            gladMeter -= gladVandNed;
        }

        //t�mmer gl�de recursen n�r tis recursen er over en bestemt m�ngde
        if (tisMeter > whenTisNed)
        {
            gladMeter -= gladTisNed;
        }

        gladShow.size = gladMeter / 100f; //viser recursen i UI
    }

    //g�r det muligt at p�virke tis recursen fra andre scripts
    public void RemuveTis(float amaunt)
    {
        tisMeter -= amaunt;

        if (tisMeter < minBar)
            tisMeter = minBar;

        tisShow.size = tisMeter / 100f;
    }

    //g�r det muligt at p�virke vand recursen fra andre scripts
    public void AddVand(float amaunt)
    {
        vandMeter += amaunt;

        if (vandMeter > maxBar)
            vandMeter = maxBar;

        vandShow.size = vandMeter / 100f;
    }

    //g�r det muligt at p�virke gl�de recursen fra andre scripts
    public void AddGlad(float amaunt)
    {
        gladMeter += amaunt;

        if (gladMeter > maxBar)
            gladMeter = maxBar;

        gladShow.size = gladMeter / 100f;
    }

    //alle recurserne bliver sent til save maneger
    public void SaveRecorses()
    {
        dato = DateTime.Today + DateTime.Now.TimeOfDay;

        SaveClass sd = new SaveClass();
        sd.dato = dato;
        sd.vandData = vandMeter;
        sd.tisData = tisMeter;
        sd.gladData = gladMeter;
    }

    //alle recurserne bliver loadet fra save maneger
    private void LoadRecorses()
    {
        SaveClass sd = new SaveClass();
        dato = sd.dato;

        TimeSpan lastVisit = DateTime.Today + DateTime.Now.TimeOfDay - dato;
        Debug.Log(lastVisit);
    }

}