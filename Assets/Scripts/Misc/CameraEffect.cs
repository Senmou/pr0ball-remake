using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class CameraEffect : MonoBehaviour {

    public static CameraEffect instance;

    [SerializeField] private Image overlay;

    private Vector2 startPos;
    //private GameObject bloomVolume;

    private void Awake() {
        instance = this;
        startPos = transform.position;

        //bloomVolume = GameObject.Find("VFX_Bloom");
        //bloomVolume.SetActive(PersistentData.instance.enableBloom);

        EventManager.StartListening("ToggleBloom", OnToggleBloom);
    }

    private void OnToggleBloom() {
        //bloomVolume.SetActive(PersistentData.instance.enableBloom);
    }

    public void Shake(float duration, float magnitude, bool withOverlay = false) {
        if (PersistentData.instance.screenShake)
            StartCoroutine(ShakeCamera(duration, magnitude, withOverlay));
    }

    private IEnumerator ShakeCamera(float duration, float magnitude, bool withOverlay) {

        Color overlayColor = Color.red;

        if (withOverlay)
            overlay.gameObject.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < duration) {

            while (Time.timeScale == 0f) {
                transform.position = startPos;
                yield return null;
            }

            if (withOverlay) {
                overlayColor.a = Random.Range(0.05f, 0.1f);
                overlay.color = overlayColor;
            }

            // Interrupt, if screen is shaking while it's turned off
            if (!PersistentData.instance.screenShake)
                break;

            transform.position = startPos + new Vector2(Random.value, Random.value) * magnitude;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (withOverlay)
            overlay.gameObject.SetActive(false);
        transform.position = startPos;
    }
}
