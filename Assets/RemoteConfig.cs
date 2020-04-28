using Unity.RemoteConfig;
using UnityEngine;

public class RemoteConfig : MonoBehaviour {

    public static RemoteConfig instance;

    public struct userAttributes { }
    public struct appAttributes { }

    public struct RemoteHealthMultiplier {
        public float enemy_0;
        public float enemy_1;
        public float enemy_2;
        public float enemy_3;
        public float enemy_4;
        public float enemy_5;
    }

    public float remoteItemSpawnChance;
    public RemoteHealthMultiplier remoteHealthMultiplier;

    private void Awake() {
        instance = this;
        ConfigManager.FetchCompleted += ApplyRemoteSettings;
        ConfigManager.FetchConfigs(new userAttributes(), new appAttributes());

        remoteItemSpawnChance = 5f;

        remoteHealthMultiplier.enemy_0 = 1f;
        remoteHealthMultiplier.enemy_1 = 1.1f;
        remoteHealthMultiplier.enemy_2 = 8f;
        remoteHealthMultiplier.enemy_3 = 1.5f;
        remoteHealthMultiplier.enemy_4 = 5f;
        remoteHealthMultiplier.enemy_5 = 6.5f;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F))
            ConfigManager.FetchConfigs(new userAttributes(), new appAttributes());
    }

    public void FetchConfig() {
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
                ApplyRemoteValues();
                break;
        }
    }

    private void ApplyRemoteValues() {

        // Remote values are integers, because dumbfuck Unity can't push/pull float values without errors
        // Every value needs to be divided by 10 to get the correct hp multiplier

        remoteItemSpawnChance = ConfigManager.appConfig.GetInt("itemSpawnChance", 50) / 10f;

        remoteHealthMultiplier.enemy_0 = ConfigManager.appConfig.GetInt("enemy0hpMult", 10) / 10f;
        remoteHealthMultiplier.enemy_1 = ConfigManager.appConfig.GetInt("enemy1hpMult", 11) / 10f;
        remoteHealthMultiplier.enemy_2 = ConfigManager.appConfig.GetInt("enemy2hpMult", 80) / 10f;
        remoteHealthMultiplier.enemy_3 = ConfigManager.appConfig.GetInt("enemy3hpMult", 15) / 10f;
        remoteHealthMultiplier.enemy_4 = ConfigManager.appConfig.GetInt("enemy4hpMult", 50) / 10f;
        remoteHealthMultiplier.enemy_5 = ConfigManager.appConfig.GetInt("enemy5hpMult", 65) / 10f;
    }
}
