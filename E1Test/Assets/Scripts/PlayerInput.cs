using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 5f;
   
    private Vector2 movement = Vector2.zero;
    
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private TrailRenderer tr;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr.emitting = false;
    } 
    
    void OnMove(InputValue value)
    {
        Vector2 movement = value.Get<Vector2>();
        this.movement = movement;
    }

    void Update()
    {
        Move(movement.x, movement.y);
    }
    
    private void Move(float x, float z)
    {
        rb.velocity = new Vector3(x * speed, rb.velocity.y, z * speed);
    }



// ----- my Dash code added ------  \\

    void OnDash(InputValue value)
    {
        StartCoroutine(Dash(movement.x, movement.y));
    }

    private IEnumerator Dash(float x, float z)
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashDuration)
        {
            tr.emitting = true;
            rb.velocity = new Vector3(x * dashSpeed, rb.velocity.y, z * dashSpeed);
            yield return null;
        }  
        tr.emitting = false;
    }


}