using System.Collections;
using UnityEngine;

public class MusicController : MonoBehaviour {

    public static MusicController instance;

    public AudioClip[] musicClips;

    private int index = 0;
    private int Index {
        get => index;
        set {
            index = value;
            if(index >= musicClips.Length) {
                index = index % musicClips.Length;
            }
        }
    }
    private float lastVolume;
    private AudioClip lastAudioClip;
    private AudioSource musicAudioSource;

    private void Awake() {
        instance = this;
        musicAudioSource = transform.Find("Music").GetComponent<AudioSource>();

        Index = Random.Range(0, musicClips.Length);
    }

    private void Start() {
        StartCoroutine(PlayRandomMusic());
    }

    private IEnumerator PlayRandomMusic() {

        bool shouldPlayNextSong = true;

        while (true) {
            if (shouldPlayNextSong) {
                Index++;
                shouldPlayNextSong = false;
                AudioClip nextSong = musicClips[Index];
                SetMusic(nextSong);
                yield return new WaitForSecondsRealtime(nextSong.length);
                shouldPlayNextSong = true;
            }
        }
    }

    public void SetMusic(AudioClip audioClip) {
        lastAudioClip = musicAudioSource.clip;
        musicAudioSource.clip = audioClip;
        musicAudioSource.Play();
    }

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
