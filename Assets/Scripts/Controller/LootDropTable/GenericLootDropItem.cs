using UnityEngine;

public class GenericLootDropItem {

    public GameObject item;
    public string poolName;
    public float probabilityWeight;
    public float probabilityPercent;

    [HideInInspector]
    public float probabilityRangeFrom;
    [HideInInspector]
    public float probabilityRangeTo;
}
