using UnityEngine;

public class BaseItem : MonoBehaviour {

    public virtual void OnItemCollect() {
        Debug.Log("Item collected!");
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        OnItemCollect();
    }
}
