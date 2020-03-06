public static class Statistics {

    public static GameStatistics game;
    public static BallStatistics balls;
    public static SkillStatistics skills;
    public static EnemyStatistics enemies;
    public static BenitratorStatistics benitrator;

    public static void OnLoadGame() {

    }

    public static void OnSaveGame() {

    }

    public static void ResetData() {

    }

    public struct BallStatistics {
        public int fired;
        public int crits;
        public int collisions;
        public int damageDealt;
    }

    public struct EnemyStatistics {
        public int killed;
        public int spawned;
    }

    public struct SkillStatistics {

        public Skill skill_1;
        public Skill skill_2;
        public Skill skill_3;

        public int skillPointsSpend;

        public struct Skill {
            public int used;
            public int damageDealt;
        }
    }

    public struct BenitratorStatistics {
        public int plays;
        public int wins;
        public int loses;
        public int totalBets;
        public float AverageBet { get => (float)totalBets / plays; }
    }

    public struct GameStatistics {
        public int TotalSkillPointsSpend { get => skills.skillPointsSpend + benitrator.totalBets; }
    }
}
