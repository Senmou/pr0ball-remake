using UnityEngine.UI;
using UnityEngine;

public class HeartContainer : MonoBehaviour {

    [HideInInspector] public bool heartIsFull;

    public Transform forcePoint;
    public Rigidbody2D heartLeft;
    public Rigidbody2D heartRight;

    private Color color;
    private Image leftImage;
    private Image rightImage;

    private LTDescr tween;
    private Vector2 startPos;

    private void Awake() {
        heartIsFull = true;
        startPos = transform.position;
        leftImage = heartLeft.GetComponent<Image>();
        rightImage = heartRight.GetComponent<Image>();
        color = leftImage.color;
    }

    public void Explode(bool withAnimation) {

        if (heartIsFull) {
            heartIsFull = false;
            heartLeft.isKinematic = false;
            heartRight.isKinematic = false;
            heartLeft.simulated = true;
            heartRight.simulated = true;
            heartLeft.AddForceAtPosition(Vector2.left, forcePoint.position, ForceMode2D.Impulse);
            heartRight.AddForceAtPosition(Vector2.right, forcePoint.position, ForceMode2D.Impulse);

            if (withAnimation) {
                tween = LeanTween.value(1f, 0f, 1f)
                    .setOnUpdate((alpha) => SetAlpha(alpha))
                    .setDestroyOnComplete(true)
                    .setOnComplete(() => {
                        heartLeft.gameObject.SetActive(false);
                        heartRight.gameObject.SetActive(false);
                    });
            } else {
                heartLeft.gameObject.SetActive(false);
                heartRight.gameObject.SetActive(false);
            }
        }
    }

    public void Restore() {
        if (tween != null)
            LeanTween.cancel(tween.uniqueId);

        heartIsFull = true;
        heartLeft.isKinematic = true;
        heartRight.isKinematic = true;
        heartLeft.simulated = false;
        heartRight.simulated = false;
        heartLeft.gameObject.SetActive(true);
        heartRight.gameObject.SetActive(true);
        heartLeft.transform.position = startPos;
        heartRight.transform.position = startPos;
        heartLeft.transform.rotation = Quaternion.identity;
        heartRight.transform.rotation = Quaternion.identity;
        heartLeft.velocity = Vector2.zero;
        heartRight.velocity = Vector2.zero;
        color.a = 1f;
        leftImage.color = color;
        rightImage.color = color;
    }

    private void SetAlpha(float value) {
        color.a = value;
        leftImage.color = color;
        rightImage.color = color;
    }
}
