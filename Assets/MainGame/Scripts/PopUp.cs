using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField]
    private float tonIn;
    [SerializeField]
    private float tonUd;

    private float timeTaker;
    [SerializeField]
    private float timeTakerUd;
    private TextMeshProUGUI text;
    private Image back;
    private Color textColor;
    private Color BackColor;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        back = GetComponent<Image>();

        textColor = text.color;
        BackColor = back.color;
    }

    // Update is called once per frame
    void Update()
    {
        timeTaker += Time.deltaTime;

        if (tonIn > timeTaker)
        {
            timeTakerUd += Time.deltaTime;

            textColor.a = timeTakerUd / tonIn;
            text.color = textColor;

            BackColor.a = timeTakerUd / tonIn;
            back.color = BackColor;
        }
        else if (tonIn + tonUd > timeTaker)
        {
            timeTakerUd -= Time.deltaTime;

            textColor.a = timeTakerUd / tonUd;
            text.color = textColor;

            BackColor.a = timeTakerUd / tonUd;
            back.color = BackColor;
        }
        else if (timeTakerUd < 0)
            Destroy(gameObject);
    }

    public void ChangeText(string theText)
    {
        text.text = theText;
    }
}
