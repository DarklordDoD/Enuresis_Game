using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UseSnack : MonoBehaviour
{
    [SerializeField]
    private GameObject showSnack;

    [HideInInspector]
    public string snackType;
    private Snacks snackStorige;
    private GameObject thePet;
    private TextMeshProUGUI showAmount;

    private void Start()
    {
        snackStorige = GetComponentInParent<Snacks>();
        GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprites/{snackType}");
        thePet = GameObject.FindGameObjectWithTag("Player");

        showAmount = GetComponentInChildren<TextMeshProUGUI>();
        foreach (ASnack aSnack in snackStorige.snacks)
        {
            if (aSnack.snackType == snackType)
            {
                showAmount.text = aSnack.amaunt.ToString();
            }
        }
    }

    public void EatSnack()
    {
        if (!GetComponentInParent<SnackMenu>().isEating)
        {
            try
            {
                foreach (ASnack aSnack in snackStorige.snacks)
                {
                    if (aSnack.snackType == snackType)
                    {
                        aSnack.amaunt -= 1;
                        showAmount.text = aSnack.amaunt.ToString();

                        GetComponentInParent<SnackMenu>().isEating = true;

                        GetComponentInParent<Ressourcer>().AddGlad(aSnack.giveHappy);

                        Transform petHand = thePet.transform.GetChild(0).transform.GetChild(2);
                        Instantiate(showSnack, petHand);
                        petHand.GetChild(petHand.childCount - 1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/{snackType}");

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
        }     
    }
}
