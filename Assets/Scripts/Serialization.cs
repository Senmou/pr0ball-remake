using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;
using System.IO;
using System;

public static class Serialization {

    public static string fileName = "saveData.dat";
    public static string directory = Path.Combine(Application.persistentDataPath, "data");
    public static string filePath = Path.Combine(directory, fileName);

    public static void Save() {

        try {
            SaveData saveData = new SaveData();

            if (!Directory.Exists(directory)) {
                Directory.CreateDirectory(directory);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream file = new FileStream(filePath, FileMode.Create);
            formatter.Serialize(file, saveData);
            file.Close();
            Debug.Log("Saved Data to: " + directory.Replace("/", "\\"));
        } catch (Exception e) {
            Debug.LogWarning("Failed to save data to: " + filePath);
            Debug.LogWarning("Error: " + e.Message);
            throw;
        }

    }

    public static bool Load() {

        if (!File.Exists(filePath))
            return false;

        try {
            
            IFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);
            SaveData saveData = formatter.Deserialize(stream) as SaveData;

            PersistentData.instance.LoadDataFromSaveFile(saveData);

            Debug.Log("Loaded data from: " + filePath);
            stream.Close();
            return true;
        } catch (Exception e) {
            Debug.LogWarning("Failed to load data from: " + filePath.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
            throw e;
        }
    }
}
