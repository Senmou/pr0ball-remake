using UnityEngine;
using TMPro;

public class DisplayPlayerName : MonoBehaviour {

    private TextMeshProUGUI nameUI;

    private void Awake() {
        nameUI = transform.FindChild<TextMeshProUGUI>("Text");
        UpdateNameUI();
    }

    public void UpdateNameUI() {
        string playerName = PersistentData.instance.playerName;

        if (string.IsNullOrEmpty(playerName))
            playerName = "[anonymous]";
        nameUI.text = playerName;
    }

    public void OnChangeNameButtonClicked() {
        CanvasManager.instance.SwitchCanvas(CanvasType.NAME, hideLastMenu: false, addToHistory: true);
    }
}
