// Script to control reticle position
using UnityEngine;

public class TrackTarget : MonoBehaviour
{
    public Transform _camera;
    Transform _target;

    public Transform Target { set { _target = value; }}

    void Update()
    {
        if (_target != null) 
            transform.position = Vector3.Lerp(_camera.position, _target.position, 0.9f);
    }
}
