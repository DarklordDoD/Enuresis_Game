using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;


public static class SaveClass
{
    public static void WriteToFile(string fileName, List<string> gemDette, bool addToSave)
    {
        string fileContents = string.Join(",", gemDette);
        string path = Application.persistentDataPath + $"/{fileName}.txt";

        if (addToSave)
        {
            string loadData = File.ReadAllText(path);
            fileContents = $"{loadData}\n{fileContents}";
        }

        FileStream stream = new FileStream(path, FileMode.Create);
        stream.Close();      

        try
        {
            File.WriteAllText(path, fileContents);          
            Debug.Log($"Gemt ({fileContents}) to ({fileName})");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {fileName} with exception {e}");
        }     
    }

    public static List<string> LoadFromFile(string fileName, out List<string> loadList)
    {
        string path = Application.persistentDataPath + $"/{fileName}.txt";

        try
        {
            string loadData = File.ReadAllText(path);
            return loadList = loadData.Split(',').ToList();
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {path} with exception {e}");
            loadList = null;
            return null;
        }
    }
}
