using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class EnterNameMenu : CanvasController {

    private MoveUI moveUI;
    private Button okButton;
    private Image okButtonBackground;
    private TMP_InputField inputField;
    private PauseBackground pauseBackground;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        okButton = transform.FindChild<Button>("OkButton");
        pauseBackground = FindObjectOfType<PauseBackground>();
        inputField = transform.FindChild<TMP_InputField>("NameInputField");
        okButtonBackground = transform.FindChild<Image>("OkButton/Background");
    }

    private void Update() {
        if (string.IsNullOrEmpty(inputField.text) || string.IsNullOrWhiteSpace(inputField.text)) {
            okButton.interactable = false;
            okButtonBackground.color = new Color(0.35f, 0.35f, 0.35f, 1f); // grey
        } else {
            okButton.interactable = true;
            okButtonBackground.color = new Color(0.8235295f, 0.2352941f, 0.1333333f, 1f); // red
        }
    }

    public void OnOkButtonClick() {
        PersistentData.instance.playerName = inputField.text;
        FindObjectOfType<DisplayPlayerName>().UpdateNameUI();
    }

    public override void Show() {
        inputField.text = PersistentData.instance.playerName;
        pauseBackground.disableInteractability = true;
        moveUI.FadeTo(Vector2.zero, 0.5f);
    }

    public override void Hide() {
        pauseBackground.disableInteractability = false;
        moveUI.FadeTo(new Vector2(0f, -35f), 0.5f, true);
    }
}
