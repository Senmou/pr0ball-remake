using System.Collections.Generic;
using UnityEngine;

public class BallTypes : MonoBehaviour {
    
    public List<BallStats> balls;

    public BallStats GetBall(BallColor ballColor) {
        foreach (var stats in balls) {
            if (stats.ballColor == ballColor)
                return stats;
        }
        Debug.LogWarning("Ball with color [" + ballColor + "] not found. Returned first ball.");
        return balls[0];
    }
}
