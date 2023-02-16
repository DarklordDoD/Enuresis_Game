using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AddRessourcer : MonoBehaviour
{
    [SerializeField]
    private List<float> addRessuorcerTil = new List<float> {0,0,0};
    [SerializeField]
    private float delayMellemKlik;
    [SerializeField]
    private Image showTimer;

    private Ressourcer ressourcer;
    private float klikTimer;
    [SerializeField]
    private bool tjekTrikker;
    [SerializeField]
    private Collider2D Detecter;
    private void Start()
    {
        Detecter = transform.GetChild(0).gameObject.GetComponent<Collider2D>();

        klikTimer = delayMellemKlik;
    }

    private void Update()
    {
        if(klikTimer < delayMellemKlik)
            klikTimer += Time.deltaTime;

        if (showTimer)
            showTimer.fillAmount = klikTimer / delayMellemKlik;
    }

    public void AddNow()
    {
        if (tjekTrikker)
        {
            ressourcer = GameObject.FindGameObjectWithTag("GameController").GetComponent<Ressourcer>();

            if (klikTimer >= delayMellemKlik)
            {

                klikTimer = 0;
                tjekTrikker = false;
                Detecter.enabled = false;

                ressourcer.RemuveTis(addRessuorcerTil[0]);
                ressourcer.AddVand(addRessuorcerTil[1]);
                ressourcer.AddGlad(addRessuorcerTil[2]);
            }
        }
    }

    public void DoTask()
    {
        tjekTrikker = true;
        Detecter.enabled = true;
    }
}
