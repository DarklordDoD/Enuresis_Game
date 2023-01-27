using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public static class SaveClass
{
    //gem variabler ved at sende en string liste til denne funktion
    public static void WriteToFile(string fileName, List<string> gemDette, bool addToSave)
    {
        string fileContents = string.Join(",", gemDette); //konvater list til string
        string path = Application.persistentDataPath + $"/{fileName}.txt"; //find stede hvor filer skal gemmes

        //prøver at loade en file så filen kan forlænges
        if (addToSave)
        {
            try
            {
                string loadData = File.ReadAllText(path);
                fileContents = $"{loadData}\n{fileContents}"; //sette de sceneste variabler i forlengelse af filens indhold
            }
            catch (Exception e)
            {
                Debug.Log($"No Save. {e}");
            }
        }

        //laver ny file
        FileStream stream = new FileStream(path, FileMode.Create); 
        stream.Close();

        //prøver at skrive string ind i filen
        try
        {
            File.WriteAllText(path, fileContents); 
            //Debug.Log($"Gemt ({fileContents}) to ({fileName})");
        }
        catch //(Exception e)
        {
            //Debug.LogError($"Failed to write to {fileName} with exception {e}");
        }
    }

    //load var iabler, som en liste af strings, fra denne funktion
    public static List<string> LoadFromFile(string fileName, out List<string> loadList)
    {
        string path = Application.persistentDataPath + $"/{fileName}.txt"; //finder file

        //prøver at loade filens indhold og send liste tilbage
        try
        {
            string loadData = File.ReadAllText(path);
            return loadList = loadData.Split(',', '\n').ToList();
        }
        catch //(Exception e) //hvis fejl sand ikke en liste
        {
            //Debug.LogError($"Failed to read from {path} with exception {e}");
            loadList = null;
            return loadList;
        }
    }
}
