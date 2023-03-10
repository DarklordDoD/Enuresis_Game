using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetTypeMenu : MonoBehaviour
{

    private PetScelektion PetStorige;
    // Start is called before the first frame update
    void Start()
    {
        PetStorige = GameObject.FindGameObjectWithTag("GameController").GetComponent<PetScelektion>();
    }

    public void PetChange(bool right)
    {
        PetStorige.RotatePet(right);
    }

    public void PetCelekt()
    {
        PetStorige.ScelektEnd();
    }
}
