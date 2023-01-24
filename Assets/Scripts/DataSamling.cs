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
    private TMP_InputField nameInput;
    [SerializeField]
    private List<string> svarList;
    [SerializeField]
    private string splitListExeption;
    [SerializeField]
    private List<string> gotList;

    private int page;
    private string standart;
    private string sidsteNavn;

    public void SporgPlayer()
    {
        try
        {
            SaveClass.LoadFromFile("SaveData", out gotList);
        }
        catch (Exception e)
        {
            Debug.Log($"No Save. {e}");
        }

        string lastSvarDato = gotList[gotList.Count - 8];
        sidsteNavn = gotList[gotList.Count - 7];

        if (lastSvarDato != DateTime.Today.ToString("dd-MM-yyyy"))
        {
            dato.text = DateTime.Today.ToString("dd/MM/yyyy");
            svarList.Add(DateTime.Today.ToString("dd/MM/yyyy"));
            page = -1;
            NextSpogsmaal();
        }
        else
            UdAf();
    }

    private void NextSpogsmaal()
    {
        switch (page)
        {
            case -1:
                page++;
                svarIndput[page].SetActive(true);
                standart = GameObject.FindWithTag("SvarText").GetComponent<TextMeshProUGUI>().text;
                nameInput.text = sidsteNavn;
                break;

            case 3:
                if (svarList[page + 1] == "Nej" || svarList[page + 1] == "No")
                    page = 5;
                else
                {
                   svarList.Add(splitListExeption);
                   page++;
                }
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
        if (svarList.Count > page + 1 && svarList[page + 1] != splitListExeption)
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
        gameObject.SetActive(false);
    }
}
