using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFade : MonoBehaviour
{
    [SerializeField] Material _dissolve;
    [SerializeField] GameObject _particles;
    [SerializeField] float _effectDuration;

    private Renderer[] _renderers;
    private ParticleSystem _thisParticle;
    private int _shaderVariable;

    void Start()
    {
        _renderers = GetComponentsInChildren<Renderer>();
        // Slightly improves perfomance to call by ID
        _shaderVariable = Shader.PropertyToID("_Cuttoff");
    }

    public void Dissolve()
    {
        for (int i = 0; i < _renderers.Length; i++) {
            // Change all shaders to this one
            _renderers[i].material = _dissolve;
            // Start with no dissolving
            _renderers[i].material.SetFloat(_shaderVariable, 0);
        }

        // Adjust particle system to match effect length
        _thisParticle = Instantiate(_particles, transform.position, Quaternion.identity)
            .GetComponent<ParticleSystem>();
        var main = _thisParticle.main;
        main.duration = _effectDuration;

        // Once setup is complete, begin the effect
        _thisParticle.Play();
        StartCoroutine(IDissolve());
    }

    IEnumerator IDissolve()
    {
        // Controls the change per frame
        float previousValue = 0;

        while (previousValue < 1) {
            // Accesses the entire list of this model's materials
            for (int i = 0; i < _renderers.Length; i++) {
                // Adjust the dissolve value for each material
                _renderers[i].material.SetFloat(_shaderVariable, previousValue + (Time.deltaTime/_effectDuration));
            }
            // Update the reference value
            previousValue = _renderers[0].material.GetFloat(_shaderVariable);
            yield return null;
        }

        // Once the object is invisible, destroy it (with delay, just in case)
        Invoke(nameof(DeleteThisObject), 0.35f);
    }

    void DeleteThisObject()
    {
        Destroy(gameObject);
    }
}
