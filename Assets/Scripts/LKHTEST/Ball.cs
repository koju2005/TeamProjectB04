using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D Rb;
    private Vector2 force;
    public float speed = 5f;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Invoke(nameof(SetRandomTrajectory), 1f);
    }

    private void SetRandomTrajectory()
    {
        force = Vector2.zero;
        force.x = Random.Range(-0.5f, 0.5f);
        force.y = -1;

        Rb.AddForce(force.normalized * speed);
    }

    void Update()
    {
        if (Rb.velocity != Vector2.zero)
        {
            Rb.velocity = Rb.velocity.normalized * speed;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (Rb.velocity.y < speed * 0.1 && Rb.velocity.y > speed * (-0.1))
        {
            if (Rb.velocity.y <= 0)
            {
                Rb.velocity += new Vector2(0, -0.3f) * speed;
            }
            if (Rb.velocity.y > 0)
            {
                Rb.velocity += new Vector2(0, 0.3f) * speed;
            }
        }
    }

    public void SetPosition(float x, float y)
    {
        transform.position = new Vector2(x, y);
    }

    public void ForceBall(float x, float y)
    {
        force.x = x;
        force.y = y;

        Rb.AddForce(force.normalized * speed);
    }
}
