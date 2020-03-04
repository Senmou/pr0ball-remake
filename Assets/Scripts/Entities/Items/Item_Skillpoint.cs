using UnityEngine;

public class Item_Skillpoint : MonoBehaviour {

    private RectTransform rect;
    private EnemyController enemyController;

    private void Awake() {
        rect = GetComponent<RectTransform>();
        enemyController = FindObjectOfType<EnemyController>();
        EventManager.StartListening("ReachedNextLevel", OnReachedNextLevel);
    }

    private void OnReachedNextLevel() {
        enemyController.activeItems.Remove(gameObject);
        Destroy(gameObject);
    }

    public void OnItemCollect() {
        Score.instance.IncSkillPoints(1);
        enemyController.activeItems.Remove(gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ball"))
            OnItemCollect();
    }
}
