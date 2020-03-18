using MarchingBytes;
using UnityEngine;

public class PooledParticleSystem : MonoBehaviour {

    [HideInInspector] public new ParticleSystem particleSystem;
    private ParticleSystemRenderer particleSystemRenderer;

    private void Awake() {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
    }

    public void SetColor(Color color) {
        particleSystemRenderer.sharedMaterial.SetColor(Shader.PropertyToID("Color_65DE3E46"), color);
    }

    public void OnParticleSystemStopped() {
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }
}
