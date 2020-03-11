﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class PersistentData : MonoBehaviour {

    public static PersistentData instance;

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

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        soundData = new SoundData();
        scoreData = new ScoreData();
        ballData = new BallData();
        skillData = new SkillData();
        currentLevelData = new CurrentLevelData();
        highscores = new Highscores();
        statistics = new Statistics();

        Serialization.Load();
    }

    public void LoadDataFromSaveFile(SaveData saveData) {
        soundData = saveData.soundData ?? new SoundData();
        scoreData = saveData.scoreData ?? new ScoreData();
        ballData = saveData.ballData ?? new BallData();
        skillData = saveData.skillData ?? new SkillData();
        currentLevelData = saveData.currentLevelData ?? new CurrentLevelData();
        highscores = saveData.highscores ?? new Highscores();
        statistics = saveData.statistics;
        elapsedTimeSinceRestart = saveData.elapsedTimeSinceRestart;
        isGameOver = saveData.isGameOver;
        uniColor = saveData.uniColor;
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
public class SoundData {

    public float musicVolume;
    public float masterVolume;
    public float sfxBallVolume;
    public float sfxSkillVolume;

    public SoundData() {
        musicVolume = 5f;
        masterVolume = 10f;
        sfxBallVolume = 5f;
        sfxSkillVolume = 5f;
    }
}

[Serializable]
public class ScoreData {
    public long score;
    public long highscore;
    public int skillPoints;

    public ScoreData() {
        score = 0;
        highscore = 0;
        skillPoints = 0;
    }
}

[Serializable]
public class BallData {

    public int damage;
    public float critChance;
    public float critDamage;
    public int ballCount;

    public BallData() {
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
        public int cost;
        public int usedCounter;

        public bool locked;
        public bool usedThisTurn;
    }

    private Skill[] skills;
    public int[] equippedSkillIDs;

    public void SetSkillData(int id, bool locked, int usedCounter, int cost, bool usedThisTurn) {
        skills[id].locked = locked;
        skills[id].usedCounter = usedCounter;
        skills[id].cost = cost;
        skills[id].usedThisTurn = usedThisTurn;
    }

    public Skill GetSkillData(int id) {
        Skill skill = new Skill();
        skill.locked = PersistentData.instance.skillData.skills[id].locked;
        skill.cost = PersistentData.instance.skillData.skills[id].cost;
        skill.usedThisTurn = PersistentData.instance.skillData.skills[id].usedThisTurn;
        return skill;
    }

    public SkillData() {
        skills = new Skill[3];

        // index is skillBarSlot.id
        equippedSkillIDs = new int[3] { -1, -1, -1 };

        int skillCount = 3;
        for (int i = 0; i < skillCount; i++) {
            skills[i].locked = true;
            skills[i].usedCounter = 0;
            skills[i].cost = 1;
            skills[i].usedThisTurn = false;
        }
    }
}

[Serializable]
public class CurrentLevelData {

    [Serializable]
    public enum EntityType {
        Enemy_0,
        Enemy_1,
        Enemy_2,
        Item,
        Enemy_3,
        Enemy_4,
        Enemy_5
    }

    [Serializable]
    public struct EntityData {
        public EntityType entityType;
        public float posX;
        public float posY;

        // hp for enemies, skillPointValue for items
        public int value;
    }

    public int wave;
    public int level;
    public int dangerLevel;
    public int spawnPointIndex;
    public List<EntityData> activeEntities;

    public void AddEntity(EntityType _entityType, float _posX, float _posY, int _value = -1) {
        EntityData entityData = new EntityData { entityType = _entityType, posX = _posX, posY = _posY, value = _value };
        activeEntities.Add(entityData);
    }

    public CurrentLevelData() {
        wave = 1;
        level = 1;
        dangerLevel = 0;
        spawnPointIndex = 0;
        activeEntities = new List<EntityData>();
    }
}

[Serializable]
public class Highscores {

    [Serializable]
    public struct HighscoreEntry {
        public long highscore;
        public string timestamp;
    }

    public List<HighscoreEntry> entries;

    public Highscores() {
        entries = new List<HighscoreEntry>();
    }

    public void AddHighscore(long _highscore, string _timestamp) {
        HighscoreEntry entry = new HighscoreEntry { highscore = _highscore, timestamp = _timestamp };
        entries.Add(entry);

        entries = entries.OrderByDescending(e => e.highscore).ToList();

        for (int i = 0; i < entries.Count; i++) {
            if (i > 10)
                entries.RemoveAt(i);
        }
    }
}