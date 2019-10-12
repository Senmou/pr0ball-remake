public class OrangeBall : Ball {

    private void Start() {
        ballStats = ballTypes.GetBall(BallColor.ORANGE);
    }
}
