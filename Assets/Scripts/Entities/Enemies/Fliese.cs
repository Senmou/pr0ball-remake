using UnityEngine;

public class Fliese : BaseEnemy {

    private void Update() {
        transform.position += new Vector3(0f, 1f) * Time.deltaTime;
    }
}
