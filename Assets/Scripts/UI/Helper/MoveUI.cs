using System.Collections;
using UnityEngine;

public class MoveUI : MonoBehaviour {
    
    public void FadeTo(Vector2 targetPos, float timeToFade, bool deactivateObjectWhenReachingTargetPos = false) {
        StopAllCoroutines();
        StartCoroutine(FadePosition(targetPos, timeToFade, deactivateObjectWhenReachingTargetPos));
    }

    private IEnumerator FadePosition(Vector2 targetPos, float timeToFade, bool deactivateObjectWhenReachingTargetPos) {
        
        float t = 0f;
        while (t < 1f) {
            transform.position = Vector2.Lerp(transform.position, targetPos, t);
            t += Time.unscaledDeltaTime / timeToFade;
            yield return null;
        }
        yield return null;

        if (deactivateObjectWhenReachingTargetPos)
            gameObject.SetActive(false);
    }
}
