using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PetScelektion : MonoBehaviour
{
    public List<GameObject> petScelektion;
    [SerializeField]
    private Vector2 showPosition;

    public string petName;
    [HideInInspector]
    public int petType;

    private List<string> saveList;
    [SerializeField]
    private bool inPetMenu;
    private GameObject thePet;

    private void Awake()
    {
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
            
            Invoke("HidePet", 0);
        } 
        else
            Instantiate(petScelektion[petType], thePet.transform);
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

        
    }
}
