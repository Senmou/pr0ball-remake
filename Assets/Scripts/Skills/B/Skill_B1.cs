using MarchingBytes;
using System.Collections;
using UnityEngine;

public class Skill_B1 : Skill {

    private const string orangeBallPoolName = "OrangeBallPool";

    [SerializeField] private GameObject b1_cannon;
    [SerializeField] private GameObject orangeBall;

    private float ballsPerSecond = 5f;
    private AudioSource audioSource;

    private new void Awake() {
        base.Awake();
        audioSource = GameObject.Find("SfxSpawn").GetComponent<AudioSource>();
    }

    protected override IEnumerator ActionCoroutine() {

        b1_cannon.SetActive(true);
        audioSource.PlayOneShot(audioSource.clip);

        while (pending) {
            if (ballController.BallCount > 0)
                pending = false;
            yield return null;
        }

        while (ballController.BallCount > 0) {
            ShootBall();
            yield return new WaitForSeconds(1f / ballsPerSecond);
        }

        b1_cannon.SetActive(false);
        yield return null;
    }

    private void ShootBall() {
        Vector3 spawnPoint = b1_cannon.transform.position;
        OrangeBall ball = EasyObjectPool.instance.GetObjectFromPool(orangeBallPoolName, spawnPoint, Quaternion.identity).GetComponent<OrangeBall>();
        ball.body.AddForce(b1_cannon.transform.up * 200f, ForceMode2D.Impulse);
    }
}
