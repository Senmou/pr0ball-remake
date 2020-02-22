using TMPro;
using UnityEngine;

public class HighscoreTable : MonoBehaviour {

    private Transform container;
    private Transform template;

    private void Awake() {
        

        
    }

    private void Start() {
        container = transform.FindChild<Transform>("EntryContainer");
        template = transform.FindChild<Transform>("EntryContainer/EntryTemplate");
        template.gameObject.SetActive(false);

        float templateHeight = 210f;

        int highscoreCount = PersistentData.instance.highscores.entries.Count;

        for (int i = 0; i < highscoreCount; i++) {
            Transform entryTemplate = Instantiate(template, container);
            RectTransform rect = entryTemplate.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(0f, -templateHeight * i);

            TextMeshProUGUI highscoreUI = entryTemplate.FindChild<TextMeshProUGUI>("Value");
            TextMeshProUGUI timestampUI = entryTemplate.FindChild<TextMeshProUGUI>("Playtime");

            timestampUI.text = PersistentData.instance.highscores.entries[i].timestamp;
            highscoreUI.text = PersistentData.instance.highscores.entries[i].highscore.ToString();

            entryTemplate.gameObject.SetActive(true);
        }
    }
}
