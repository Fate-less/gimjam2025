using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    public float speed;
    public Vector2 movement;
    private GameObject ballObject;
    private Rigidbody2D rb;
    
    void Start()
    {
        ballObject = GameObject.FindGameObjectWithTag("Ball");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ChaseBall();
    }

    public void ChaseBall()
    {
        // Calculate movement
        if(gameObject.transform.position.y > 3.3f)
        {
            transform.position = new Vector2(transform.position.x,3.3f);
        }
        else if (gameObject.transform.position.y < -3.3f)
        {
            transform.position = new Vector2(transform.position.x, -3.3f);
        }
        float ballPosition = Mathf.Clamp(ballObject.transform.position.y, -3.3f, 3.3f);
        movement = new Vector2(0, ballPosition) * speed * Time.deltaTime;

        // Move paddle
        transform.Translate(movement);
    }
}
