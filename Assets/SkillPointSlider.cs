using UnityEngine.UI;
using UnityEngine;

public class SkillPointSlider : MonoBehaviour {

    [SerializeField] private FloatingText floatingText;

    private Slider slider;
    private Canvas canvas;
    
    private void Awake() {
        slider = GetComponent<Slider>();
        canvas = FindObjectOfType<Canvas>();
        EventManager.StartListening("EnemyDied", OnEnemyDied);
    }

    private void OnEnemyDied() {
        if (slider.value < slider.maxValue)
            slider.value++;

        if(slider.value == slider.maxValue) {
            slider.value = 0;
            Score.instance.IncSkillPoints(1);
            GameObject go = Instantiate(floatingText, new Vector2(4.3f, 22f), Quaternion.identity).gameObject;
            go.GetComponent<FloatingText>().SetText("+1 Skillpunkt");
            go.transform.parent = canvas.transform;
        }
    }
}
