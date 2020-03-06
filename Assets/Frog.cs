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
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {

            BaseEnemy[] activeEnemiesCopy = new BaseEnemy[enemyController.activeEnemies.Count];
            enemyController.activeEnemies.CopyTo(activeEnemiesCopy);

            foreach (var enemy in activeEnemiesCopy) {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < detonationRadius) {
                    enemy.TakeDamage(explosionDamage);
                    Statistics.skills.skill_2.damageDealt += explosionDamage;
                }
            }

            Instantiate(particleSystem, transform.position, Quaternion.identity);
            EasyObjectPool.instance.ReturnObjectToPool(gameObject);
            CameraEffect.instance.Shake(0.2f, 1f);
        }
    }
}
