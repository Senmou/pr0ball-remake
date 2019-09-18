using System;

[Serializable]
public class SaveData {

    public SfxData sfxData;
    public MusicData musicData;

    public SaveData() {
        sfxData = PersistentData.instance.sfxData;
        musicData = PersistentData.instance.musicData;
    }
}
