using UnityEngine;
using TMPro;

public class BaseItem : MonoBehaviour {

    [HideInInspector] public int value;
    [HideInInspector] public bool reachedStartingPosition;
    [HideInInspector] public CurrentLevelData.EntityType entityType;

    protected SkillBar skillBar;
    protected AudioSource sfxCoins;

    private EnemyController enemyController;

    protected void Awake() {
        skillBar = FindObjectOfType<SkillBar>();
        enemyController = FindObjectOfType<EnemyController>();
        sfxCoins = GameObject.Find("SfxCoins").GetComponent<AudioSource>();
        EventManager.StartListening("ReachedNextLevel", OnReachedNextLevel);
    }

    public void SetValue(int _value, TextMeshProUGUI valueUI = null) {
        value = _value;
        valueUI?.SetText(value.ToString());
    }

    protected void OnItemCollect() {
        OnItemCollected();
        sfxCoins.Play();
        DestroyAndRemoveItem();
    }

    public void DestroyAndRemoveItem() {
        enemyController.activeItems.Remove(this);
        Destroy(gameObject);
    }

    protected void OnReachedNextLevel() {
        Destroy(gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ball"))
            OnItemCollect();
    }

    protected virtual void OnItemCollected() { }
}
