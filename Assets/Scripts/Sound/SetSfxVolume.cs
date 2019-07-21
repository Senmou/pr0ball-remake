using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetSfxVolume : MonoBehaviour {

    public Button plus;
    public Button minus;
    public TextMeshProUGUI volumeUI;
    public AudioMixer audioMixer;

    private float maxVolume = 10f;
    private float currentVolume;

    private SfxData sfxData;

    private void OnValidate() {
        currentVolume = Mathf.Clamp(currentVolume, 0f, maxVolume);
    }

    private void Awake() {
        sfxData = PersistentData.instance.sfxData;
        currentVolume = (int)sfxData.volume;
    }

    private void Start() {
        SetVolume(sfxData.volume);
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
        sfxData.volume = volume;
        volumeUI.text = volume.ToString();
    }
}
