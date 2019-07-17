using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour {

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void OnClickBackButton() {
        SceneManager.LoadScene(0);
    }

    public void FadeIn() {
        animator.SetTrigger("FadeIn");
    }

    public void FadeOut() {
        animator.SetTrigger("FadeOut");
    }
}
