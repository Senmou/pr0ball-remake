using TMPro;
using UnityEngine;

public class GetMusicTitle : MonoBehaviour {

    [SerializeField] private AudioSource musicAudioSource;

    private TextMeshProUGUI musicTitleUI;

    private void Awake() {
        musicTitleUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        musicTitleUI.text = musicAudioSource.clip.name;
    }
}
