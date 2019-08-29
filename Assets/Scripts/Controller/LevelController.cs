using System.Collections;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour {

    public static LevelController instance;

    public GameObject levelUI;
    public TextMeshProUGUI currentLevelUI;

    public int currentLevel;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        EventManager.StartListening("ReachedNextLevel", OnReachedNextLevel);
    }

    private void Start() {
        UpdateLevelUI();
    }

    private void UpdateLevelUI() {
        currentLevelUI.text = currentLevel.ToString();
    }

    private void OnReachedNextLevel() {
        currentLevel++;
        bool isBossLevel = currentLevel % 10 == 0;
        if (isBossLevel) {
            EventManager.TriggerEvent("ReachedBossLevel");
        }
        UpdateLevelUI();
    }

    public void ShowCurrentLevel() {
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn() {

        Vector2 targetPos = new Vector2(0f, 17.5f);

        float distance = Mathf.Infinity;
        while (distance > 1f) {
            levelUI.transform.position = Vector2.Lerp(levelUI.transform.position, targetPos, Time.deltaTime * 10f);
            distance = Vector2.Distance(levelUI.transform.position, targetPos);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut() {
        Vector2 targetPos = new Vector2(0f, 40f);

        float distance = Mathf.Infinity;
        while (distance > 1f) {
            levelUI.transform.position = Vector2.Lerp(levelUI.transform.position, targetPos, Time.deltaTime * 10f);
            distance = Vector2.Distance(levelUI.transform.position, targetPos);
            yield return null;
        }

        yield return null;
    }
}
