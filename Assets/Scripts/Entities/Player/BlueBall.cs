public class BlueBall : Ball {

    private void Start() {
        ballStats = ballTypes.GetBall(BallColor.BLUE);
    }
}
