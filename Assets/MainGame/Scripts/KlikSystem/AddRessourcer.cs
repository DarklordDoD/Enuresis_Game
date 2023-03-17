using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.UI;

public class AddRessourcer : MonoBehaviour
{
    [SerializeField]
    private List<float> addRessuorcerTil = new List<float> {0,0,0,0};
    [SerializeField]
    private float delayMellemKlik;
    [SerializeField]
    private Image showTimer;

    private Ressourcer ressourcer;
    private float klikTimer;
    [SerializeField]
    private bool tjekTrikker;
    private Collider2D Detecter;

    [SerializeField]
    private string theAnimation;
    private Animator petAnimator;
    private muvePet petMuvment;

    private void Start()
    {
        Detecter = transform.GetChild(0).gameObject.GetComponent<Collider2D>();

        GameObject thePet = GameObject.FindGameObjectWithTag("Player");
        petAnimator = thePet.GetComponentInChildren<Animator>();
        petMuvment = thePet.GetComponent<muvePet>();

        klikTimer = delayMellemKlik;

        ressourcer = GameObject.FindGameObjectWithTag("GameController").GetComponent<Ressourcer>();
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
            if (klikTimer >= delayMellemKlik)
            {
                petMuvment.onTask = true;
                petAnimator.SetBool(theAnimation, true);
                
                Invoke("StopAnimation", 0.5f);

                klikTimer = 0;
                tjekTrikker = false;
                Detecter.enabled = false;

                TheAddAmount();
            }
        }
    }

    public void TheAddAmount()
    {
        ressourcer.RemuveTis(addRessuorcerTil[0]);
        ressourcer.AddVand(addRessuorcerTil[1]);
        ressourcer.AddGlad(addRessuorcerTil[2]);
        ressourcer.monny += Convert.ToInt32(addRessuorcerTil[3]);
    }

    private void StopAnimation()
    {
        petAnimator.SetBool(theAnimation, false);
    }

    public void DoTask()
    {
        if (klikTimer >= delayMellemKlik)
            tjekTrikker = true;

        Detecter.enabled = true;
    }
}
