using UnityEngine;
using TMPro;

public class RestartGame : MonoBehaviour {

    private SkillBar skillBar;
    private SkillController skillController;
    private PlayStateController playStateController;
    private TextMeshProUGUI receivableGoldenPointsUI;

    private void Awake() {
        skillBar = FindObjectOfType<SkillBar>();
        skillController = FindObjectOfType<SkillController>();
        playStateController = FindObjectOfType<PlayStateController>();
        receivableGoldenPointsUI = transform.FindChild<TextMeshProUGUI>("ReceivableGoldenPoints/Value");
    }

    private void Update() {
        receivableGoldenPointsUI.text = Score.instance.receivableGoldenPoints.ToString();
    }

    public void ResetGameData() {
        Score.instance.BookReceivableGoldenPoints();

        playStateController.gameRestarted = true;

        skillBar.ResetData();
        Score.instance.ResetData();
        skillController.ResetData();
        BallStats.Instance.ResetStats();
    }
}
