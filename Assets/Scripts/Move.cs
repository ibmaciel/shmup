using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector2 speed = new Vector2(10, 10);

    public Vector2 direction = new Vector2(-1, 0);

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    private void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    void Update()
    {        
        movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
    }

    void FixedUpdate()
    {
        if (rigidbodyComponent == null) return;
        
        rigidbodyComponent.velocity = movement;
    }
}
