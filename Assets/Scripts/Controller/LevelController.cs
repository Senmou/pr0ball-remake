using UnityEngine;

public class LevelController : MonoBehaviour {

    #region Singleton
    public static LevelController instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    #endregion

   
}
