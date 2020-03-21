using UnityEngine.UI;
using UnityEngine;

public class SkillPointSlider : MonoBehaviour {

    public int killsNeeded;
    public int skillPointsEarned;

    [SerializeField] private FloatingText floatingText;

    private Slider slider;
    private Canvas canvas;
    
    private void Awake() {
        slider = GetComponent<Slider>();
        canvas = FindObjectOfType<Canvas>();

        slider.maxValue = killsNeeded;

        EventManager.StartListening("EnemyDied", OnEnemyDied);
    }

    private void OnEnemyDied() {
        if (slider.value < slider.maxValue)
            slider.value++;

        if(slider.value == slider.maxValue) {
            slider.value = 0;
            Score.instance.IncSkillPoints(skillPointsEarned);
            GameController.instance.SpawnFloatingText("+" + skillPointsEarned + " Skillpunkte", new Vector2(4.3f, 22f));
            EventManager.TriggerEvent("SkillPointGained");
        }
    }

    public void ResetData() {
        slider.value = 0;
    }
}
