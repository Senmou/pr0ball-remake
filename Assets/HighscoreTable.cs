using UnityEngine;
using TMPro;

public class HighscoreTable : MonoBehaviour {

    private Transform template;
    private Transform container;

    private void Start() {
        container = transform.FindChild<Transform>("EntryContainer");
        template = transform.FindChild<Transform>("EntryContainer/EntryTemplate");
        template.gameObject.SetActive(false);

        EventManager.StartListening("HighscoreEntryAdded", UpdateUI);

        UpdateUI();
    }

    private void UpdateUI() {

        int highscoreCount = PersistentData.instance.highscores.entries.Count;

        // Clear all entries
        foreach (Transform item in container) {
            if (item.gameObject.activeSelf)
                Destroy(item.gameObject);
        }

        // Recreate all entries
        for (int i = 0; i < highscoreCount; i++) {
            Transform newEntry = Instantiate(template, container);

            TextMeshProUGUI highscoreUI = newEntry.FindChild<TextMeshProUGUI>("Value");
            TextMeshProUGUI timestampUI = newEntry.FindChild<TextMeshProUGUI>("Playtime");

            timestampUI.text = PersistentData.instance.highscores.entries[i].timestamp;
            highscoreUI.text = PersistentData.instance.highscores.entries[i].highscore.ToString();

            newEntry.gameObject.SetActive(true);
        }
    }
}
