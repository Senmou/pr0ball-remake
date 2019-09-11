﻿using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour {

    public TextMeshProUGUI currentWaveUI;
    private WaveStateController waveStateController;

    private void Awake() {
        waveStateController = FindObjectOfType<WaveStateController>();
    }

    private void Update() {
        UpdateUI();
    }

    private void UpdateUI() {
        currentWaveUI.text = waveStateController.CurrentWave.ToString() + "/" + WaveStateController.wavesPerLevel;
    }
}
