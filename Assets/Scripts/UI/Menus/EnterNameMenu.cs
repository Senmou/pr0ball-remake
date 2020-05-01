using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class EnterNameMenu : CanvasController {

    private Button okButton;
    private TMP_InputField inputField;
    private PauseBackground pauseBackground;
    private DisplayPlayerName displayPlayerName;

    private void Awake() {
        transform.position = new Vector2(0f, 0f);
        okButton = transform.FindChild<Button>("OkButton");
        pauseBackground = FindObjectOfType<PauseBackground>();
        displayPlayerName = FindObjectOfType<DisplayPlayerName>();
        inputField = transform.FindChild<TMP_InputField>("NameInputField");
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
