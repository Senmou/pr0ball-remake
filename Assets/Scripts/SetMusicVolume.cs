using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetMusicVolume : MonoBehaviour {

    public AudioMixer audioMixer;

    private Slider slider;

    private void Awake() {
        slider = GetComponent<Slider>();
    }

    private void Start() {
        float volumePref = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        slider.value = volumePref;
        SetVolume(volumePref);
    }

    public void SetVolume(float sliderValue) {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20f);
    }

    private void OnApplicationPause(bool pause) {
        if (pause) {
            PlayerPrefs.SetFloat("MusicVolume", slider.value);
            PlayerPrefs.Save();
        }
    }
}
