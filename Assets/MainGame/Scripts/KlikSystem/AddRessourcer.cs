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
                petMuvment.onTask = true;
                petAnimator.SetBool(theAnimation, true);
                
                Invoke("StopAnimation", 0.5f);

                klikTimer = 0;
                tjekTrikker = false;
                Detecter.enabled = false;

                ressourcer.RemuveTis(addRessuorcerTil[0]);
                ressourcer.AddVand(addRessuorcerTil[1]);
                ressourcer.AddGlad(addRessuorcerTil[2]);
            }
        }
    }

    private void StopAnimation()
    {
        petAnimator.SetBool(theAnimation, false);
    }

    public void DoTask()
    {
        tjekTrikker = true;
        Detecter.enabled = true;
    }
}
