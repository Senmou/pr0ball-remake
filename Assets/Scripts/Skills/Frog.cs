using MarchingBytes;
using System.Collections;
using UnityEngine;

public class Frog : MonoBehaviour {

    [SerializeField] private float detonationRadius;
    [SerializeField] private new ParticleSystem particleSystem;

    private AudioSource audioSource;
    private EnemyController enemyController;

    private int explosionDamage = 0;

    public void SetDamage(int damage) {
        explosionDamage = damage;
    }

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(1f, 1.3f);
        enemyController = FindObjectOfType<EnemyController>();
    }

    private void OnEnable() {
        StartCoroutine(ReturnToPool());
    }

    private IEnumerator ReturnToPool() {
        yield return new WaitForSeconds(3f);
        if (gameObject.activeInHierarchy)
            EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {

            BaseEnemy[] activeEnemiesCopy = new BaseEnemy[enemyController.activeEnemies.Count];
            enemyController.activeEnemies.CopyTo(activeEnemiesCopy);

            int limit = activeEnemiesCopy.Length;
            for (int i = 0; i < limit; i++) {
                float distanceToEnemy = Vector2.Distance(transform.position, activeEnemiesCopy[i].transform.position);
                if (distanceToEnemy < detonationRadius) {
                    activeEnemiesCopy[i].TakeDamage(explosionDamage);
                    Statistics.Instance.skills.skill_2.damageDealt += explosionDamage;
                }
            }

            Instantiate(particleSystem, transform.position, Quaternion.identity);
            if (gameObject.activeInHierarchy)
                EasyObjectPool.instance.ReturnObjectToPool(gameObject);
            CameraEffect.instance.Shake(0.2f, 1f);
        }
    }
}
