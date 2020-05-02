using UnityEngine.UI;
using UnityEngine;

public class PrivacyPolicyDialogue : CanvasController {

    private Button playButton;
    private Toggle toggle;
    private Image okButtonBackground;
    private PauseBackground pauseBackground;

    private void Awake() {
        pauseBackground = FindObjectOfType<PauseBackground>();
        playButton = transform.FindChild<Button>("PlayButton");
        toggle = transform.FindChild<Toggle>("Toggle_Agreement");
        okButtonBackground = transform.FindChild<Image>("PlayButton/Background");

        toggle.isOn = false;
    }

    private void Update() {
        if (!toggle.isOn) {
            playButton.interactable = false;
            okButtonBackground.color = new Color(0.35f, 0.35f, 0.35f, 1f); // grey
        } else {
            playButton.interactable = true;
            okButtonBackground.color = new Color(0.8235295f, 0.2352941f, 0.1333333f, 1f); // red
        }
    }

    public void OnPlayButtonClick() {
        PersistentData.instance.privacyPolicyAgreement = true;
    }

    public override void Show() {
        transform.position = new Vector2(0f, 0f);
        pauseBackground.disableInteractability = true;
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
