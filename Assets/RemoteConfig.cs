using Unity.RemoteConfig;
using UnityEngine;

public class RemoteConfig : MonoBehaviour {

    public struct userAttributes { }
    public struct appAttributes { }

    public float enemyHealthMultiplier = 1f;

    private void Awake() {
        ConfigManager.FetchCompleted += ApplyEnemyHealthMultiplier;
        ConfigManager.FetchConfigs(new userAttributes(), new appAttributes());
    }

    public void ApplyEnemyHealthMultiplier(ConfigResponse response) {
        if (response.status == ConfigRequestStatus.Success) {
            enemyHealthMultiplier = ConfigManager.appConfig.GetFloat("enemyHealthMultiplier");
        } else
            enemyHealthMultiplier = 1f;
    }
}
