using UnityEngine;

public class Toggle_UniColor : MonoBehaviour {

    private void Start() {
        PersistentData.instance.uniColor = true;
    }

    public void OnValueChanged(bool value) {
        PersistentData.instance.uniColor = value;
        EventManager.TriggerEvent("ToggleUniColor");
    }
}
