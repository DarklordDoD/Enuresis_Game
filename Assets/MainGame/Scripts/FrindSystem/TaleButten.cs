using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaleButten : MonoBehaviour
{
    [HideInInspector]
    public bool lukAktiv;

    public void AktiverNext()
    {
        if (lukAktiv)
        {
            GetComponent<SceneSkift>().SkiftTil();
            return;
        }

        GetComponentInParent<FrindSamtale>().RunSamtale(true);
    }
}
