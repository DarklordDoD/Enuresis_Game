using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeInput : MonoBehaviour
{
    private List<string> validInputs;
    [SerializeField]
    private string textNu;

    private TMP_InputField timeText;
    [SerializeField]
    private int testForInput;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<TMP_InputField>();
        validInputs = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
    }

    public void InputChange()
    {
        bool dontRemuve = false;

        foreach (string theInput in validInputs)
        {
            if (timeText.text.EndsWith(theInput))
                dontRemuve = true;
        }

        if (!dontRemuve && timeText.text.Length != 0)
            timeText.text = timeText.text.Remove(timeText.text.Length - 1);

        if (timeText.text.Length == 3 && !timeText.text.EndsWith(":"))
        {
            timeText.text = timeText.text.Insert(2, ":");
            timeText.MoveToEndOfLine(false, false);
        }
    }
}
