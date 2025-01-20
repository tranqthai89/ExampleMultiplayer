using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    private float moveHorizontal, moveVertical;
    private Vector2 movement;

    void Start()
    {
        movement = Vector2.zero;
    }
    void Reset()
    {
        Debug.Log("[PlayerController] Reset");
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 4f;
    }
    void Update(){
        if(!IsOwner){return;}
        Move();
    }
    void Move(){
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        movement.x = moveHorizontal;
        movement.y = moveVertical;

        rb.velocity = movement * moveSpeed;
    }
}
