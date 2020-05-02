using UnityEngine.UI;
using UnityEngine;

public class OptionsMenu : CanvasController {

    private MoveUI moveUI;
    //private Toggle toggleBloom;
    private Toggle toggleUniColor;
    private Toggle toggleParticleSystems;
    private Toggle toggleBlackBackground;
    private Toggle toggleBenitratorAnimation;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        //toggleBloom = transform.FindChild<Toggle>("MiscOptions/Toggle_Bloom");
        toggleUniColor = transform.FindChild<Toggle>("MiscOptions/Toggle_UniColor");
        toggleParticleSystems = transform.FindChild<Toggle>("MiscOptions/Toggle_ParticleSystems");
        toggleBlackBackground = transform.FindChild<Toggle>("MiscOptions/Toggle_BlackBackground");
        toggleBenitratorAnimation = transform.FindChild<Toggle>("MiscOptions/Toggle_BenitratorAnimation");

        if (PersistentData.instance.firstAppStart) {
            //toggleBloom.isOn = true;
            toggleUniColor.isOn = false;
            toggleParticleSystems.isOn = true;
            toggleBlackBackground.isOn = false;
            toggleBenitratorAnimation.isOn = false;

            PersistentData.instance.uniColor = false;
            PersistentData.instance.enableBloom = true;
            PersistentData.instance.blackBackground = false;
            PersistentData.instance.enableParticleSystems = true;
            PersistentData.instance.benitratorWithoutAnimation = false;
        } else {
            //toggleBloom.isOn = PersistentData.instance.enableBloom;
            toggleUniColor.isOn = PersistentData.instance.uniColor;
            toggleBlackBackground.isOn = PersistentData.instance.blackBackground;
            toggleParticleSystems.isOn = PersistentData.instance.enableParticleSystems;
            toggleBenitratorAnimation.isOn = PersistentData.instance.benitratorWithoutAnimation;
        }
    }

    public void OnValueChanged_BenitratorAnimation(bool value) {
        PersistentData.instance.benitratorWithoutAnimation = value;
    }

    public void OnValueChanged_BlackBackground(bool value) {
        PersistentData.instance.blackBackground = value;
        EventManager.TriggerEvent("ToggleBlackBackground");
    }

    public void OnValueChanged_UniColor(bool value) {
        PersistentData.instance.uniColor = value;
        EventManager.TriggerEvent("ToggleUniColor");
    }

    public void OnValueChanged_Particlesystems(bool value) {
        PersistentData.instance.enableParticleSystems = value;
        EventManager.TriggerEvent("ToggleParticleSystems");
    }

    public void OnValueChanged_Bloom(bool value) {
        //PersistentData.instance.enableBloom = value;
        //EventManager.TriggerEvent("ToggleBloom");
    }

    public override void Show() {
        moveUI.FadeTo(new Vector2(0f, 0f), 0.5f);
    }

    public override void Hide() {
        moveUI.FadeTo(new Vector2(-30f, 0f), 0.5f, true);
    }
}
