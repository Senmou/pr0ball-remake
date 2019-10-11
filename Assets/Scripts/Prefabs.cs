using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour {

    [System.Serializable]
    public struct ListElement {
        public GameObject prefab;
        public BallColor ballType;
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
            if (item.ballType == ballType)
                return item.prefab;
        }
        return null;
    }
}
