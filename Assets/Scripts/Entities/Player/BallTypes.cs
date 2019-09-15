using System.Collections.Generic;
using UnityEngine;

public class BallTypes : MonoBehaviour {

    public static BallTypes instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public List<BallStats> ballStats;

    public BallStats GetBallStats(BallType ballType) {
        foreach (var stats in ballStats) {
            if (stats.ballType == ballType)
                return stats;
        }
        return ballStats[0];
    }
}
