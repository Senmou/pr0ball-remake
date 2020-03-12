using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Skill_Hammertime : Skill {

    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioClip[] hitAudioClips;
    [SerializeField] private GameObject hammerTimeLogo;
    [SerializeField] private GameObject onHitParticleSystem;

    public GameObject hammer;
    public ContactFilter2D contactFilter;

    private BoxCollider2D hammerCollider;
    private AudioSource audioSource;

    private new void Awake() {
        base.Awake();
        hammer = Instantiate(hammer);
        hammerCollider = hammer.GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[0];
        hammer.SetActive(false);

        hammerTimeLogo = Instantiate(hammerTimeLogo, new Vector3(-30f, 9f), Quaternion.identity);
        hammerTimeLogo.gameObject.SetActive(false);
    }

    private void Start() {
        description = "pr0-chan";
    }

    protected override int CalcDamage() => cost + cost * (enemyHPReference.MaxHP / 8);

    protected override IEnumerator ActionCoroutine() {

        Statistics.Instance.skills.skill_1.used++;

        float xPos = Random.Range(-8f, 7.4f);
        hammer.transform.position = new Vector3(xPos, 26f);
        Vector2 startPos = hammer.transform.position;
        Vector2 targetPos = startPos + new Vector2(0f, -52f);

        List<BaseEnemy> hitEnemies = new List<BaseEnemy>();

        hammer.SetActive(true);
        float fallingSpeed = 1.5f;

        int randomAudioClip = Random.Range(0, audioClips.Length - 1);
        audioSource.clip = audioClips[randomAudioClip];
        audioSource.Play();

        StartCoroutine(ShowHammerTimeLogo());

        while (Vector2.Distance(hammer.transform.position, targetPos) > 0.1f) {
            hammer.transform.position = Vector2.MoveTowards(hammer.transform.position, targetPos, fallingSpeed * Time.deltaTime);
            if (!GameController.isGamePaused)
                fallingSpeed += 0.7f;

            Collider2D[] hits = new Collider2D[16];
            int numHits = Physics2D.OverlapCollider(hammerCollider, contactFilter, hits);

            for (int i = 0; i < numHits; i++) {
                BaseEnemy hitEnemy = hits[i].GetComponentInParent<BaseEnemy>();

                if (hitEnemy && hitEnemy.canTakeDamageFromSkill) {
                    int randomHitAudioClip = Random.Range(0, hitAudioClips.Length - 1);
                    audioSource.PlayOneShot(hitAudioClips[randomHitAudioClip], 0.4f);

                    ParticleSystemRenderer psr = Instantiate(onHitParticleSystem, hitEnemy.transform.position, Quaternion.identity).GetComponent<ParticleSystemRenderer>();
                    psr.material.color = hitEnemy.GetColor();

                    hitEnemy.TakeDamage(TotalDamage);
                    hitEnemy.canTakeDamageFromSkill = false;
                    hitEnemies.Add(hitEnemy);

                    CameraEffect.instance.Shake(0.05f, 0.5f);
                    Statistics.Instance.skills.skill_1.damageDealt += TotalDamage;
                }
            }

            yield return null;
        }

        foreach (var enemy in hitEnemies) {
            enemy.canTakeDamageFromSkill = true;
        }

        pending = false;

        StopAllCoroutines();
        hammerTimeLogo.gameObject.SetActive(false);

        yield return null;
    }

    private IEnumerator ShowHammerTimeLogo() {

        hammerTimeLogo.gameObject.SetActive(true);

        Vector3 startPos = new Vector3(-30f, 9f);
        Vector3 midPos = new Vector3(0f, 9f);

        hammerTimeLogo.transform.position = startPos;
        while (Vector2.Distance(hammerTimeLogo.transform.position, midPos) > 0.1f) {
            hammerTimeLogo.transform.position = Vector2.Lerp(hammerTimeLogo.transform.position, midPos, 12f * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(HideHammerTimeLogo());
    }

    private IEnumerator HideHammerTimeLogo() {

        Vector3 endPos = new Vector3(30f, 9f);

        while (Vector2.Distance(hammerTimeLogo.transform.position, endPos) > 0.1f) {
            hammerTimeLogo.transform.position = Vector2.Lerp(hammerTimeLogo.transform.position, endPos, 12f * Time.deltaTime);
            yield return null;
        }

        hammerTimeLogo.gameObject.SetActive(false);
    }
}
