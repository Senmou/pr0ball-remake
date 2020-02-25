using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class CanvasSwitcher : MonoBehaviour {

    public CanvasType desiredCanvasType;

    private CanvasManager canvasManager;
    private Button menuButton;

    private void Start() {
        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(OnButtonClicked);
        canvasManager = CanvasManager.instance;
    }

    private void OnButtonClicked() {
        if (menuButton.IsInteractable())
            canvasManager.SwitchCanvas(desiredCanvasType);
    }
}
