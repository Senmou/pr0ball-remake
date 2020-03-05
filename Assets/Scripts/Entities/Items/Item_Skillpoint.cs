using UnityEngine;

public class Item_Skillpoint : MonoBehaviour {

    [HideInInspector] public CurrentLevelData.EntityType entityType;

    private RectTransform rect;
    private AudioSource sfxCoins;
    private EnemyController enemyController;

    private void Awake() {
        rect = GetComponent<RectTransform>();
        entityType = CurrentLevelData.EntityType.Item;
        enemyController = FindObjectOfType<EnemyController>();
        sfxCoins = GameObject.Find("SfxCoins").GetComponent<AudioSource>();
        EventManager.StartListening("ReachedNextLevel", OnReachedNextLevel);
    }

    private void OnReachedNextLevel() {
        enemyController.activeItems.Remove(this);
        Destroy(gameObject);
    }

    public void OnItemCollect() {
        Score.instance.IncSkillPoints(1);
        enemyController.activeItems.Remove(this);
        sfxCoins.Play();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ball"))
            OnItemCollect();
    }
}
