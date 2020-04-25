using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using TMPro;

public class HighscoreTable : MonoBehaviour {

    private Transform template;
    private Transform container;
    private StatisticsMenu statisticsMenu;

    private void Start() {
        statisticsMenu = FindObjectOfType<StatisticsMenu>();
        container = transform.FindChild<Transform>("EntryContainer");
        template = transform.FindChild<Transform>("EntryContainer/EntryTemplate");
        template.gameObject.SetActive(false);
    }

    public void UpdateUI() {

        float anchoredPosFirstElement = 0f;
        int highscoreCount = PersistentData.instance.highscores.entries.Count;

        // Clear all entries except the template
        int startIndex = container.childCount - 1;
        for (int i = startIndex; i > 0; i--) {
            Transform child = container.GetChild(i);
            if (child != null && child != template) {
                Destroy(child.gameObject);
            }
        }

        // Recreate all entries
        for (int i = 0; i < highscoreCount; i++) {
            Transform newEntry = Instantiate(template, container);

            TextMeshProUGUI highscoreUI = newEntry.FindChild<TextMeshProUGUI>("Value");
            TextMeshProUGUI timestampUI = newEntry.FindChild<TextMeshProUGUI>("Playtime");

            timestampUI.text = PersistentData.instance.highscores.entries[i].timestamp;
            highscoreUI.text = PersistentData.instance.highscores.entries[i].highscore.ToString();

            int entryID = PersistentData.instance.highscores.entries[i].id;
            newEntry.GetComponent<Button>().onClick.AddListener(() => OnClick(entryID));

            if (i == 0) anchoredPosFirstElement = newEntry.GetComponent<RectTransform>().anchoredPosition.y;

            newEntry.gameObject.SetActive(true);

            DotColorController dotColorController = newEntry.FindChild<DotColorController>("Dot").GetComponent<DotColorController>();
            dotColorController.UpdateDotColor(PersistentData.instance.highscores.entries[i].highscore);
        }
    }

    private void OnClick(int id) {

        Statistics statisticsCopy = PersistentData.instance.highscores.entries.First(entry => entry.id == id).statistics;
        statisticsMenu.SetStatistics(statisticsCopy);
        CanvasManager.instance.SwitchCanvas(CanvasType.STATISTICS);
    }
}
