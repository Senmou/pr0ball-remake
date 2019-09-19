using UnityEngine;
using TMPro;

public class PlayerHP : MonoBehaviour {

    public int maxHP;

    private int currentHP;
    private TextMeshProUGUI playerHp;

    public int CurrentHP {
        get => currentHP;
    }

    private void Awake() {
        playerHp = transform.FindChild<TextMeshProUGUI>("Value");

        maxHP = 10;
        currentHP = PersistentData.instance.playerData.hp == 0 ? maxHP : PersistentData.instance.playerData.hp;
        UpdateUI();
    }

    public void TakeDamage(int amount) {
        currentHP -= amount;

        if (currentHP <= 0) {
            EventManager.TriggerEvent("FailedLevel");
            currentHP = maxHP;
        }

        UpdateUI();
    }

    private void UpdateUI() {
        playerHp.text = currentHP.ToString();
        PersistentData.instance.playerData.hp = currentHP;
    }
}
