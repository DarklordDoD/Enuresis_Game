using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Instilinger : MonoBehaviour
{
    [SerializeField]
    private GameObject instillingMenu;
    [SerializeField]
    private List<Button> sprogButtens;

    //[HideInInspector]
    public int sprog;
    [HideInInspector]
    public float soundLevel;

    private List<string> gotList;
    private List<string> saveList;

    private void Awake()
    {
        try
        {
            LoadInstilinger();
        }
        catch
        {
            sprog = 0;
            soundLevel = 50;
        }
    }

    private void Start()
    {
        AktivateButten();
    }

    private void AktivateButten()
    {
        foreach (Button button in sprogButtens)
            button.interactable = true;

        sprogButtens[sprog].interactable = false;
    }

    void OnApplicationQuit()
    {
        SaveInstillinger();
    }

    public void OpenMenu()
    {
        if (instillingMenu.activeSelf)
            instillingMenu.SetActive(false);
        else
            instillingMenu.SetActive(true);
    }

    public void VelgSprog(int detteSprog)
    {
        sprog = detteSprog;

        AktivateButten();
    }

    private void LoadInstilinger()
    {
        SaveClass.LoadFromFile("Instillinger", out gotList);

        soundLevel = float.Parse(gotList[0]);
        sprog = int.Parse(gotList[1]);
    }

    public void SaveInstillinger()
    {
        saveList = new List<string>() {sprog.ToString(),soundLevel.ToString()};

        SaveClass.WriteToFile("Instillinger", saveList, false);
    }
}
