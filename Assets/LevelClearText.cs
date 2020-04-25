using System.Collections;
using UnityEngine;
using TMPro;

public class LevelClearText : MonoBehaviour {

    public AudioSource audioSource;
    public TextMeshProUGUI textUI;

    private void Awake() {
        transform.localScale = Vector2.zero;
    }

    public void ShowLevelClearRewardText(int reward) {
        gameObject.SetActive(true);
        StartCoroutine(ShowTextCoroutine(reward));
    }

    private IEnumerator ShowTextCoroutine(int reward) {
        textUI.text = reward.ToString();
        FadeIn();
        yield return new WaitForSecondsRealtime(1f);
        FadeOut();
    }

    private void FadeIn() {
        audioSource.Play();
        LeanTween.scale(gameObject, Vector2.one, 0.3f)
            .setEase(LeanTweenType.easeOutBack);
    }

    private void FadeOut() {
        LeanTween.scale(gameObject, Vector2.zero, 0.3f)
           .setEase(LeanTweenType.easeInBack)
           .setOnComplete(() => gameObject.SetActive(false));
    }
}
