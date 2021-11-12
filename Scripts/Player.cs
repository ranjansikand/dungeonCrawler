using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Inventory playerInventory;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;

    [Header("Movement")]

    [SerializeField, Range(1, 20)] float speed = 3;
    [SerializeField, Range(1, 10)] float jumpVelocity = 2f;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;

    [SerializeField, Range(0, 5)] float rayDist = 0.65f;
    [SerializeField] int wallLayer, groundLayer;

    int wallLayerMask, groundLayerMask;
    Vector3 displacement;
    bool jumping;

    [Header("Health")]

    [SerializeField] int health = 3;
    [SerializeField] List<int> dangerLayers = new List<int>();
    [SerializeField] float invulnerableTime = 1;

    public bool invulnerable;
    bool dead;

    [Header("Interactions")]

    [SerializeField] float interactionRadius;
    [SerializeField] int objectLayer;

    static Collider[] objectsBuffer = new Collider[100];
    int objectLayerMask;


    void Start()
    {
        wallLayerMask = 1 << wallLayer;
        groundLayerMask = 1 << groundLayer;
        objectLayerMask = 1 << objectLayer;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        anim.SetBool("Walk", true);
        
        Vector2 inputMovement = value.ReadValue<Vector2>();
        displacement = new Vector3(inputMovement.x, 0, inputMovement.y);

        if (displacement != Vector3.zero && !dead) {
            transform.forward = displacement;
        } else {
            anim.SetBool("Walk", false);
        }
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.started && Physics.Raycast(transform.position, -1 * transform.up, rayDist, groundLayerMask) && !dead)
        {
            rb.velocity += Vector3.up * jumpVelocity;
            jumping = true;
        } 
        else {
            jumping = false;
        }
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started && !dead)
        {
            anim.SetTrigger("Attack");
        }
    }

    public void OnInteract(InputAction.CallbackContext value)
    {
        if (value.started && !dead)
        {
            int hits = Physics.OverlapSphereNonAlloc(
                transform.position, interactionRadius, objectsBuffer, objectLayerMask, QueryTriggerInteraction.Collide
                );

            if (hits > 0)
            {
                for (int i = 0; i < hits; i++)
                {
                    // interact with objects in objects buffer
                    objectsBuffer[i].GetComponent<Interactable>().Interact();
                }
            }
        }
    }

    public void OnInventoryToggled(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            playerInventory.ToggleInventory();
        }
    }

    void FixedUpdate()
    {
        if (!dead) {
            if (health <= 0) Die();

            if (!Physics.Raycast(transform.position, transform.forward, rayDist, wallLayerMask))
            {
                rb.MovePosition(transform.position + displacement * Time.fixedDeltaTime * speed);
            }

            if (rb.velocity.y < 0) {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier -1) * Time.deltaTime;
            } else if (rb.velocity.y > 0 && !jumping) {
                rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (invulnerable) return;

        for (int i = 0; i < dangerLayers.Count; i++) {
            if (other.gameObject.layer == dangerLayers[i]) {
                Hurt();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (invulnerable) return;

        for (int i = 0; i < dangerLayers.Count; i++) {
            if (other.gameObject.layer == dangerLayers[i]) {
                Hurt();
            }
        }
    }

    public void Hurt()
    {
        invulnerable = true;
        health--;

        Invoke(nameof(ResetHealth), invulnerableTime);
    }

    public void Delay()
    {
        Invoke(nameof(ResetHealth), 0.5f);
    }

    void ResetHealth()
    {
        invulnerable = false;
    }

    void Die()
    {
        dead = true;
        anim.SetTrigger("Die");
    }
}