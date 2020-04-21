using MarchingBytes;
using UnityEngine;

public class PooledParticleSystem : MonoBehaviour {

    [HideInInspector] public new ParticleSystem particleSystem;

    private ParticleSystem.MainModule mainModule;
    private ParticleSystemRenderer particleSystemRenderer;

    private void Awake() {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
        mainModule = particleSystem.main;
    }

    public void SetColor(Color color) {
        mainModule.startColor = color;
    }

    public void OnParticleSystemStopped() {
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }
}
