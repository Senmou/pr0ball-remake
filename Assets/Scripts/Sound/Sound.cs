using UnityEngine;

public struct SoundFile {
    public int fileID;
    public string path;
}

public class Sound : MonoBehaviour {

    private SoundFile bounce;
    private int maxNumStreams = 5;

    private bool soundLock;
    private float soundLockTimer;
    private float soundLockDuration;

    private SetSfxVolume sfxVolume;

    private void Awake() {

        sfxVolume = FindObjectOfType<SetSfxVolume>();

        soundLock = false;
        soundLockDuration = 0.07f;
        soundLockTimer = 0.07f;

        bounce = new SoundFile();
        bounce.path = "Sound/Sfx/Bounce.wav";

        AndroidNativeAudio.makePool(maxNumStreams);
    }

    private void Start() {
        bounce.fileID = AndroidNativeAudio.load(bounce.path);
    }

    private void Update() {
        if (soundLockTimer > 0f)
            soundLockTimer -= Time.deltaTime;
        else
            soundLock = false;

    }

    public void Bounce() {
        if (!soundLock) {
            AndroidNativeAudio.play(bounce.fileID, sfxVolume.CurrentVolume / 10f);
            soundLockTimer = soundLockDuration;
            soundLock = true;
        }
    }
}
