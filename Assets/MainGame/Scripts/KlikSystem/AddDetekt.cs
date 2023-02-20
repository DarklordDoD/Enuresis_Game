using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDetekt : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponentInParent<AddRessourcer>().AddNow();
    }
}
