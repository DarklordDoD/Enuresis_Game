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

    private void Awake()
    {
        //prøver at loade sidste gemte pet
        try
        {
            SaveClass.LoadFromFile("Pet", out saveList);
            petName = saveList[0];
            petType = int.Parse(saveList[1]);
        }
        catch
        {
            try 
            {
                GetComponentInChildren<DataSamling>().UdAf();
            }
            catch { }

            GetComponent<Ressourcer>().enabled = false; 
            
            GetComponent<SceneManeger>().NewScene("PetShop", true);

            GameObject[] thePet = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject pets in thePet)
                pets.SetActive(false);
        }
    }

    public void RotatePet(bool right)
    {
        if (right)
        {
            if (petType >= petScelektion.Count)
                petType = 0;
            else
                petType++;
        }
        else
        {
            if (petType <= 0)
                petType = petScelektion.Count;
            else
                petType--;
        }

        GameObject[] showPet = GameObject.FindGameObjectsWithTag("ShowPet");
        foreach (GameObject show in showPet)
        {
            Destroy(show);
        }

        Instantiate(petScelektion[petType], showPosition, Quaternion.identity);
    }
    public void ScelektEnd()
    {
        saveList = new List<string>() {"",""}; //set list lengde

        //samle alle variabler i en liste
        saveList[0] = petName;
        saveList[1] = petType.ToString();

        SaveClass.WriteToFile("Pet", saveList, false);

        GetComponent<Ressourcer>().enabled = false;
        GetComponent<SceneManeger>().NewScene("SampleScene", false);
    }
}
