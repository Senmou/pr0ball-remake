using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour {

    [System.Serializable]
    public struct ListElement {
        public GameObject prefab;
        public BallColor ballColor;
    }

    #region Singleton
    public static Prefabs instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    #endregion

    public List<ListElement> prefabs;

    public GameObject GetGO(BallColor ballType) {
        foreach (var item in prefabs) {
            if (item.ballColor == ballType)
                return item.prefab;
        }
        return null;
    }
}
