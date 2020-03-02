using UnityEngine;

public class MusicController : MonoBehaviour {

    public static MusicController instance;

    private AudioSource musicAudioSource;
    private AudioClip lastAudioClip;

    private void Awake() {
        instance = this;
        musicAudioSource = transform.Find("Music").GetComponent<AudioSource>();
    }

    public void SetMusic(AudioClip audioClip) {
        lastAudioClip = musicAudioSource.clip;
        musicAudioSource.clip = audioClip;
        musicAudioSource.Play();
    }

    public void RestoreLastMusicClip() {
        musicAudioSource.clip = lastAudioClip;
        musicAudioSource.Play();
    }
}
