public class GreenBall : Ball {

    private void Start() {
        ballStats = ballTypes.GetBall(BallColor.GREEN);
    }
}
