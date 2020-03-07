using System;

[Serializable]
public class Statistics {

    private static Statistics instance;
    public static Statistics Instance {
        get {
            if (instance == null)
                instance = new Statistics();
            return instance;
        }
    }

    public GameStatistics game;
    public BallStatistics balls;
    public SkillStatistics skills;
    public EnemyStatistics enemies;
    public BenitratorStatistics benitrator;

    public void OnLoadGame() {
        instance = PersistentData.instance.statistics;
    }

    public void OnSaveGame() {
        PersistentData.instance.statistics = Instance;
    }

    public void ResetData() {
        instance = new Statistics();
    }

    [Serializable]
    public struct BallStatistics {
        public int fired;
        public int crits;
        public int collisions;
        public int damageDealt;
    }

    [Serializable]
    public struct EnemyStatistics {
        public int killed;
        public int spawned;
    }

    [Serializable]
    public struct SkillStatistics {

        public Skill skill_1;
        public Skill skill_2;
        public Skill skill_3;

        public int skillPointsSpend;

        [Serializable]
        public struct Skill {
            public int used;
            public int damageDealt;
        }
    }

    [Serializable]
    public struct BenitratorStatistics {
        public int plays;
        public int wins;
        public int loses;
        public int totalBets;
        public float AverageBet { get => (float)totalBets / plays; }
    }

    [Serializable]
    public struct GameStatistics {
        public int TotalSkillPointsSpend { get => Instance.skills.skillPointsSpend + Instance.benitrator.totalBets; }
    }
}
