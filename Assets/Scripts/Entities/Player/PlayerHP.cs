using UnityEngine;
using TMPro;

public class PlayerHP : MonoBehaviour {

    public int maxHP;

    private int currentHP;
    private TextMeshProUGUI playerHp;

    private void Awake() {
        playerHp = transform.FindChild<TextMeshProUGUI>("Value");

        maxHP = 5;
        currentHP = maxHP;
        UpdateUI();
    }

    public void TakeDamage(int amount) {
        currentHP -= amount;

        if(currentHP <= 0) {
            EventManager.TriggerEvent("FailedLevel");
            currentHP = maxHP;
        }

        UpdateUI();
    }

    private void UpdateUI() {
        playerHp.text = currentHP.ToString();
    }
}
