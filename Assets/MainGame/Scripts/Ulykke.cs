using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Ulykke : MonoBehaviour
{
    Ressourcer ressourcer;

    private void Start()
    {
        ressourcer = GameObject.FindGameObjectWithTag("GameController").GetComponent<Ressourcer>();

        GetComponent<SpriteRenderer>().enabled = ressourcer.aktivStain;
        GetComponent<Collider2D>().enabled = ressourcer.aktivStain;
    }

    public void HarTisset(bool showStain)
    {
        GetComponent<SpriteRenderer>().enabled = showStain;
        GetComponent<Collider2D>().enabled = showStain;

        GameObject.FindGameObjectWithTag("GameController").GetComponent<Ressourcer>().aktivStain = showStain;
    }
}
