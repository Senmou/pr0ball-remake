using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetMusicVolume : MonoBehaviour {

    public Button plus;
    public Button minus;
    public TextMeshProUGUI volumeUI;
    public AudioMixer audioMixer;

    private int maxVolume = 10;
    private int currentVolume;

    private MusicData musicData;

    private void OnValidate() {
        currentVolume = Mathf.Clamp(currentVolume, 0, 10);
    }

    private void Awake() {
        musicData = PersistentData.instance.musicData;
        currentVolume = musicData.volume;
    }

    private void Start() {
        SetVolume(musicData.volume);
        CheckButtonInteractability();
    }

    public void OnClickPlus() {

        if (currentVolume < 10)
            currentVolume++;

        SetVolume(currentVolume);
        CheckButtonInteractability();
    }

    public void OnClickMinus() {

        if (currentVolume > 0)
            currentVolume--;

        SetVolume(currentVolume);
        CheckButtonInteractability();
    }

    private void CheckButtonInteractability() {
        minus.interactable = !(currentVolume == 0);
        plus.interactable = !(currentVolume == maxVolume);
    }

    private void SetVolume(int volume) {
        float value = Remap(volume);
        audioMixer.SetFloat("MusicVolume", value);
        musicData.volume = volume;
        volumeUI.text = volume.ToString();
    }

    // volume goes from 0 to 10, but AudioMixer works with -80 to 0
    private float Remap(int value) => value / 10f * 80f - 80f;

}
