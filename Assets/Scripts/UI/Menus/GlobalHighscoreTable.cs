using UnityEngine;
using TMPro;

public class GlobalHighscoreTable : MonoBehaviour {

    private Transform template;
    private Transform container;
    private TextMeshProUGUI timestampUI;
    private HighscoreController highscoreController;

    public struct GlobalHighscoreEntry {
        public int score;
        public string timestamp;
        public string playerName;
    }

    private void Start() {
        highscoreController = FindObjectOfType<HighscoreController>();
        container = transform.FindChild<Transform>("EntryContainer");
        template = transform.FindChild<Transform>("EntryContainer/EntryTemplate");
        template.gameObject.SetActive(false);
    }

    public void UpdateUI() {

        // Clear all entries except the template
        int startIndex = container.childCount - 1;
        for (int i = startIndex; i > 0; i--) {
            Transform child = container.GetChild(i);
            if (child != null && child != template) {
                Destroy(child.gameObject);
            }
        }

        highscoreController.ShowGlobalHighscores(OnGlobalEntriesLoaded);
    }

    private void OnGlobalEntriesLoaded(GlobalHighscoreEntry[] globalEntries) {

        float anchoredPosFirstElement = 0f;

        int limit = globalEntries.Length;
        for (int i = 0; i < limit; i++) {
            Transform newEntry = Instantiate(template, container);

            TextMeshProUGUI highscoreUI = newEntry.FindChild<TextMeshProUGUI>("Value");
            TextMeshProUGUI nameUI = newEntry.FindChild<TextMeshProUGUI>("Signature/PlayerName");
            TextMeshProUGUI timestampUI = newEntry.transform.FindChild<TextMeshProUGUI>("Signature/PlayerName/TimestampContainer/Timestamp");

            nameUI.text = globalEntries[i].playerName;
            timestampUI.text = globalEntries[i].timestamp;
            highscoreUI.text = globalEntries[i].score.ToString();

            if (i == 0) anchoredPosFirstElement = newEntry.GetComponent<RectTransform>().anchoredPosition.y;

            newEntry.gameObject.SetActive(true);

            DotColorController dotColorController = newEntry.FindChild<DotColorController>("Signature/PlayerName/TimestampContainer/Dot").GetComponent<DotColorController>();
            dotColorController.UpdateDotColor(globalEntries[i].score);
        }
    }
}
