public class BlueBall : Ball {

    private void Start() {
        ballStats = BallTypes.instance.GetBallStats(BallType.BLUE);
    }
}
