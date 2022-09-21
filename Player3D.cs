using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3D : MonoBehaviour
{
    public float moveSpeed = 5;
    public float lookSpeed = 1;
    public float jumpHeight = 5;
    
    // Make sure to set a LayerMask in Unity so this only checks the Ground objects
    public LayerMask groundLayer;
    
    private Rigidbody rb;
    private bool jump;

    private void Awake()
    {
        // Get the Rigidbody component that is on this object
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Lock the cursor in the game (No longer visible while playing)
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Get Mouse Movement (Left Right)
        var horizontalMouse = Input.GetAxisRaw("Mouse X");

        // Calculate a rotation offset (Y Only) and apply the look speed
        var rotationOffset = new Vector3(0, horizontalMouse, 0) * lookSpeed;

        // Apply this rotation (Multiply because it is a Quaternion) to the transform
        transform.rotation *= Quaternion.Euler(rotationOffset);


        // If the Space Key was pressed down this frame
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Set jump to true
            jump = true;
        }
    }
    
    private void FixedUpdate()
    {
        // Get Keyboard Input
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        // Apply the keyboard input to the transform (Get which way player should move)
        var moveDir = horizontal * transform.right + vertical * transform.forward;

        // Normalize the movement direction (Make it at a magnitude of 1) and apply the movement speed
        var move = moveDir.normalized * moveSpeed;


        // Apply the calculated movement to the rigidbody's X and Z components only
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
        

        // Check to see if the player is Grounded (See OnDrawGizmos for visual reference)
        bool grounded = Physics.CheckSphere(transform.position, 0.1f, groundLayer);
        
        // If the player should jump
        if (jump && grounded)
        {
            // Apply an Upwards Force to the player
            rb.AddForce(0, jumpHeight, 0, ForceMode.VelocityChange);
            // Reset the jump boolean
            jump = false;
        }
    }

    private void OnDrawGizmos()
    {
        // Set the gizmo color to Red (Aesthetic)
        Gizmos.color = Color.red;
        // Draw a visual representation of the Ground Check Sphere
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
