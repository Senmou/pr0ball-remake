using UnityEngine;
using TMPro;

public class HighscoreTable : MonoBehaviour {

    private Transform template;
    private Transform container;

    private void Start() {
        container = transform.FindChild<Transform>("EntryContainer");
        template = transform.FindChild<Transform>("EntryContainer/EntryTemplate");
        template.gameObject.SetActive(false);
    }

    public void UpdateUI() {

        int highscoreCount = PersistentData.instance.highscores.entries.Count;

        // Clear all entries except the template
        for (int i = 0; i < container.childCount; i++) {
            Transform child = container.GetChild(0);
            if (child != null && child != template)
                Destroy(child.gameObject);
        }

        // Recreate all entries
        for (int i = 0; i < highscoreCount; i++) {
            Transform newEntry = Instantiate(template, container);

            TextMeshProUGUI highscoreUI = newEntry.FindChild<TextMeshProUGUI>("Value");
            TextMeshProUGUI timestampUI = newEntry.FindChild<TextMeshProUGUI>("Playtime");

            timestampUI.text = PersistentData.instance.highscores.entries[i].timestamp;
            highscoreUI.text = PersistentData.instance.highscores.entries[i].highscore.ToString();

            newEntry.gameObject.SetActive(true);

            DotColorController dotColorController = newEntry.FindChild<DotColorController>("Dot").GetComponent<DotColorController>();
            dotColorController.UpdateDotColor(PersistentData.instance.highscores.entries[i].highscore);
        }
    }
}
