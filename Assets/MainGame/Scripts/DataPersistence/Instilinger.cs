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

    [Header("Sprog")]
    [SerializeField]
    private List<Button> sprogButtens;

    [HideInInspector]
    public int sprog;

    [Header("Lyd")]
    [SerializeField]
    private Slider soundSlider;
    [HideInInspector]
    public int soundLevel;

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
        VelgSprog(sprog);

        soundSlider.value =  (float)soundLevel / 100;
        Invoke("LydChange", 0f);
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

    public void LydStyrke()
    {
        soundLevel = Convert.ToInt32(soundSlider.value * 100);
        LydChange();
    }

    private void LydChange()
    {
        GetComponent<MusicManager>().SetMusicVolume((float)soundLevel / 100);
    }

    private void LoadInstilinger()
    {
        SaveClass.LoadFromFile("Instillinger", out gotList);

        soundLevel = int.Parse(gotList[1]);
        sprog = int.Parse(gotList[0]);
    }

    public void SaveInstillinger()
    {
        saveList = new List<string>() {sprog.ToString(),soundLevel.ToString()};

        SaveClass.WriteToFile("Instillinger", saveList, false);
    }
}
