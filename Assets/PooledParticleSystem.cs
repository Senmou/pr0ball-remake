using MarchingBytes;
using UnityEngine;

public class PooledParticleSystem : MonoBehaviour {

    public void OnParticleSystemStopped() {
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }
}
