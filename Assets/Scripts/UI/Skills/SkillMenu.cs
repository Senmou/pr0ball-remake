using UnityEngine;

public class SkillMenu : MonoBehaviour {

    public Slot slot;

    public void Toggle() {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
