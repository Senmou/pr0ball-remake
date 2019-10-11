public class OrangeBall : Ball {

    private void Start() {
        ballStats = BallTypes.instance.GetBall(BallColor.ORANGE);
    }
}
