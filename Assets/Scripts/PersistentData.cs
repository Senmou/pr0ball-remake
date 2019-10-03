using UnityEngine;
using System;

public class PersistentData : MonoBehaviour {

    public static PersistentData instance;

    public SfxData sfxData;
    public MusicData musicData;
    public ScoreData scoreData;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        sfxData = new SfxData();
        musicData = new MusicData();
        scoreData = new ScoreData();

        Serialization.Load();
    }

    public void LoadDataFromSaveFile(SaveData saveData) {
        sfxData = saveData.sfxData;
        musicData = saveData.musicData;
        scoreData = saveData.scoreData;
    }

    private void OnApplicationFocus(bool focus) {
        if (!focus) {
            Serialization.Save();
        }
    }

    private void OnApplicationPause(bool pause) {
        if (pause) {
            Serialization.Save();
        }
    }

    private void OnApplicationQuit() {
        Serialization.Save();
    }
}

[Serializable]
public class SfxData {
    public float volume;
}

[Serializable]
public class MusicData {
    public float volume;
}

[Serializable]
public class ScoreData {
    public int score;
}