using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PetScelektion : MonoBehaviour
{
    public List<GameObject> petScelektion;
    [SerializeField]
    private Vector2 showPosition;

    public string petName;
    [HideInInspector]
    public int petType;

    private List<string> saveList;
    private bool inPetMenu;
    private GameObject thePet;

    private GameObject snackMenu;

    private void Awake()
    {
        Application.targetFrameRate = 30;

        thePet = GameObject.FindGameObjectWithTag("Player");

        //prøver at loade sidste gemte pet
        try
        {
            SaveClass.LoadFromFile("Pet", out saveList);
            petName = saveList[0];
            petType = int.Parse(saveList[1]);
        }
        catch
        {
            inPetMenu = true;

            GetComponent<SceneManeger>().NewScene("PetShop");
        }
    }

    private void Start()
    {
        if (inPetMenu)
        {
            Instantiate(petScelektion[petType], showPosition, Quaternion.identity); //Vis det nye pet

            snackMenu = GameObject.Find("Snacks");
            snackMenu.SetActive(false);

            Invoke("HidePet", 0);
        }
        else
        {
            Instantiate(petScelektion[petType], thePet.transform);

            List<GameObject> ownedItems = GetComponent<AllItems>().boughtItems;
            GameObject petSkin = thePet.transform.GetChild(0).gameObject;

            for (int i = 2; i < saveList.Count; i++)
            {
                try
                {
                    GameObject wear = ownedItems.Where(obj => obj.name == saveList[i]).SingleOrDefault();

                    GameObject petLim = petSkin.transform.GetChild(wear.GetComponent<Item>().limNumber).gameObject;
                    Instantiate(wear, petLim.GetComponent<Transform>());
                }
                catch { }
            }
        }
    }

    private void HidePet()
    {
        //thePet.SetActive(false);
        GameObject.FindGameObjectWithTag("DataSamling").GetComponent<DataSamling>().UdAf();
    }

    public void RotatePet(bool right)
    {      
        //roter i gennem listen
        if (right) 
        {
            if (petType >= petScelektion.Count - 1)
                petType = 0;
            else
                petType++;
        }
        else
        {
            if (petType <= 0)
                petType = petScelektion.Count - 1;
            else
                petType--;
        }

        try
        {
            //fjern det gamle pet
            GameObject[] showPet = GameObject.FindGameObjectsWithTag("ShowPet");
            foreach (GameObject show in showPet)
            {
                Destroy(show);
            }
        }
        catch { }

        Instantiate(petScelektion[petType], showPosition, Quaternion.identity); //Vis det nye pet
    }
    public void ScelektEnd()
    {
        saveList = new List<string>() {"",""}; //set list lengde

        //samle alle variabler i en liste
        saveList[0] = petName;
        saveList[1] = petType.ToString();

        SaveClass.WriteToFile("Pet", saveList, false);

        thePet = GameObject.FindGameObjectWithTag("Player");
        Instantiate(petScelektion[petType], thePet.transform);

        thePet.GetComponent<muvePet>().lateStart();

        GetComponent<SceneManeger>().NewScene("Stue");

        snackMenu.SetActive(true);
    }
}
