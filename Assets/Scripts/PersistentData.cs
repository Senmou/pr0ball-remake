using UnityEngine;
using System;

public class PersistentData : MonoBehaviour {

    public static PersistentData instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        Serialization.Load();
    }

    public SfxData sfxData = new SfxData();
    public MusicData musicData = new MusicData();
    public ScoreData scoreData = new ScoreData();

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
