using Unity.RemoteConfig;
using UnityEngine;

public class RemoteConfig : MonoBehaviour {

    public static RemoteConfig remoteConfig;

    public struct userAttributes { }
    public struct appAttributes { }

    public struct HealthMultiplier {
        public float enemy_0;
        public float enemy_1;
        public float enemy_2;
        public float enemy_3;
        public float enemy_4;
        public float enemy_5;
    }

    public HealthMultiplier healthMultiplier;

    private void Awake() {
        remoteConfig = this;
        ConfigManager.FetchCompleted += ApplyRemoteSettings;
        ConfigManager.FetchConfigs(new userAttributes(), new appAttributes());
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F))
            ConfigManager.FetchConfigs(new userAttributes(), new appAttributes());
    }

    private void ApplyRemoteSettings(ConfigResponse configResponse) {

        switch (configResponse.requestOrigin) {
            case ConfigOrigin.Default:
                Debug.Log("No settings loaded this session; using default values.");
                break;
            case ConfigOrigin.Cached:
                Debug.Log("No settings loaded this session; using cached values from a previous session.");
                break;
            case ConfigOrigin.Remote:
                Debug.Log("New settings loaded this session; update values accordingly.");
                ApplyRemoteHealthMultiplier();
                break;
        }
    }

    private void ApplyRemoteHealthMultiplier() {
        healthMultiplier.enemy_0 = ConfigManager.appConfig.GetFloat("enemy0_hpMult", 1f);
        healthMultiplier.enemy_1 = ConfigManager.appConfig.GetFloat("enemy1_hpMult", 1.1f);
        healthMultiplier.enemy_2 = ConfigManager.appConfig.GetFloat("enemy2_hpMult", 8f);
        healthMultiplier.enemy_3 = ConfigManager.appConfig.GetFloat("enemy3_hpMult", 1.5f);
        healthMultiplier.enemy_4 = ConfigManager.appConfig.GetFloat("enemy4_hpMult", 5f);
        healthMultiplier.enemy_5 = ConfigManager.appConfig.GetFloat("enemy5_hpMult", 6.5f);
    }
}
