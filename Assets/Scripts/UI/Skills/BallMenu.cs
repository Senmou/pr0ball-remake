using TMPro;
using UnityEngine;

public class BallMenu : MonoBehaviour {

    private TextMeshProUGUI damage;
    private TextMeshProUGUI critChance;
    private TextMeshProUGUI critMultiplier;

    private void Awake() {
        damage = transform.FindChild<TextMeshProUGUI>("Damage/Value");
        critChance = transform.FindChild<TextMeshProUGUI>("Crit/Value");
        critMultiplier = transform.FindChild<TextMeshProUGUI>("CritMultiplier/Value");
    }

    private void UpdateUI() {
        damage.text = SO.instance.ballConfig.damage.ToString();
        critChance.text = SO.instance.ballConfig.critChance.ToString();
        critMultiplier.text = SO.instance.ballConfig.critDamageMultiplier.ToString();
    }

    public void Show() {
        GameController.instance.PauseGame();
        gameObject.SetActive(true);
        UpdateUI();
    }

    public void Hide() {
        GameController.instance.ResumeGame();
        gameObject.SetActive(false);
    }
}
