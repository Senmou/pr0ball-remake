using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGameButton : MonoBehaviour {

    public void ResetGame() {

        string fileName = "saveData.dat";
        string directory = Path.Combine(Application.persistentDataPath, "data");
        string filePath = Path.Combine(directory, fileName);

        try {
            File.Delete(filePath);

            PersistentData.instance.sfxData = null;
            PersistentData.instance.musicData = null;
            PersistentData.instance.scoreData = null;
            PersistentData.instance.ballData = null;

            Application.Quit();
        } catch (Exception ex) {
            Debug.LogException(ex);
        }
    }
}
