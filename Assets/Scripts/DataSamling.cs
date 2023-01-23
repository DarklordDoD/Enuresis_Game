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

    [SerializeField]
    private int page;
    private string standart;

    public void SporgPlayer()
    {
        dato.text = DateTime.Today.ToString("dd/MM/yyyy");
        svarList.Add((DateTime.Today).ToString("dd/MM/yyyy"));
        page = -1;
        NextSpogsmaal();
    }

    private void NextSpogsmaal()
    {
        switch (page)
        {
            case -1:
                page++;
                svarIndput[page].SetActive(true);
                standart = GameObject.FindWithTag("SvarText").GetComponent<TextMeshProUGUI>().text;
                break;

            case 3:
                if (svarList[page + 1] == "Nej" || svarList[page + 1] == "No")
                    page = 5;
                else
                    page++;
                break;

            case 4:
                page = 7;
                break;

            default:
                page++;
                break;
        }

        if (page >= svarIndput.Count)
            GemSvar();
        else
        {
            spogsmaal.text = spogsmaalN[page];

            if (page > 0)
                svarIndput[page - 1].SetActive(false);
            svarIndput[page].SetActive(true);
        }
    }

    public void SpogsmaalSvar()
    {
        if (svarList.Count > page + 1)
            svarList[page + 1] = GameObject.FindWithTag("SvarText").GetComponent<TextMeshProUGUI>().text;
        else
            svarList.Add(GameObject.FindWithTag("SvarText").GetComponent<TextMeshProUGUI>().text);       

        if (page == 0 && standart == svarList[page + 1])
            advarelse.text = "Ver venlig at udfylde et svar";
        else
        {
            advarelse.text = "";
            NextSpogsmaal();
        }
    }

    public void UdAf()
    {
        gameObject.SetActive(false);
    }

    private void GemSvar()
    {
        SaveClass.WriteToFile("SaveData", svarList, true);
        Destroy(gameObject);
    }
}
