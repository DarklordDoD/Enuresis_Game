using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRessourcer : MonoBehaviour
{
    [SerializeField]
    private List<float> addRessuorcerTil = new List<float> {0,0,0};
    [SerializeField]
    private float delayMellemKlik;

    private Ressourcer ressourcer;
    private float klikTimer;

    private void Start()
    {
        ressourcer = GameObject.FindGameObjectWithTag("GameController").GetComponent<Ressourcer>();
    }

    private void Update()
    {
        if(klikTimer < delayMellemKlik)
            klikTimer += Time.deltaTime;
    }

    public void AddNow()
    {
        if (klikTimer >= delayMellemKlik) {

            klikTimer = 0;

            ressourcer.RemuveTis(addRessuorcerTil[0]);
            ressourcer.AddVand(addRessuorcerTil[1]);
            ressourcer.AddGlad(addRessuorcerTil[2]);
        }
    }
}
