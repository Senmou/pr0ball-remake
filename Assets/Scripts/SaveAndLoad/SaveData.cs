using System;

[Serializable]
public class SaveData {

    public SoundData soundData;
    public ScoreData scoreData;
    public BallData ballData;
    public SkillData skillData;
    public CurrentLevelData currentLevelData;
    public Highscores highscores;
    public Statistics statistics;

    public bool uniColor;
    public bool isGameOver;
    public float elapsedTimeSinceRestart;

    public SaveData() {
        soundData = PersistentData.instance.soundData;
        scoreData = PersistentData.instance.scoreData;
        ballData = PersistentData.instance.ballData;
        skillData = PersistentData.instance.skillData;
        currentLevelData = PersistentData.instance.currentLevelData;
        highscores = PersistentData.instance.highscores;
        statistics = PersistentData.instance.statistics;

        uniColor = PersistentData.instance.uniColor;
        isGameOver = PersistentData.instance.isGameOver;
        elapsedTimeSinceRestart = PersistentData.instance.elapsedTimeSinceRestart;
    }
}
