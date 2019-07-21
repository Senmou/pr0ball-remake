using System.Collections;
using UnityEngine;

public class MoveUI : MonoBehaviour {
    
    public void FadeTo(Vector2 targetPos, float timeToFade) {
        StopAllCoroutines();
        StartCoroutine(FadePosition(targetPos, timeToFade));
    }

    private IEnumerator FadePosition(Vector2 targetPos, float timeToFade) {
        
        float t = 0f;
        while (t < 1f) {
            transform.position = Vector2.Lerp(transform.position, targetPos, t);
            t += Time.unscaledDeltaTime / timeToFade;
            yield return null;
        }
        yield return null;
    }
}
