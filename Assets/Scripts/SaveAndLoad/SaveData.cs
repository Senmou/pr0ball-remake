using System;

[Serializable]
public class SaveData {

    public BallData ballData;
    public SoundData soundData;
    public ScoreData scoreData;
    public SkillData skillData;
    public Highscores highscores;
    public Statistics statistics;
    public CurrentLevelData currentLevelData;

    public bool vSync;
    public int targetFPS;
    public bool uniColor;
    public bool isGameOver;
    public bool enableBloom;
    public bool screenShake;
    public long backupOffset;
    public string playerName;
    public bool firstAppStart;
    public bool blackBackground;
    public bool enableParticleSystems;
    public bool privacyPolicyAgreement;
    public float elapsedTimeSinceRestart;
    public string latestDataPrivacyPolicy;
    public bool benitratorWithoutAnimation;

    public SaveData() {
        ballData = PersistentData.instance.ballData;
        soundData = PersistentData.instance.soundData;
        scoreData = PersistentData.instance.scoreData;
        skillData = PersistentData.instance.skillData;
        highscores = PersistentData.instance.highscores;
        statistics = PersistentData.instance.statistics;
        currentLevelData = PersistentData.instance.currentLevelData;

        vSync = PersistentData.instance.vSync;
        uniColor = PersistentData.instance.uniColor;
        targetFPS = PersistentData.instance.targetFPS;
        isGameOver = PersistentData.instance.isGameOver;
        playerName = PersistentData.instance.playerName;
        screenShake = PersistentData.instance.screenShake;
        enableBloom = PersistentData.instance.enableBloom;
        backupOffset = PersistentData.instance.backupOffset;
        firstAppStart = PersistentData.instance.firstAppStart;
        blackBackground = PersistentData.instance.blackBackground;
        enableParticleSystems = PersistentData.instance.enableParticleSystems;
        privacyPolicyAgreement = PersistentData.instance.privacyPolicyAgreement;
        elapsedTimeSinceRestart = PersistentData.instance.elapsedTimeSinceRestart;
        latestDataPrivacyPolicy = PersistentData.instance.latestDataPrivacyPolicy;
        benitratorWithoutAnimation = PersistentData.instance.benitratorWithoutAnimation;
    }
}
