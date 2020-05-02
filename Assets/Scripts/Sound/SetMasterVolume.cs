using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SetMasterVolume : MonoBehaviour {

    public Button plus;
    public Button minus;
    public AudioMixer audioMixer;
    public TextMeshProUGUI volumeUI;

    private float currentVolume;
    private float maxVolume = 10f;

    private void OnValidate() {
        currentVolume = Mathf.Clamp(currentVolume, 0f, maxVolume);
    }

    private void Awake() {
        currentVolume = (int)PersistentData.instance.soundData.masterVolume;

        EventManager.StartListening("SaveGame", OnSaveGame);
    }

    private void OnSaveGame() {
        PersistentData.instance.soundData.masterVolume = currentVolume;
    }

    private void Start() {
        SetVolume(currentVolume);
        CheckButtonInteractability();
    }

    public float GetCurrentVolume() => currentVolume;

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
        audioMixer.SetFloat("masterVolume", value);
        volumeUI.text = volume.ToString();
    }
}
