using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDetekt : MonoBehaviour
{

    [SerializeField]
    private AddRessourcer interaktivObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interaktivObject.AddNow();
    }
}
