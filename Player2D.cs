using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    // Public variables (editable in Inspector)
    public float speed = 5;
    public float jumpForce = 10;

    // Private variables
    SpriteRenderer sprite;
    Rigidbody2D rb;

    void Awake()
    {
        // Get the other components on this object (Assuming SpriteRenderer and Rigidbody2D)
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        // Get the Keyboard input (A and D or Arrow Keys)
        var horizontal = Input.GetAxisRaw("Horizontal");

        // Apply a velocity only on the X direction of the Rigidbody2D
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (horizontal > 0)
        {
            // Set Sprite to Look Right
            sprite.flipX = false;
        }
        else if (horizontal < 0)
        {
            // Set Sprite to Look Left
            sprite.flipX = true;
        }


        // Check is the player is Grounded
        // (This can be greatly improved with the Ground Layer check from Player3D)
        var grounded = Physics2D.OverlapCircle(transform.position + Vector3.down, 0.1f);

        // Get Keyboard Input for Jumping (Space)
        var jump = Input.GetKeyDown(KeyCode.Space);

        // If the player intends to jump this frame and is currently grounded
        if (jump && grounded)
        {
            // Set the Rigidbody velocity only on the Y direction
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }
}
