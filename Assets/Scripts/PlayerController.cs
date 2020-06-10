using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 speed = new Vector2(50, 50);
        
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    private Vector3 rendererSize;

    private void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        rendererSize = GetComponent<Renderer>().bounds.size;
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        movement = new Vector2(speed.x * inputX, speed.y * inputY);

        bool shoot = Input.GetButtonDown("Fire1");
        shoot |= Input.GetButtonDown("Fire2");

        if (shoot)
        {
            Weapon weapon = GetComponent<Weapon>();
            if (weapon != null)
            {
                weapon.Attack(false);
            }
        }

        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;

        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;

        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, leftBorder + rendererSize.x / 2, rightBorder - rendererSize.x / 2),
          Mathf.Clamp(transform.position.y, topBorder + rendererSize.y / 2, bottomBorder - rendererSize.y / 2),
          transform.position.z
        );
    }

    void FixedUpdate()
    {        
        if (rigidbodyComponent == null) return;

        rigidbodyComponent.velocity = movement;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool damagePlayer = false;

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {            
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);

            damagePlayer = true;
        }
                
        if (damagePlayer)
        {
            Health playerHealth = this.GetComponent<Health>();
            if (playerHealth != null) playerHealth.Damage(1);
        }
    }
}
