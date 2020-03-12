using UnityEngine.UI;
using UnityEngine;

public class OptionsMenu : CanvasController {

    private MoveUI moveUI;
    private Toggle toggleUniColor;
    private Toggle toggleParticleSystems;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        toggleUniColor = transform.FindChild<Toggle>("MiscOptions/Toggle_UniColor");
        toggleParticleSystems = transform.FindChild<Toggle>("MiscOptions/Toggle_ParticleSystems");
        
        if (PersistentData.instance.firstAppStart) {
            toggleUniColor.isOn = false;
            toggleParticleSystems.isOn = true;

            PersistentData.instance.uniColor = false;
            PersistentData.instance.enableParticleSystems = true;
        } else {
            toggleUniColor.isOn = PersistentData.instance.uniColor;
            toggleParticleSystems.isOn = PersistentData.instance.enableParticleSystems;
        }
    }

    public void OnValueChanged_UniColor(bool value) {
        PersistentData.instance.uniColor = value;
        EventManager.TriggerEvent("ToggleUniColor");
    }

    public void OnValueChanged_Particlesystems(bool value) {
        PersistentData.instance.enableParticleSystems = value;
        EventManager.TriggerEvent("ToggleParticleSystems");
    }

    public override void Show() {
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public override void Hide() {
        moveUI.FadeTo(new Vector2(-30f, 0f), 0.5f, true);
    }
}
