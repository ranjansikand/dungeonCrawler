// Script to control disappearing platforms
// Requires dissolve shader

using UnityEngine;

public class Dis_Platform : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Collider _collider;
    private float t = 0.0f;
    private Material[] mats;
    private int shaderVariable;

    [SerializeField] float speed = .15f;

    private void Start(){
        meshRenderer = this.GetComponent<MeshRenderer>();
        _collider = this.GetComponent<Collider>();

        mats = meshRenderer.materials;
        meshRenderer.materials = mats;
        shaderVariable = Shader.PropertyToID("_Cutoff");
    }

    
    private void Update(){
        mats[0].SetFloat(shaderVariable, Mathf.Sin(t * speed));
        t += Time.deltaTime;
        
        if (mats[0].GetFloat(shaderVariable) > 0.5f) {
            _collider.enabled = false;
        } else {
            _collider.enabled = true;
        }
    }
}
