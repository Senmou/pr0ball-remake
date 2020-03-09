using UnityEngine;
using TMPro;

public class Item_Skillpoint : MonoBehaviour {

    [HideInInspector] public int value;
    [HideInInspector] public CurrentLevelData.EntityType entityType;

    private RectTransform rect;
    private AudioSource sfxCoins;
    private TextMeshProUGUI valueUI;
    private EnemyController enemyController;

    private void Awake() {
        rect = GetComponent<RectTransform>();
        entityType = CurrentLevelData.EntityType.Item;
        enemyController = FindObjectOfType<EnemyController>();
        valueUI = transform.FindChild<TextMeshProUGUI>("Value");
        sfxCoins = GameObject.Find("SfxCoins").GetComponent<AudioSource>();
        EventManager.StartListening("ReachedNextLevel", OnReachedNextLevel);

        int random = Random.Range(0, 100);
        if (random < 3)
            value = Random.Range(5, 11);
        else
            value = Random.Range(1, 4);

        valueUI.text = value.ToString();
    }

    public void SetValue(int _value) {
        value = _value;
        valueUI.text = value.ToString();
    }

    private void OnReachedNextLevel() {
        Destroy(gameObject);
    }

    public void OnItemCollect() {
        Score.instance.IncSkillPoints(value);
        sfxCoins.Play();
        DestroyAndRemoveItem();
    }

    public void DestroyAndRemoveItem() {
        enemyController.activeItems.Remove(this);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ball"))
            OnItemCollect();
    }
}
