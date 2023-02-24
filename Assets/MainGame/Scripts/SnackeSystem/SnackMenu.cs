using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SnackMenu : MonoBehaviour
{
    [SerializeField]
    private float openSpeed;
    [SerializeField]
    private GameObject snackHolder;
    [SerializeField]
    private int spacing;
    [SerializeField]
    private Vector2 startPosition;

    private Snacks snackStorige;
    private GameObject snackMenu;

    private float openAmount;
    [HideInInspector]
    public bool menuOpen;

    [SerializeField]
    private float insetIndhold;
    private int nextIndhold;
    private Vector2 snackPosition;
    public bool isEating;

    // Start is called before the first frame update
    void Start()
    {
        snackStorige = GetComponentInParent<Snacks>();
        snackMenu = this.transform.GetChild(0).gameObject;
        openAmount = 0;
        snackMenu.SetActive(false);

        snackPosition.y = startPosition.y + transform.position.y;
        snackPosition.x = startPosition.x + transform.position.x;
    }

    private void Update()
    {
        if (menuOpen && openAmount < 100)
        {
            openAmount += openSpeed * Time.deltaTime;

            if (openAmount > insetIndhold)
            {
                if (insetIndhold < 100)
                    try
                    {
                        insetIndhold += 100 / snackStorige.snacks.Count;
                    }
                    catch { }

                if (snackStorige.snacks.Count > nextIndhold)
                {
                    Instantiate(snackHolder, snackMenu.transform);

                    GameObject theShowSnack = snackMenu.transform.GetChild(snackMenu.transform.childCount - 1).gameObject;
                    theShowSnack.GetComponent<UseSnack>().snackType = snackStorige.snacks[nextIndhold].snackType;
                    theShowSnack.transform.position = snackPosition;

                    snackPosition.x += spacing;
                    nextIndhold += 1;
                }
            }
        }
        else if (!menuOpen && openAmount > 0)
        {
            openAmount -= openSpeed * Time.deltaTime;

            snackPosition.y = startPosition.y + transform.position.y;
            snackPosition.x = startPosition.x + transform.position.x;

            if (openAmount < insetIndhold)
            {
                if (insetIndhold > 0)
                    try
                    {
                        insetIndhold -= 100 / snackStorige.snacks.Count;
                    }
                    catch { }

                if (openAmount > 0) 
                {
                    int allShowSnacks = snackMenu.transform.childCount;

                    if (allShowSnacks > 0)
                        Destroy(snackMenu.transform.GetChild(allShowSnacks - 1).gameObject);

                    if (nextIndhold > 0)
                        nextIndhold -= 1;
                }
                else
                { snackMenu.SetActive(false); }
            } 
        }

        snackMenu.GetComponent<Image>().fillAmount = openAmount / 100;
    }

    public void OpenMenu()
    {       
        menuOpen = !menuOpen;
        snackMenu.SetActive(true);
    }
}
