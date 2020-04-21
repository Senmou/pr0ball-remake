using UnityEngine.UI;
using UnityEngine;

public class ItemTween : MonoBehaviour {
    
    public bool rotate;
    public bool useGradient;
    public Gradient gradient;

    private float direction;
    private Image image;
    private Vector3 targetSize;

    private void Awake() {
        image = GetComponent<Image>();
        direction = (Random.value < 0.5f) ? -1f : 1f;
        targetSize = transform.localScale + new Vector3(0.2f, 0.2f);
    }

    private void Start() {
        if (rotate)
            LeanTween.rotateAround(gameObject, Vector3.forward, 360f * direction, 10f).setLoopClamp();

        if (useGradient)
            LeanTween.value(0f, 1f, 10f).setOnUpdate(x => SetColor(x)).setLoopClamp();
    }

    private void SetColor(float value) {
        if (image)
            image.color = gradient.Evaluate(value);
    }
}
