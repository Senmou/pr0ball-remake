using UnityEngine;
using System;

public class PersistentData : MonoBehaviour {

    public static PersistentData instance;

    public SfxData sfxData;
    public MusicData musicData;
    public ScoreData scoreData;
    public BallData ballData;
    public SkillData skillData;
    public CurrentLevelData currentLevelData;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        sfxData = new SfxData();
        musicData = new MusicData();
        scoreData = new ScoreData();
        ballData = new BallData();
        skillData = new SkillData();
        currentLevelData = new CurrentLevelData();

        Serialization.Load();
    }

    public void LoadDataFromSaveFile(SaveData saveData) {
        sfxData = saveData.sfxData ?? new SfxData();
        musicData = saveData.musicData ?? new MusicData();
        scoreData = saveData.scoreData ?? new ScoreData();
        ballData = saveData.ballData ?? new BallData();
        skillData = saveData.skillData ?? new SkillData();
        currentLevelData = saveData.currentLevelData ?? new CurrentLevelData();
    }

    private void OnApplicationFocus(bool focus) {
        if (!focus) {
            Serialization.Save();
        }
    }

    private void OnApplicationPause(bool pause) {
        if (pause) {
            Serialization.Save();
        }
    }

    private void OnApplicationQuit() {
        Serialization.Save();
    }
}

[Serializable]
public class SfxData {
    public float volume;

    public SfxData() {
        volume = 5;
    }
}

[Serializable]
public class MusicData {
    public float volume;

    public MusicData() {
        volume = 5;
    }
}

[Serializable]
public class ScoreData {
    public int score;
    public int highscore;
    public int skillPoints;

    public ScoreData() {
        score = 0;
        highscore = 0;
        skillPoints = 0;
    }
}

[Serializable]
public class BallData {
    public int level;

    public int damage;
    public float critChance;
    public float critDamage;
    public int ballCount;

    public BallData() {
        level = 1;

        damage = 1;
        critChance = 0f;
        critDamage = 2f;
        ballCount = 1;
    }
}

[Serializable]
public class SkillData {

    [Serializable]
    public struct Skill {
        public int level;
        public bool locked;
        public int remainingCoolDown;
    }

    private Skill[] skills;
    public int[] equippedSkillIDs;

    public void SetSkillData(int id, int level, bool locked, int remainingCoolDown) {
        skills[id].level = level;
        skills[id].locked = locked;
        skills[id].remainingCoolDown = remainingCoolDown;
    }

    public Skill GetSkillData(int id) {
        Skill skill = new Skill();
        skill.level = PersistentData.instance.skillData.skills[id].level;
        skill.locked = PersistentData.instance.skillData.skills[id].locked;
        skill.remainingCoolDown = PersistentData.instance.skillData.skills[id].remainingCoolDown;
        return skill;
    }

    public SkillData() {
        skills = new Skill[16];

        // index is skillBarSlot.id
        equippedSkillIDs = new int[4] { -1, -1, -1, -1 };

        int skillCount = 16;
        for (int i = 0; i < skillCount; i++) {
            skills[i].level = 1;
            skills[i].locked = true;
        }
    }
}

[Serializable]
public class CurrentLevelData {

    public int level;

    public CurrentLevelData() {
        level = 1;
    }
}
