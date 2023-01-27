using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjecktSkift : MonoBehaviour
{
    [SerializeField]
    private bool right; //Hvilken retning skal den roter

    PetScelektion petS;

    private void Start()
    {
            petS = GameObject.FindGameObjectWithTag("GameController").GetComponent<PetScelektion>();
    }

    //for besked om at den næste sene skal loades
    public void SkiftTil()
    {
        petS.RotatePet(right);
    }
}
