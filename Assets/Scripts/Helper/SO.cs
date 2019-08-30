using UnityEngine;

public class SO : MonoBehaviour {

    public static SO instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public BallConfig ballConfig;
}
