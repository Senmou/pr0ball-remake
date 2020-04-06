using UnityEngine.UI;
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
        timestampUI = transform.FindChild<TextMeshProUGUI>("")
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

        int limit = globalEntries.Length;
        for (int i = 0; i < limit; i++) {
            Transform newEntry = Instantiate(template, container);

            TextMeshProUGUI highscoreUI = newEntry.FindChild<TextMeshProUGUI>("Value");
            TextMeshProUGUI timestampUI = newEntry.FindChild<TextMeshProUGUI>("Playtime");

            timestampUI.text = PersistentData.instance.highscores.entries[i].timestamp;
            highscoreUI.text = PersistentData.instance.highscores.entries[i].highscore.ToString();

            int entryID = PersistentData.instance.highscores.entries[i].id;
            //newEntry.GetComponent<Button>().onClick.AddListener(() => OnClick(entryID));

            newEntry.gameObject.SetActive(true);

            DotColorController dotColorController = newEntry.FindChild<DotColorController>("Dot").GetComponent<DotColorController>();
            dotColorController.UpdateDotColor(PersistentData.instance.highscores.entries[i].highscore);
        }
    }
}
