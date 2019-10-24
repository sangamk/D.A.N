using Assets.Engine.DataModels;
using System.IO;
using UnityEngine;

public class JsonReader
{
    private readonly string basePath = "./Assets/Engine/";
    public Planet LoadPlanet(string name)
    {
        string path = basePath + name;
        string json = LoadFile(path);
        Debug.Log("Loaded file:");
        Debug.Log(json);
        if (string.IsNullOrEmpty(json))
        {
            Debug.LogWarning("File not loaded");
            return null;
        }

        Planet planet = JsonUtility.FromJson<Planet>(json);
        Debug.Log("Serialized JSON:");
        Debug.Log(planet);

        return planet;
    }

    private string LoadFile(string path)
    {
        string json = "";
        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
               json = sr.ReadToEnd();
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"The file {path} could not be read");
            Debug.LogError(e.Message);
        }
        return json;
    }
}
