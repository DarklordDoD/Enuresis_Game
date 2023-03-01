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

    [Header("Frind")]
    [SerializeField]
    private GameObject frindButten;
    [SerializeField]
    private List<Sprite> buttenLooks;
    [SerializeField]
    private GameObject theFrind;
    [SerializeField]
    private Vector2 frindStartPosition;
    [SerializeField]
    private bool frind;
    //[HideInInspector]
    public bool nuMiniGame;

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
        Invoke("LydChange", 0.5f);
        Invoke("AddFrind", 0.5f);
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

    public void FrindEvent()
    {
        frind = !frind;

        AddFrind();
    }

    public void AddFrind()
    {
        frindButten.GetComponent<Image>().sprite = buttenLooks[Convert.ToInt32(frind)];

        if (!nuMiniGame)
        {
            if (frind && muvePet.frindInstance == null)
                Instantiate(theFrind, frindStartPosition, Quaternion.identity);
            else if (!frind)
                Destroy(muvePet.frindInstance);
        }
    }

    private void LoadInstilinger()
    {
        SaveClass.LoadFromFile("Instillinger", out gotList);

        soundLevel = int.Parse(gotList[1]);
        sprog = int.Parse(gotList[0]);
        frind = Convert.ToBoolean(gotList[2]);
    }

    public void SaveInstillinger()
    {
        saveList = new List<string>() {sprog.ToString(),soundLevel.ToString(),frind.ToString()};

        SaveClass.WriteToFile("Instillinger", saveList, false);
    }
}
