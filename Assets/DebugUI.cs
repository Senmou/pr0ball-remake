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
            FindObjectOfType<Skill_Hammertime>().hasToken = true;
            FindObjectOfType<Skill_Frogs>().hasToken = true;
            FindObjectOfType<Skill_Triggered>().hasToken = true;
            Score.instance.IncSkillPoints(10);
        }

        if (GUILayout.Button("Input Name")) {
            CanvasManager.instance.SwitchCanvas(CanvasType.NAME);
        }

        if (GUILayout.Button("GameOver")) {
            Score.instance.LoseLife();
            Score.instance.LoseLife();
            Score.instance.LoseLife();
        }

        if (GUILayout.Button("Random Score")) {
            FindObjectOfType<HighscoreController>().PostRandomScore();
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
