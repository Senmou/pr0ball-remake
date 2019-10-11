using UnityEngine;
using System;

public class PersistentData : MonoBehaviour {

    public static PersistentData instance;

    public SfxData sfxData;
    public MusicData musicData;
    public ScoreData scoreData;
    public BallData ballData;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        sfxData = new SfxData();
        musicData = new MusicData();
        scoreData = new ScoreData();
        ballData = new BallData();

        Serialization.Load();
    }

    public void LoadDataFromSaveFile(SaveData saveData) {
        sfxData = saveData.sfxData;
        musicData = saveData.musicData;
        scoreData = saveData.scoreData;
        ballData = saveData.ballData;
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

    public SfxData(){
        volume = 5;
    }
}

[Serializable]
public class MusicData {
    public float volume;

    public MusicData() {
        volume = 5;
    }
}

[Serializable]
public class ScoreData {
    public int score;

    public ScoreData() {
        score = 0;
    }
}

[Serializable]
public class BallData {
    public int blueBallLevel;

    public BallData() {
        blueBallLevel = 1;
    }
}