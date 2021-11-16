using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    Transform target;

    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (target != null) transform.LookAt(target.position + Vector3.up);
    }

    public void UpdateTarget(Transform newTarget)
    {
        target = newTarget;
        Debug.Log("Updated target");
    }
}
