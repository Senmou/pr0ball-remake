using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetSfxVolume : MonoBehaviour {

    public AudioMixer audioMixer;

    private Slider slider;

    private void Awake() {
        slider = GetComponent<Slider>();
    }

    private void Start() {
        float volumePref = PlayerPrefs.GetFloat("SfxVolume", 0.5f);
        slider.value = volumePref;
        SetVolume(volumePref);
    }

    public void SetVolume(float sliderValue) {
        audioMixer.SetFloat("SfxVolume", Mathf.Log10(sliderValue) * 20f);
    }

    private void OnApplicationPause(bool pause) {
        if (pause) {
            PlayerPrefs.SetFloat("SfxVolume", slider.value);
            PlayerPrefs.Save();
        }
    }
}
