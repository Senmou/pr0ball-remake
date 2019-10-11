public class GreenBall : Ball {

    private void Start() {
        ballStats = BallTypes.instance.GetBall(BallColor.GREEN);
    }
}
