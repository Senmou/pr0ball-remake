using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetMusicVolume : MonoBehaviour {

    public AudioMixer audioMixer;

    private Slider slider;
    private MusicData musicData;

    private void Awake() {
        slider = GetComponent<Slider>();
        musicData = PersistentData.instance.musicData;
    }

    private void Start() {
        slider.value = musicData.volume;
        SetVolume(musicData.volume);
    }

    public void SetVolume(float sliderValue) {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20f);
        musicData.volume = sliderValue;
    }
}
