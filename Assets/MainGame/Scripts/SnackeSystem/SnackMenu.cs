using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SnackMenu : MonoBehaviour
{
    private Snacks snackStorige;
    private GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        snackStorige = GetComponentInParent<Snacks>();
        menu = this.transform.GetChild(0).gameObject;
        menu.GetComponent<Image>();
    }

    public void OpenMenu()
    {
        
    }
}
