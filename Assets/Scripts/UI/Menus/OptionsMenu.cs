using UnityEngine.UI;
using UnityEngine;

public class OptionsMenu : CanvasController {

    private MoveUI moveUI;
    private Toggle toggle;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        toggle = transform.FindChild<Toggle>("MiscOptions/Toggle_UniColor");
        toggle.isOn = PersistentData.instance.uniColor;
    }

    public void OnValueChanged(bool value) {
        PersistentData.instance.uniColor = value;
        EventManager.TriggerEvent("ToggleUniColor");
    }

    public override void Show() {
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public override void Hide() {
        moveUI.FadeTo(new Vector2(-30f, 0f), 0.5f, true);
    }
}
