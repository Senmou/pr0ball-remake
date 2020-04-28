using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class EnterNameMenu : CanvasController {

    private Button okButton;
    private Image okButtonBackground;
    private TMP_InputField inputField;
    private PauseBackground pauseBackground;
    private DisplayPlayerName displayPlayerName;

    private void Awake() {
        transform.position = new Vector2(0f, 0f);
        okButton = transform.FindChild<Button>("OkButton");
        pauseBackground = FindObjectOfType<PauseBackground>();
        displayPlayerName = FindObjectOfType<DisplayPlayerName>();
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
        if (displayPlayerName)
            displayPlayerName.UpdateNameUI();
    }

    public override void Show() {
        pauseBackground.disableInteractability = true;
        inputField.text = PersistentData.instance.playerName;
        LeanTween.scale(gameObject, Vector3.one, 0.1f)
           .setOnStart(() => gameObject.SetActive(true))
           .setIgnoreTimeScale(true)
           .setEase(showEaseType);
    }

    public override void Hide() {
        pauseBackground.disableInteractability = false;
        LeanTween.scale(gameObject, Vector3.zero, 0.15f)
             .setIgnoreTimeScale(true)
             .setEase(hideEaseType)
             .setOnComplete(() => gameObject.SetActive(false));
    }
}
