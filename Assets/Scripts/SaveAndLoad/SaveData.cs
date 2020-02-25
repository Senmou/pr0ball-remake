using System;

[Serializable]
public class SaveData {

    public SfxData sfxData;
    public MusicData musicData;
    public ScoreData scoreData;
    public BallData ballData;
    public SkillData skillData;
    public CurrentLevelData currentLevelData;
    public Highscores highscores;

    public bool isGameOver;
    public float elapsedTimeSinceRestart;

    public SaveData() {
        sfxData = PersistentData.instance.sfxData;
        musicData = PersistentData.instance.musicData;
        scoreData = PersistentData.instance.scoreData;
        ballData = PersistentData.instance.ballData;
        skillData = PersistentData.instance.skillData;
        currentLevelData = PersistentData.instance.currentLevelData;
        highscores = PersistentData.instance.highscores;

        isGameOver = PersistentData.instance.isGameOver;
        elapsedTimeSinceRestart = PersistentData.instance.elapsedTimeSinceRestart;
    }
}
