using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseSnack : MonoBehaviour
{
    [HideInInspector]
    public string snackType;
    private Snacks snackStorige;
    private GameObject thePet;

    private void Start()
    {
        snackStorige = GetComponentInParent<Snacks>();
        GetComponent<Image>().sprite = Resources.Load<Sprite>($"Snack/{snackType}");
        thePet = GameObject.FindGameObjectWithTag("Player");
    }

    public void EatSnack()
    {
        try
        {
            foreach (ASnack aSnack in snackStorige.snacks)
            {
                if (aSnack.snackType == snackType)
                {
                    aSnack.amaunt -= 1;

                    GetComponentInParent<Ressourcer>().AddGlad(aSnack.giveHappy);

                    thePet.GetComponentInChildren<Animator>().SetBool("Spise", true);
                    thePet.GetComponent<muvePet>().onTask = true;                   

                    if (aSnack.amaunt <= 0)
                    {
                        Destroy(gameObject);

                        snackStorige.snacks.Remove(aSnack);
                    }
                }
            }
        } catch { }

        Invoke("StopAnimation", 0.5f);
    }

    private void StopAnimation()
    {
        thePet.GetComponentInChildren<Animator>().SetBool("Spise", false);
    }
}
