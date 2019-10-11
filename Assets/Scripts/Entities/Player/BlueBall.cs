public class BlueBall : Ball {

    private void Start() {
        ballStats = BallTypes.instance.GetBall(BallColor.BLUE);
    }
}
