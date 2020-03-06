public static class Statistics {

    public static BenitratorStats benitrator;

    public struct BenitratorStats {
        public int plays;
        public int wins;
        public int loses;
        public int totalBets;
        public float AverageBet { get => (float)totalBets / plays; }
    }
}
