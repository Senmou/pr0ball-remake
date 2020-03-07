using UnityEngine;

public class StatisticDebugOutput : MonoBehaviour {

    public int collisions;
    public float averageBet;

    private void Update() {
        collisions = Statistics.Instance.balls.collisions;
        averageBet = Statistics.Instance.benitrator.AverageBet;
    }
}
