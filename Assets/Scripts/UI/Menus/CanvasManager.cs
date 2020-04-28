using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CanvasType {
    GAMEOVER,
    PAUSE,
    OPTIONS,
    BALLS,
    SKILLS,
    HIGHSCORES,
    NONE,
    STATISTICS,
    LAST,
    NAME,
    GLOBAL_HIGHSCORES,
    SECURITY_QUESTION_NEW_GAME,
    DATA_SECURITY_POLICY
}

public class CanvasManager : MonoBehaviour {

    public static CanvasManager instance;

    private CanvasController lastActiveCanvas;
    private List<CanvasController> canvasControllerList;

    private CanvasType currentActiveCanvasType;
    public CanvasType CurrentActiveCanvasType { get => currentActiveCanvasType; }

    public Stack<CanvasType> canvasHistory;

    private void Awake() {
        if (instance == null)
            instance = this;

        canvasHistory = new Stack<CanvasType>();
        canvasControllerList = GetComponentsInChildren<CanvasController>().ToList();
        canvasControllerList.ForEach(x => x.Hide());
    }

    public void GoOneCanvasBack() {
        if (canvasHistory.Count == 0) {
            return;
        } else {
            canvasHistory.Pop();
            SwitchCanvas(canvasHistory.Pop());
        }
    }

    public void SwitchCanvas(CanvasType type, bool hideLastMenu = true, bool addToHistory = true) {

        if (addToHistory)
            canvasHistory.Push(type);
        currentActiveCanvasType = type;

        if (type == CanvasType.NONE) {
            canvasControllerList.ForEach(x => {
                if (x.gameObject.activeSelf)
                    x.Hide();
            });
            GameController.instance.ResumeGame();
            return;
        }

        if (hideLastMenu && lastActiveCanvas != null) {
            if (lastActiveCanvas.gameObject.activeSelf)
                lastActiveCanvas.Hide();
        }

        CanvasController desiredCanvas = canvasControllerList.Find(x => x.canvasType == type);

        if (desiredCanvas != null) {
            desiredCanvas.gameObject.SetActive(true);
            desiredCanvas.Show();
            lastActiveCanvas = desiredCanvas;
            GameController.instance.PauseGame();
        } else Debug.LogWarning("The desired canvas " + "[" + type.ToString() + "] " + "was not found!");
    }
}
