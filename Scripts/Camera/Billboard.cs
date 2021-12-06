// Faces toward Camera
// Script is meant for world-space UI such as
// healthbars, reticles, or dialog boxes

using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform mainCamera;

    void Start()
    {
        if (mainCamera == null) mainCamera = GameObject.FindWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.forward);
    }
}
