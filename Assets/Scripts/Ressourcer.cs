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
    private DateTime dato;

    private void Awake()
    {
        SaveRecorses();
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

        LoadRecorses();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (opdateringsTid > reduceSpeed)
        {
            opdateringsTid = 0;

            if (tisMeter < maxBar)
                TisControl();

            if (vandMeter > minBar)
                VandControl();

            if (gladMeter > minBar)
                GladControl();

        }
        else
        {
            opdateringsTid += 1 * Time.deltaTime;
        }
    }

    private void TisControl()
    {
        tisMeter += tisUp;

        if (vandMeter >= whenVandUp)
        {
            tisMeter += tisVandUp;
        }

        tisShow.size = tisMeter / 100f;
    }

    private void VandControl()
    {
        vandMeter -= vandNed;

        vandShow.size = vandMeter / 100f;
    }

    private void GladControl()
    {
        if (vandMeter < whenVandNed)
        {
            gladMeter -= gladVandNed;
        }

        if (tisMeter > whenTisNed)
        {
            gladMeter -= gladTisNed;
        }

        gladShow.size = gladMeter / 100f;
    }

    public void RemuveTis(float amaunt)
    {
        tisMeter -= amaunt;

        if (tisMeter < minBar)
            tisMeter = minBar;

        tisShow.size = tisMeter / 100f;
    }

    public void AddVand(float amaunt)
    {
        vandMeter += amaunt;

        if (vandMeter > maxBar)
            vandMeter = maxBar;

        vandShow.size = vandMeter / 100f;
    }

    public void AddGlad(float amaunt)
    {
        gladMeter += amaunt;

        if (gladMeter > maxBar)
            gladMeter = maxBar;

        gladShow.size = gladMeter / 100f;
    }

    public void SaveRecorses()
    {
        dato = DateTime.Today + DateTime.Now.TimeOfDay;

        SaveClass sd = new SaveClass();
        sd.dato = dato;
        sd.vandData = vandMeter;
        sd.tisData = tisMeter;
        sd.gladData = gladMeter;
    }

    private void LoadRecorses()
    {
        SaveClass sd = new SaveClass();
        dato = sd.dato;

        TimeSpan lastVisit = DateTime.Today + DateTime.Now.TimeOfDay - dato;
        Debug.Log(lastVisit);
    }

}