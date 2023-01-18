using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class DataSamling : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dato;
    [SerializeField]
    private TextMeshProUGUI spogsmaal;
    [SerializeField]
    private TextMeshProUGUI advarelse;

    [SerializeField]
    private List<string> spogsmaalN;
    [SerializeField]
    private List<GameObject> svarIndput;
    [SerializeField]
    private List<string> svarList;

    private int page;

    public void SporgPlayer()
    {
        dato.text = DateTime.Today.ToString("dd/MM/yyyy");
        page = -1;
        NextSpogsmaal();
    }

    private void NextSpogsmaal()
    {
        page++;
        spogsmaal.text = spogsmaalN[page];
        if (page > 0)
            svarIndput[page - 1].SetActive(false);
        svarIndput[page].SetActive(true);      
    }

    public void SpogsmaalSvar()
    {
        svarList[page] = GameObject.FindWithTag("SvarText").GetComponent<TextMeshProUGUI>().text;   

        if (svarList[page] != null || svarList[page] != "Valg")
        {
            advarelse.text = "";
            NextSpogsmaal();
        }
        else
            advarelse.text = "Ver venlig at udfylde et svar";
    }

    public void UdAf()
    {
        gameObject.SetActive(false);
    }
}
