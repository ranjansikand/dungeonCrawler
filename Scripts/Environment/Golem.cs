using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : MonoBehaviour, IDamagable
{
    [SerializeField] Vector3[] _waypoints;
    int _previousWaypoint = -1;

    WaitForSeconds _delay = new WaitForSeconds(0.5f);
    bool _walking = false;

    public void Damage(int damage) {
        _walking = true;

        if (_previousWaypoint < _waypoints.Length) {
            _previousWaypoint++;
        } else {
            _previousWaypoint = 0;
        }
        
       StartCoroutine(ICheckPath());
    }

    public int MaxHealth() { return 0; }

    IEnumerator ICheckPath()
    {
        while (_walking) {
            if (Physics.Raycast(transform.position, transform.forward, 2f)) {
                Vector3.MoveTowards(transform.position, _waypoints[_previousWaypoint], 0.5f);
            }

            if (Vector3.Distance(transform.position, _waypoints[_previousWaypoint]) < 1) {
                _walking = false;
                StopCoroutine(ICheckPath());
            }

            yield return _delay;
        }
    }
}
