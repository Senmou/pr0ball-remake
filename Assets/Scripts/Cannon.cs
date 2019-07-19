using UnityEngine;

public class Cannon : MonoBehaviour {

    private Vector3 direction;
    private Quaternion targetRotation;
    private float currentAngle;
    private float rotationSpeed = 10f;
    private float limit = 70f;

    private void Update() {
        RotateCannon();
    }

    private void RotateCannon() {

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        currentAngle = Vector2.Angle(Vector2.down, direction);

        if (currentAngle < limit)
            transform.up = Vector2.Lerp(transform.up, direction, Time.unscaledDeltaTime * rotationSpeed);
        else {
            if (transform.up.x < 0f)
                transform.up = new Vector3(-0.95f, transform.up.y, transform.up.z);
            else
                transform.up = new Vector3(0.95f, transform.up.y, transform.up.z);
        }
    }
}