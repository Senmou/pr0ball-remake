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
    NONE
}

public class CanvasManager : MonoBehaviour {

    public static CanvasManager instance;

    private List<CanvasController> canvasControllerList;
    private CanvasController lastActiveCanvas;

    private CanvasType currentActiveCanvasType;
    public CanvasType CurrentActiveCanvasType { get => currentActiveCanvasType; }

    private void Awake() {
        if (instance == null)
            instance = this;

        canvasControllerList = GetComponentsInChildren<CanvasController>().ToList();
        canvasControllerList.ForEach(x => x.Hide());
    }

    private void Start() {
        SwitchCanvas(CanvasType.NONE);
    }

    public void SwitchCanvas(CanvasType type) {

        currentActiveCanvasType = type;

        if (type == CanvasType.NONE) {
            canvasControllerList.ForEach(x => x.Hide());
            GameController.instance.ResumeGame();
            return;
        }

        if (lastActiveCanvas != null) {
            lastActiveCanvas.Hide();
        }

        CanvasController desiredCanvas = canvasControllerList.Find(x => x.canvasType == type);

        if (desiredCanvas != null) {
            desiredCanvas.Show();
            lastActiveCanvas = desiredCanvas;
            GameController.instance.PauseGame(type.ToString());
        } else Debug.LogWarning("The desired canvas " + "[" + type.ToString() + "] " + "was not found!");
    }
}
