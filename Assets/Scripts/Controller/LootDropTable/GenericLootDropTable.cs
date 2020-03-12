using System.Collections.Generic;
using UnityEngine;

public class GenericLootDropTable<T> where T : GenericLootDropItem {

    [SerializeField]
    public List<T> lootDropItems;

    float probabilityTotalWeight;

    private T GetItem(string poolName) {
        foreach (var item in lootDropItems) {
            if (item.poolName == poolName)
                return item;
        }
        return null;
    }

    public void AddWeight(string poolName, float weight, bool validateTable = false) {
        T item = GetItem(poolName);
        item.probabilityWeight += weight;

        if (validateTable)
            ValidateTable();
    }

    public void SetWeight(string poolName, float weight) {
        T item = GetItem(poolName);
        item.probabilityWeight = weight;
        ValidateTable();
    }

    public void ValidateTable() {
        if (lootDropItems != null && lootDropItems.Count > 0) {
            float currentProbabilityWeightMaximum = 0f;

            foreach (T lootDropItem in lootDropItems) {
                if (lootDropItem.probabilityWeight < 0f) {
                    Debug.LogWarning("You can't have negative weight on an item. Resetting item's weight to 0.");
                    lootDropItem.probabilityWeight = 0f;
                } else {
                    lootDropItem.probabilityRangeFrom = currentProbabilityWeightMaximum;
                    currentProbabilityWeightMaximum += lootDropItem.probabilityWeight;
                    lootDropItem.probabilityRangeTo = currentProbabilityWeightMaximum;
                }
            }

            probabilityTotalWeight = currentProbabilityWeightMaximum;

            foreach (T lootDropItem in lootDropItems) {
                lootDropItem.probabilityPercent = (lootDropItem.probabilityWeight / probabilityTotalWeight) * 100;
            }
        }
    }

    public T PickLootDropItem() {
        float pickedNumber = Random.Range(0f, probabilityTotalWeight);

        foreach (T lootDropItem in lootDropItems) {
            if (pickedNumber > lootDropItem.probabilityRangeFrom && pickedNumber < lootDropItem.probabilityRangeTo)
                return lootDropItem;
        }

        Debug.LogError("Item couldn't be picked. Make sure that all active loot drop tables contain at least one item");
        return lootDropItems[0];
    }
}
