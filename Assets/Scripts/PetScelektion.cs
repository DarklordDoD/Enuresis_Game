using System;
using System.Collections.Generic;
using UnityEngine;

public class PetScelektion : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> petScelektion;
    [SerializeField]
    private Vector2 showPosition;
    public string petName;
    [SerializeField]
    private int petType;

    private List<string> saveList;
    private bool inPetMenu;

    private void Awake()
    {
        //prøver at loade sidste gemte pet
        try
        {
            SaveClass.LoadFromFile("Pet", out saveList);
            petName = saveList[0];
            petType = int.Parse(saveList[1]);

            Instantiate(petScelektion[petType], GameObject.FindGameObjectWithTag("Player").transform);
        }
        catch
        {
            inPetMenu = true;

            try 
            {
                GetComponentInChildren<DataSamling>().UdAf();
            }
            catch { }

            GetComponent<SceneManeger>().NewScene("PetShop", true);

            GameObject[] thePet = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject pets in thePet)
                pets.SetActive(false);       
        }
    }

    private void Start()
    {
        if (inPetMenu)
            Instantiate(petScelektion[petType], showPosition, Quaternion.identity); //Vis det nye pet
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

        GetComponent<SceneManeger>().NewScene("SampleScene", true);

        Instantiate(petScelektion[petType], GameObject.FindGameObjectWithTag("Player").transform);  
    }
}
