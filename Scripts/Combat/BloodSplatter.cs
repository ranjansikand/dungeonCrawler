// Script attached to blood splatter effects

using UnityEngine;

public class BloodSplatter : MonoBehaviour
{  
    [SerializeField] float _particleLifetime;
    
    void Awake()
    {
        Invoke("ParticleDestruction", _particleLifetime);
    }

    void ParticleDestruction()
    {
        Destroy(gameObject);
    }
}
