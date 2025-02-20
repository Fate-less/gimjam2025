using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roam : State
{
    public float speed;
    float newYPOS;
    float generateYPOSCooldown = 0.5f;
    float currentGenerateYPOSCooldown;

    void Update()
    {

        if (gameObject.transform.position.y > 3.3f)
        {
            transform.position = new Vector2(transform.position.x, 3.3f);
        }
        else if (gameObject.transform.position.y < -3.3f)
        {
            transform.position = new Vector2(transform.position.x, -3.3f);
        }
        currentGenerateYPOSCooldown -= Time.deltaTime;
        if(currentGenerateYPOSCooldown <= 0)
        {
            Random.Range(-3.2f, 3.2f);
            Debug.Log("New YPOS generated");
            currentGenerateYPOSCooldown = generateYPOSCooldown;
        }
        Vector2 movement = new Vector2(0, newYPOS) * speed * Time.deltaTime;

        // Move paddle
        transform.Translate(movement);
    }
}
