using UnityEngine;

public class Item_AddBall : MonoBehaviour {

    private EnemyController enemyController;

    private void Awake() {
        enemyController = FindObjectOfType<EnemyController>();
    }

    public void OnItemCollect() {
        BallStats.Instance.OnItemCollected_AddBall();
        enemyController.activeItems.Remove(gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ball"))
            OnItemCollect();
    }
}
