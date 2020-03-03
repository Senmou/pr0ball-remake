using UnityEngine;

public class Item_AddBall : MonoBehaviour {

    private EnemyController enemyController;

    private void Awake() {
        enemyController = FindObjectOfType<EnemyController>();
        EventManager.StartListening("ReachedNextLevel", OnReachedNextLevel);
    }

    private void OnReachedNextLevel() {
        enemyController.activeItems.Remove(gameObject);
        Destroy(gameObject);
    }

    public void OnItemCollect() {
        BallStats.Instance.AddBalls();
        enemyController.activeItems.Remove(gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ball"))
            OnItemCollect();
    }
}
