using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveEnvironment : MonoBehaviour
{
    [Header("Disappearing Objects")]

    [SerializeField] bool disappears;
    [SerializeField] GameObject disappearEffect;
    [SerializeField] GameObject lockEffect;

    public void Toggle()
    {
        if (disappears)
        {
            if (gameObject.activeSelf)
            {
                if (disappearEffect != null) Instantiate(disappearEffect, transform.position + Vector3.up, transform.rotation);
                gameObject.SetActive(false);
            }
            else if (!gameObject.activeSelf)
            {
                if (lockEffect != null) Instantiate(lockEffect, transform.position + Vector3.up, transform.rotation);
                gameObject.SetActive(true);
            }
        }

    }
}