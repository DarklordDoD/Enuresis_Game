using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationEnd : MonoBehaviour
{
    [SerializeField]
    private GameObject snackDesplay;

    private void Start()
    {
        //GameObject.FindGameObjectWithTag("GameController");
        snackDesplay = GameObject.FindGameObjectWithTag("GameController").transform.Find("Snacks").gameObject;
    }

    public void MuveAgain()
    {
        GetComponentInParent<muvePet>().onTask = false;

        snackDesplay.GetComponent<SnackMenu>().isEating = false;
    }

    public void RemuveSnack()
    {
        Transform pethHand = transform.GetChild(2).transform;
        Destroy(pethHand.GetChild(pethHand.childCount - 1).gameObject);
    }
}
