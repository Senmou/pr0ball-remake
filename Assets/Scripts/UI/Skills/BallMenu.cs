using TMPro;
using UnityEngine;

public class BallMenu : MonoBehaviour {

    private TextMeshProUGUI damage;
    private TextMeshProUGUI critChance;
    private TextMeshProUGUI critMultiplier;
    private BallStats ballStats;

    private void Awake() {
        ballStats = FindObjectOfType<BallStats>();
        damage = transform.FindChild<TextMeshProUGUI>("Damage/Value");
        critChance = transform.FindChild<TextMeshProUGUI>("Crit/Value");
        critMultiplier = transform.FindChild<TextMeshProUGUI>("CritMultiplier/Value");
    }

    private void UpdateUI() {
        damage.text = ballStats.Damage().ToString();
        critChance.text = ballStats.CritChance.ToString();
        critMultiplier.text = ballStats.CritDamageMultiplier.ToString();
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
