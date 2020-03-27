using UnityEngine;
using System.IO;
using System;

public class DebugUI : MonoBehaviour {

    private void OnGUI() {

        if (GUILayout.Button("GameOver")) {
            Score.instance.DecScore((int)Score.instance.score + 1);
        }

        if (GUILayout.Button("DELETE")) {
            DeleteSaveFile();
        }

        if (GUILayout.Button("History")) {
            string list = "";
            foreach(var h in FindObjectOfType<CanvasManager>().canvasHistory) {
                list += h + " - ";
            }
            Debug.Log(list);
        }
    }

    public void DeleteSaveFile() {

        string fileName = GameController.saveFileName;
        string directory = Path.Combine(Application.persistentDataPath, "data");
        string filePath = Path.Combine(directory, fileName);

        try {
            File.Delete(filePath);
            PersistentData.instance = null;
            Application.Quit();
        } catch (Exception ex) {
            Debug.LogException(ex);
        }
    }
}
