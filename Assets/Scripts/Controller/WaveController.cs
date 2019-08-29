using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wave {

    public List<BaseEnemy> enemies;
    public List<Transform> spawnPoints;

    public Wave() {
        spawnPoints = SpawnPoints.instance.GetRandomSpawnPoints();
    }
}

public class WaveController : MonoBehaviour {

    public static WaveController instance;

    public GameObject waveUI;
    public TextMeshProUGUI currentWaveUI;

    public int currentWave;
    public int wavesPerLevel;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        EventManager.StartListening("WaveCompleted", OnWaveCompleted);
    }

    private void Start() {
        UpdateWaveUI();
    }

    private void OnWaveCompleted() {
        currentWave++;

        if (currentWave > wavesPerLevel) {
            currentWave = 1;
            EventManager.TriggerEvent("ReachedNextLevel");
        }

        UpdateWaveUI();
    }

    private IEnumerator FadeIn() {

        Vector2 targetPos = new Vector2(0f, 8.3f);

        float distance = Mathf.Infinity;
        while (distance > 1f) {
            waveUI.transform.position = Vector2.Lerp(waveUI.transform.position, targetPos, Time.deltaTime * 10f);
            distance = Vector2.Distance(waveUI.transform.position, targetPos);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut() {
        Vector2 targetPos = new Vector2(30f, 8.3f);

        float distance = Mathf.Infinity;
        while (distance > 1f) {
            waveUI.transform.position = Vector2.Lerp(waveUI.transform.position, targetPos, Time.deltaTime * 10f);
            distance = Vector2.Distance(waveUI.transform.position, targetPos);
            yield return null;
        }
        waveUI.transform.position = new Vector2(-30f, 8.3f);
        yield return null;
    }

    private void UpdateWaveUI() {
        currentWaveUI.text = currentWave.ToString() + "/" + wavesPerLevel;
    }
}
