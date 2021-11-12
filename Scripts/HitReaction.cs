using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReaction : MonoBehaviour, IDamagable
{
    [SerializeField] Animator _animator;
    [SerializeField] Item drop;

    int hits = 0;
    
    public void Damage(int damage)
    {
        _animator.SetTrigger("hurt");
        hits++;
        Debug.Log("Hit " + hits + " time(s)!");
    }
}
