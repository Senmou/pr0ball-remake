using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetSfxVolume : MonoBehaviour {

    public AudioMixer audioMixer;

    private Slider slider;
    private SfxData sfxData;

    private void Awake() {
        slider = GetComponent<Slider>();
        sfxData = PersistentData.instance.sfxData;
    }

    private void Start() {
        slider.value = sfxData.volume;
        SetVolume(sfxData.volume);
    }

    public void SetVolume(float sliderValue) {
        audioMixer.SetFloat("SfxVolume", Mathf.Log10(sliderValue) * 20f);
        sfxData.volume = sliderValue;
    }
}
