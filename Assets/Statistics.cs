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

    public void OnChacheData() {
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
        public int SkillsUsedTotal { get => skill_1.used + skill_2.used + skill_3.used; }
        public int SkillsDamageDealtTotal { get => skill_1.damageDealt + skill_2.damageDealt + skill_3.damageDealt; }

        [Serializable]
        public struct Skill {
            public int used;
            public int damageDealt;
        }
    }

    public float DamageDealtPercentage_Skill_1 { get => skills.SkillsDamageDealtTotal > 0 ? (float)skills.skill_1.damageDealt / skills.SkillsDamageDealtTotal : 0f; }
    public float DamageDealtPercentage_Skill_2 { get => skills.SkillsDamageDealtTotal > 0 ? (float)skills.skill_2.damageDealt / skills.SkillsDamageDealtTotal : 0f; }
    public float DamageDealtPercentage_Skill_3 { get => skills.SkillsDamageDealtTotal > 0 ? (float)skills.skill_3.damageDealt / skills.SkillsDamageDealtTotal : 0f; }

    [Serializable]
    public struct BenitratorStatistics {
        public int plays;
        public int wins;
        public int loses;
        public int totalBets;
        public float AverageBet {
            get {
                if (plays > 0)
                    return (float)totalBets / plays;
                else
                    return 0;
            }
        }
        public float WinsPercentage { get => (wins + loses) > 0 ? (float)wins / (wins + loses) : 0f; }
        public float LosesPercentage { get => (wins + loses) > 0 ? (float)loses / (wins + loses) : 0f; }
    }

    [Serializable]
    public struct GameStatistics {
        public int TotalSkillPointsSpend { get => Instance.skills.skillPointsSpend + Instance.benitrator.totalBets; }
    }
}
