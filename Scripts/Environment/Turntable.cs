// script for object that rotates golems
using UnityEngine;

public enum Direction {
    Right = 90,
    Left = -90,
}
[RequireComponent(typeof(Rigidbody))]
public class Turntable : MonoBehaviour
{
    [SerializeField] Direction turnDirection;
    [SerializeField] bool flipDirection = false;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }

    void FlipDirection()
    {
        if (turnDirection == Direction.Right) {
            turnDirection = Direction.Left;
        } else {
            turnDirection = Direction.Right;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        var hit = other.gameObject.GetComponent<IRotatable>();
        hit?.TurnAxis((int)turnDirection);

        if (flipDirection) FlipDirection();
    }
}
