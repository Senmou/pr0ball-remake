using UnityEngine;

public class PersistentData : MonoBehaviour {

    public static PersistentData instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        LoadAll();
    }

    public SfxData sfxData = new SfxData();
    public MusicData musicData = new MusicData();

    private void LoadAll() {
        LoadSfxData();
        LoadMusicData();
    }

    private void SaveAll() {
        SaveSfxData();
        SaveMusicData();
    }

    private void LoadSfxData() {
        sfxData.volume = PlayerPrefs.GetFloat("SfxVolume", 0.5f);
    }

    private void SaveSfxData() {
        PlayerPrefs.SetFloat("SfxVolume", sfxData.volume);
    }

    private void LoadMusicData() {
        musicData.volume = PlayerPrefs.GetInt("MusicVolume", 5);
    }

    private void SaveMusicData() {
        PlayerPrefs.SetInt("MusicVolume", musicData.volume);
    }

    private void OnApplicationFocus(bool focus) {
        if (!focus) {
            SaveAll();
        }
    }

    private void OnApplicationPause(bool pause) {
        if (pause) {
            SaveAll();
        }
    }

    private void OnApplicationQuit() {
        SaveAll();
    }
}

public class SfxData {

    public float volume;
}

public class MusicData {

    public int volume;
}
