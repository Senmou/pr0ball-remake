using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetSfxVolume : MonoBehaviour {

    public Button plus;
    public Button minus;
    public TextMeshProUGUI volumeUI;
    public AudioMixer audioMixer;

    public float CurrentVolume { get => currentVolume; }

    private float maxVolume = 10f;
    private float currentVolume;

    private void OnValidate() {
        currentVolume = Mathf.Clamp(currentVolume, 0f, maxVolume);
    }

    private void Awake() {
        currentVolume = (int)PersistentData.instance.sfxData.volume;
        EventManager.StartListening("SaveGame", OnSaveGame);
    }

    private void OnSaveGame() {
        PersistentData.instance.sfxData.volume = currentVolume;
    }

    private void Start() {
        SetVolume(currentVolume);
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

    public void SetVolume(float volume) {
        float value = volume.Map(0f, maxVolume, -40f, 0f);
        value = currentVolume <= 0f ? -80f : value;
        audioMixer.SetFloat("SfxVolume", value);
        volumeUI.text = volume.ToString();
    }
}
