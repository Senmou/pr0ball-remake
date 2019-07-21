using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetMusicVolume : MonoBehaviour {

    public Button plus;
    public Button minus;
    public TextMeshProUGUI volumeUI;
    public AudioMixer audioMixer;

    private float maxVolume = 10f;
    private float currentVolume;

    private MusicData musicData;

    private void OnValidate() {
        currentVolume = Mathf.Clamp(currentVolume, 0f, maxVolume);
    }

    private void Awake() {
        musicData = PersistentData.instance.musicData;
        currentVolume = (int)musicData.volume;
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
        minus.interactable = !(currentVolume <= 0);
        plus.interactable = !(currentVolume >= maxVolume);
    }

    private void SetVolume(float volume) {
        float value = volume.Map(0f, maxVolume, -40f, 0f);
        value = currentVolume <= 0f ? -80f : value;
        audioMixer.SetFloat("MusicVolume", value);
        musicData.volume = volume;
        volumeUI.text = volume.ToString();
    }
}
