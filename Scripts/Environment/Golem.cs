// Script to make golems move
using UnityEngine;

public class Golem : MonoBehaviour, IDamagable, IRotatable
{
    Animator _animator;
    int _walkingHash;

    [SerializeField] Vector3 _direction;
    [SerializeField] float _speed;

    bool _walk = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _walkingHash = Animator.StringToHash("Walk");

        _direction = transform.forward;
    }

    void Update()
    {
        if (_walk) {
            if (Physics.SphereCast(new Ray(transform.position + Vector3.up, _direction), .1f, 1.5f)) {
                _walk = false;
                _animator.SetBool(_walkingHash, false);
            } else {
                transform.position += _direction * _speed * Time.deltaTime;
            }
        }
    }

    public void Damage(int damage) {
        // start walking in the opposite direction
        _animator.SetBool(_walkingHash, true);
        _walk = true;

        FaceNewDirection();
    }

    public int CurrentHealth() { return 0; }

    void FaceNewDirection() {
        _direction *= -1;
        transform.forward = _direction;
    }

    public void TurnAxis(int rotation)
    {
        transform.Rotate(new Vector3(0, rotation, 0));
        _direction = transform.forward;
    }
}
