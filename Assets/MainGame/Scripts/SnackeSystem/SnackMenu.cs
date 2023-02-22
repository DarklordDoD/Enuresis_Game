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
    private bool menuOpen;

    private float insetIndhold;
    private int nextIndhold;
    [HideInInspector]
    public List<GameObject> theSnacks;
    private Vector2 snackPosition; 

    // Start is called before the first frame update
    void Start()
    {
        snackStorige = GetComponentInParent<Snacks>();
        snackMenu = this.transform.GetChild(0).gameObject;
        openAmount = 0;

        foreach (ASnack aSnack in snackStorige.snacks)
        {
            theSnacks.Add(snackHolder);
        }

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
                    if (theSnacks.Count > 10)
                        insetIndhold += 100 / theSnacks.Count;
                    else
                        insetIndhold += 50;

                if (theSnacks.Count > nextIndhold)
                {
                    Instantiate(theSnacks[nextIndhold], snackMenu.transform);

                    GameObject theShowSnack = snackMenu.transform.GetChild(snackMenu.transform.childCount - 1).gameObject;
                    theShowSnack.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Snack/{snackStorige.snacks[nextIndhold].snackType}");
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
                    if (theSnacks.Count > 10)
                        insetIndhold -= 100 / theSnacks.Count;
                    else
                        insetIndhold -= 50;

                if (openAmount > 0) 
                {
                    int allShowSnacks = snackMenu.transform.childCount;

                    if (allShowSnacks > 0)
                        Destroy(snackMenu.transform.GetChild(allShowSnacks - 1).gameObject);

                    if (nextIndhold > 0)
                        nextIndhold -= 1;
                }               
            } 
        }

        snackMenu.GetComponent<Image>().fillAmount = openAmount / 100;
    }

    public void OpenMenu()
    {
        menuOpen = !menuOpen;
    }
}
