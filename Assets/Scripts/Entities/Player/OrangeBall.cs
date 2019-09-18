public class OrangeBall : Ball {

    private void Start() {
        ballStats = BallTypes.instance.GetBallStats(BallType.ORANGE);
    }
}
