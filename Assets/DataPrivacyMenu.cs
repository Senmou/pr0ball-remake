using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class DataPrivacyMenu : CanvasController {

    private MoveUI moveUI;
    private TextMeshProUGUI policyUI;
    private PauseBackground pauseBackground;

    private void Awake() {
        moveUI = GetComponent<MoveUI>();
        pauseBackground = FindObjectOfType<PauseBackground>();
        policyUI = transform.FindChild<TextMeshProUGUI>("ScrollRect/Text");
    }

    public override void Show() {
        policyUI.text = "Lädt...";
        StartCoroutine(FetchPrivacyPolicy());
        pauseBackground.disableInteractability = true;
        moveUI.FadeTo(Vector2.zero, 0.5f);
    }

    public override void Hide() {
        pauseBackground.disableInteractability = false;
        Vector2 edgeVector = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));
        float screenWidth = edgeVector.x * 2f;

        moveUI.FadeTo(new Vector2(-screenWidth, 0f), 0.5f, true);
    }

    private IEnumerator FetchPrivacyPolicy() {

        string url = Constants.dataPrivacyPolicy;

        using (UnityWebRequest www = UnityWebRequest.Get(url)) {

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                policyUI.text = PersistentData.instance.latestDataPrivacyPolicy;
            } else {
                string html = www.downloadHandler.text;

                HTMLParser parser = new HTMLParser();
                string output = parser.Parse(html);
                policyUI.text = output;
                PersistentData.instance.latestDataPrivacyPolicy = output;
            }
        }
    }
}
