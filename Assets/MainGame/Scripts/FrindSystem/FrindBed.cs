using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrindBed : MonoBehaviour
{

    private Instilinger gameInstilinger;

    // Start is called before the first frame update
    void Start()
    {

        gameInstilinger = GameObject.FindGameObjectWithTag("GameController").GetComponent<Instilinger>();
        SetBedStait();
    }

    public void SetBedStait()
    {
        GetComponent<SpriteRenderer>().enabled = gameInstilinger.frind;
    }
}
