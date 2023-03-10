using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelgPetButten : MonoBehaviour
{
    public void EndNu()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<PetScelektion>().ScelektEnd();
    }
}
