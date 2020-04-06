using UnityEngine;
using System.IO;
using System;

public class DebugUI : MonoBehaviour {

    public static DebugUI instance;

    private void Awake() {
        instance = this;
    }

    private void OnGUI() {

        if (GUILayout.Button("DELETE")) {
            DeleteSaveFile();
        }

        if (GUILayout.Button("Token")) {
            FindObjectOfType<Skill_Hammertime>().tokenCount += 10;
            FindObjectOfType<Skill_Frogs>().tokenCount += 10;
            FindObjectOfType<Skill_Triggered>().tokenCount += 10;
            Score.instance.IncSkillPoints(10);
        }

        if (GUILayout.Button("Input Name")) {
            CanvasManager.instance.SwitchCanvas(CanvasType.NAME);
        }

        if (GUILayout.Button("GameOver")) {
            Score.instance.DecScore((int)Score.instance.score + 1);
        }

        if (GUILayout.Button("Random Score")) {
            FindObjectOfType<HighscoreController>().PostRandomScore();
        }

        if (GUILayout.Button("Print Scores")) {
            FindObjectOfType<HighscoreController>().GetHighscores();
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
