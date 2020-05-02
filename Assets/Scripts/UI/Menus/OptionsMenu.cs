using UnityEngine.UI;
using UnityEngine;

public class OptionsMenu : CanvasController {

    private MoveUI moveUI;
    //private Toggle toggleBloom;
    private Toggle toggleVSync;
    private Toggle toggleFPS_30;
    private Toggle toggleFPS_60;
    private Toggle toggleUniColor;
    private Toggle toggleScreenShake;
    private Toggle toggleParticleSystems;
    private Toggle toggleBlackBackground;
    private Toggle toggleBenitratorAnimation;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        //toggleBloom = transform.FindChild<Toggle>("MiscOptions/Toggle_Bloom");
        toggleUniColor = transform.FindChild<Toggle>("MiscOptions/Toggle_UniColor");
        toggleVSync = transform.FindChild<Toggle>("MiscOptions/Toggle_FPS/Toggle_VSync");
        toggleScreenShake = transform.FindChild<Toggle>("MiscOptions/Toggle_ScreenShake");
        toggleFPS_30 = transform.FindChild<Toggle>("MiscOptions/Toggle_FPS/Toggle_FPS_30");
        toggleFPS_60 = transform.FindChild<Toggle>("MiscOptions/Toggle_FPS/Toggle_FPS_60");
        toggleParticleSystems = transform.FindChild<Toggle>("MiscOptions/Toggle_ParticleSystems");
        toggleBlackBackground = transform.FindChild<Toggle>("MiscOptions/Toggle_BlackBackground");
        toggleBenitratorAnimation = transform.FindChild<Toggle>("MiscOptions/Toggle_BenitratorAnimation");

        if (PersistentData.instance.firstAppStart) {
            //toggleBloom.isOn = true;
            toggleVSync.isOn = false;
            toggleFPS_60.isOn = true;
            toggleUniColor.isOn = false;
            toggleScreenShake.isOn = true;
            toggleParticleSystems.isOn = true;
            toggleBlackBackground.isOn = false;
            toggleBenitratorAnimation.isOn = false;

            QualitySettings.vSyncCount = 0;
            PersistentData.instance.vSync = false;
            PersistentData.instance.targetFPS = 60;
            PersistentData.instance.uniColor = false;
            PersistentData.instance.enableBloom = true;
            PersistentData.instance.screenShake = true;
            PersistentData.instance.blackBackground = false;
            PersistentData.instance.enableParticleSystems = true;
            PersistentData.instance.benitratorWithoutAnimation = false;
        } else {
            //toggleBloom.isOn = PersistentData.instance.enableBloom;
            if (PersistentData.instance.vSync) {
                QualitySettings.vSyncCount = 1;
                toggleVSync.isOn = true;
            } else if (PersistentData.instance.targetFPS == 30) {
                Application.targetFrameRate = 30;
                toggleFPS_30.isOn = true;
            } else {
                Application.targetFrameRate = 60;
                toggleFPS_60.isOn = true;
            }
            toggleUniColor.isOn = PersistentData.instance.uniColor;
            toggleScreenShake.isOn = PersistentData.instance.screenShake;
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

    public void OnValueChanged_ToggleFPS_30(bool value) {
        Application.targetFrameRate = 30;
        PersistentData.instance.targetFPS = 30;
    }

    public void OnValueChanged_ToggleFPS_60(bool value) {
        Application.targetFrameRate = 60;
        PersistentData.instance.targetFPS = 60;
    }

    public void OnValueChanged_ToggleFPS_VSync(bool value) {
        PersistentData.instance.vSync = value;
        QualitySettings.vSyncCount = value ? 1 : 0;
    }

    public void OnValueChanged_ScreenShake(bool value) {
        PersistentData.instance.screenShake = value;
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
