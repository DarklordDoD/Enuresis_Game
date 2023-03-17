using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShowButton : MonoBehaviour
{
    public Button buttonToAppear;
    public List<Button> showButtons = new List<Button>();

    private void Start()
    {
       

        // add listeners to the showButtons' OnClick events
        foreach (Button showButton in showButtons)
        {
            showButton.onClick.AddListener(MakeButtonAppear);
        }
    }

    public void MakeButtonAppear()
    {
        // check if the button is already active
        if (!buttonToAppear.gameObject.activeSelf)
        {
            // set the button to be visible
            buttonToAppear.gameObject.SetActive(true);
        }
    }
}