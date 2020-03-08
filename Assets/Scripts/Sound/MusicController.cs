using System.Collections;
using UnityEngine;

public class MusicController : MonoBehaviour {

    public static MusicController instance;

    private float lastVolume;
    private AudioClip lastAudioClip;
    private AudioSource musicAudioSource;

    private void Awake() {
        instance = this;
        musicAudioSource = transform.Find("Music").GetComponent<AudioSource>();
    }

    public void SetMusic(AudioClip audioClip) {
        lastAudioClip = musicAudioSource.clip;
        musicAudioSource.clip = audioClip;
        musicAudioSource.Play();
    }

    //public void RestoreLastMusicClip() {
    //    musicAudioSource.clip = lastAudioClip;
    //    musicAudioSource.Play();
    //}

    public void SetVolume(float volume) {
        lastVolume = musicAudioSource.volume;
        musicAudioSource.volume = volume;
    }

    public void RestoreLastVolume() {
        musicAudioSource.volume = lastVolume;
    }

    public void ChangeVolumeForSeconds(float volume, float seconds) {
        StartCoroutine(ChangeVolume(volume, seconds));
    }

    private IEnumerator ChangeVolume(float volume, float seconds) {
        float oldVolume = musicAudioSource.volume;
        musicAudioSource.volume = volume;
        yield return new WaitForSecondsRealtime(seconds);
        musicAudioSource.volume = oldVolume;
    }
}
