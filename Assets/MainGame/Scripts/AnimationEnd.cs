using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationEnd : MonoBehaviour
{
    private GameObject snackDesplay;

    private void Start()
    {
        snackDesplay = GameObject.FindGameObjectWithTag("GameController").transform.Find("Snacks").gameObject;
    }

    public void MuveAgain()
    {
        try
        {
            GetComponentInParent<muvePet>().onTask = false;
        } catch { }

        snackDesplay.GetComponent<SnackMenu>().isEating = false;
    }

    public void RemuveSnack()
    {
        GetComponent<Animator>().SetBool("Spise", false);

        try
        {
            Transform pethHand = transform.GetChild(2).transform;
        
            Destroy(pethHand.GetChild(pethHand.childCount - 1).gameObject);
        } catch { }
    }
}
