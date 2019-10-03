﻿using System;

[Serializable]
public class SaveData {

    public SfxData sfxData;
    public MusicData musicData;
    public ScoreData scoreData;

    public SaveData() {
        sfxData = PersistentData.instance.sfxData;
        musicData = PersistentData.instance.musicData;
        scoreData = PersistentData.instance.scoreData;
    }
}
