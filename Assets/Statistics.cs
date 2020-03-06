public static class Statistics {

    public static BallStatistics balls;
    public static BenitratorStatistics benitrator;

    public struct BallStatistics {
        public int fired;
        public int crits;
        public int collisions;
        public int damageDealt;
    }

    public struct BenitratorStatistics {
        public int plays;
        public int wins;
        public int loses;
        public int totalBets;
        public float AverageBet { get => (float)totalBets / plays; }
    }
}
