using UnityEngine;
using System.IO;
using System;

public class ResetGameButton : MonoBehaviour {

    public void ResetGame() {

        string fileName = "saveData.dat";
        string directory = Path.Combine(Application.persistentDataPath, "data");
        string filePath = Path.Combine(directory, fileName);

        try {
            File.Delete(filePath);

            PersistentData.instance.soundData = null;
            PersistentData.instance.scoreData = null;
            PersistentData.instance.ballData = null;
            PersistentData.instance.skillData = null;
            PersistentData.instance.currentLevelData = null;
            PersistentData.instance.highscores = null;
            PersistentData.instance.statistics = null;

            Application.Quit();
        } catch (Exception ex) {
            Debug.LogException(ex);
        }
    }
}
