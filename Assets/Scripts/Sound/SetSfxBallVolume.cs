using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SetSfxBallVolume : MonoBehaviour {

    public Button plus;
    public Button minus;
    public AudioMixer audioMixer;
    public TextMeshProUGUI volumeUI;

    private float maxVolume = 10f;
    private float currentVolume;
    private Sound ballSound;
    private AudioSource ballAudioSource;

    private void OnValidate() {
        currentVolume = Mathf.Clamp(currentVolume, 0f, maxVolume);
    }

    private void Awake() {
        ballSound = FindObjectOfType<Sound>();
        ballAudioSource = GameObject.Find("SfxBounce").GetComponent<AudioSource>();
        currentVolume = (int)PersistentData.instance.soundData.sfxBallVolume;
        EventManager.StartListening("SaveGame", OnSaveGame);
    }

    private void OnSaveGame() {
        PersistentData.instance.soundData.sfxBallVolume = currentVolume;
    }

    private void Start() {
        SetVolume(currentVolume);
        CheckButtonInteractability();
    }

    public float GetCurrentVolume() => currentVolume;

    public void OnClickPlus() {

        if (currentVolume < 10) {
            currentVolume++;
            PlayBallSound();
        }

        SetVolume(currentVolume);
        CheckButtonInteractability();
    }

    public void OnClickMinus() {

        if (currentVolume > 0) {
            currentVolume--;
            PlayBallSound();
        }

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
        audioMixer.SetFloat("sfxBallVolume", value);
        volumeUI.text = volume.ToString();
    }

    private void PlayBallSound() {
#if UNITY_ANDROID && !UNITY_EDITOR
            ballSound.Bounce();
#else
        ballAudioSource.PlayOneShot(ballAudioSource.clip);
#endif
    }
}
