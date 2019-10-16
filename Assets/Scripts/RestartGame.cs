using UnityEngine;
using TMPro;

public class RestartGame : MonoBehaviour {

    public TextMeshProUGUI receivableGoldenPointsUI;
    private SkillBar skillBar;
    private SkillController skillController;

    private void Awake() {
        skillBar = FindObjectOfType<SkillBar>();
        skillController = FindObjectOfType<SkillController>();
        receivableGoldenPointsUI = transform.FindChild<TextMeshProUGUI>("ReceivableGoldenPoints/Value");
    }

    private void Update() {
        receivableGoldenPointsUI.text = Score.instance.receivableGoldenPoints.ToString();
    }

    public void ResetGameData() {
        Score.instance.BookReceivableGoldenPoints();

        skillBar.ResetData();
        LevelData.ResetData();
        Score.instance.ResetData();
        skillController.ResetData();
    }
}
