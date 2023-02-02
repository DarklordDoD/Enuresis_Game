using System.Collections.Generic;
using UnityEngine;
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
    private string lastSvarDato;

    private void Start()
    {
        SporgPlayer();
    }

    //starter Sp�rgeskemaet op
    public void SporgPlayer()
    {       
        //pr�ver at loade gem file
        try
        {
            SaveClass.LoadFromFile("SaveData", out gotList);

            //tjekker om spilleren har svaret i dag og lukker sp�rge skemaet hvis de gjore
            lastSvarDato = gotList[gotList.Count - 8];
            sidsteNavn = gotList[gotList.Count - 7];
        }
        catch { }    

        if (lastSvarDato != DateTime.Today.ToString("dd-MM-yyyy"))
        {
            dato.text = DateTime.Today.ToString("dd/MM/yyyy"); //sette dagens dato ind p� sk�rmen
            svarList.Add(DateTime.Today.ToString("dd/MM/yyyy")); //setter dagens dato p� gem listen
            page = -1;
            NextSpogsmaal();
        }
        else
            UdAf();
    }

    private void NextSpogsmaal()
    {
        //switch til hanling ved spesifikke sp�rgsm�l
        switch (page)
        {
            case -1:
                page++;
                svarIndput[page].SetActive(true);
                standart = GameObject.FindWithTag("SvarText").GetComponent<TextMeshProUGUI>().text;
                nameInput.text = sidsteNavn;
                break;

            case 3:
                if (svarList[page + 1] == "Ja" || svarList[page + 1] == "Yes")
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
      
        if (page >= svarIndput.Count) //gem svar n�r sp�rgeskemaet er f�rdigt
            GemSvar();
        else
        {
            //neste sp�rgsm�l
            spogsmaal.text = spogsmaalN[page];

            foreach (GameObject svar in svarIndput)
                svar.SetActive(false);
            svarIndput[page].SetActive(true);
        }
    }

    //tager imod svar
    public void SpogsmaalSvar()
    {
        //s�rger for at listen altid bliver lige lang
        if (svarList.Count > page + 1 && svarList[page + 1] != splitListExeption)
            svarList[page + 1] = GameObject.FindWithTag("SvarText").GetComponent<TextMeshProUGUI>().text;
        else
            svarList.Add(GameObject.FindWithTag("SvarText").GetComponent<TextMeshProUGUI>().text);       

        //registrer om der er svaret p� sp�rgsm�let
        if (page == 0 && standart == svarList[page + 1])
            advarelse.text = "Ver venlig at udfylde et svar";
        else
        {
            advarelse.text = "";
            NextSpogsmaal();
        }
    }

    //g�r id af sp�rgeskemaet uden at gemme
    public void UdAf()
    {
        gameObject.SetActive(false);
    }

    //gemmer og g�r ud af sp�rgeskemate
    private void GemSvar()
    {
        SaveClass.WriteToFile("SaveData", svarList, true);
        gameObject.SetActive(false);
    }
}
