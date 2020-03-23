﻿using System.Collections;
using MarchingBytes;
using UnityEngine;

public class GameController : MonoBehaviour {

    public const string saveFileName = "saveData_v1.dat";

    [SerializeField] private FloatingText floatingText;

    public static bool isGamePaused = false;
    public static GameController instance = null;

    private Canvas canvas;
    private BallMenu ballMenu;
    private PauseMenu mainMenu;
    private SkillMenu skillMenu;
    private RestartGame restartGame;
    private EnemyController enemyController;

    private float elapsedTimeSinceRestart;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        ballMenu = FindObjectOfType<BallMenu>();
        mainMenu = FindObjectOfType<PauseMenu>();
        skillMenu = FindObjectOfType<SkillMenu>();
        restartGame = FindObjectOfType<RestartGame>();
        enemyController = FindObjectOfType<EnemyController>();
        canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();

        LevelData.SetCurrentLevelData(PersistentData.instance.currentLevelData);

        elapsedTimeSinceRestart = PersistentData.instance.elapsedTimeSinceRestart;

        EventManager.StartListening("SaveGame", OnSaveGame);
        EventManager.StartListening("ChacheData", OnChacheData);
        EventManager.StartListening("GameRestarted", OnGameRestarted);
    }

    public void SpawnFloatingText(string text, Vector2 position) {
        FloatingText go = EasyObjectPool.instance.GetObjectFromPool("FloatingText_pool", position, Quaternion.identity).GetComponent<FloatingText>();
        go.SetText(text);
        go.transform.SetParent(canvas.transform);
        go.ReturnToPoolAfter(0.9f);
    }

    private void Start() {

        if (PersistentData.instance.isGameOver) {
            restartGame.StartNewGame();
        } else {
            enemyController.LoadEntities();
            Statistics.Instance.OnLoadGame();
        }
    }

    private void Update() {

        elapsedTimeSinceRestart += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (CanvasManager.instance.CurrentActiveCanvasType == CanvasType.NONE)
                CanvasManager.instance.SwitchCanvas(CanvasType.PAUSE);
            else if (CanvasManager.instance.CurrentActiveCanvasType != CanvasType.GAMEOVER)
                CanvasManager.instance.GoOneCanvasBack();
        }
    }

    public int GetPlaytimeMinutes() => (int)elapsedTimeSinceRestart / 60;

    private void OnChacheData() {
        PersistentData.instance.currentLevelData.wave = LevelData.Wave;
        PersistentData.instance.currentLevelData.level = LevelData.Level;
        PersistentData.instance.currentLevelData.dangerLevel = LevelData.DangerLevel;
        PersistentData.instance.elapsedTimeSinceRestart = elapsedTimeSinceRestart;
        Statistics.Instance.OnChacheData();
    }

    private void OnSaveGame() {
        PersistentData.instance.firstAppStart = false;
    }

    private void OnGameRestarted() {
        elapsedTimeSinceRestart = 0f;
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        isGamePaused = true;
        EventManager.TriggerEvent("GamePaused");
    }

    public void ResumeGame() {
        StartCoroutine(ResumeGameAtEndOfFrame());
    }

    private IEnumerator ResumeGameAtEndOfFrame() {
        yield return new WaitForEndOfFrame();
        Time.timeScale = 1f;
        isGamePaused = false;
        EventManager.TriggerEvent("GameResumed");
    }
}
