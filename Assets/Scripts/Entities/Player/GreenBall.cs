public class GreenBall : Ball {

    private void Start() {
        ballStats = BallTypes.instance.GetBallStats(BallType.GREEN);
    }
}
