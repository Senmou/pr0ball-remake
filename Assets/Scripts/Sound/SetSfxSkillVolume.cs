using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SetSfxSkillVolume : MonoBehaviour {

    public Button plus;
    public Button minus;
    public AudioMixer audioMixer;
    public TextMeshProUGUI volumeUI;

    private float maxVolume = 10f;
    private float currentVolume;
    private AudioSource audioSourceSkill_1;

    private void OnValidate() {
        currentVolume = Mathf.Clamp(currentVolume, 0f, maxVolume);
    }

    private void Awake() {
        audioSourceSkill_1 = GameObject.Find("Skill_Hammertime").GetComponent<AudioSource>();
        currentVolume = (int)PersistentData.instance.soundData.sfxSkillVolume;
        EventManager.StartListening("SaveGame", OnSaveGame);
    }

    private void OnSaveGame() {
        PersistentData.instance.soundData.sfxSkillVolume = currentVolume;
    }

    private void Start() {
        SetVolume(currentVolume);
        CheckButtonInteractability();
    }

    public void OnClickPlus() {

        if (currentVolume < 10) {
            currentVolume++;
            audioSourceSkill_1.Play();
        }

        SetVolume(currentVolume);
        CheckButtonInteractability();
    }

    public void OnClickMinus() {

        if (currentVolume > 0) {
            currentVolume--;
            audioSourceSkill_1.Play();
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
        audioMixer.SetFloat("sfxSkillVolume", value);
        volumeUI.text = volume.ToString();
    }
}
