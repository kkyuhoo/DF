using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    //
    Rigidbody2D rb;
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 아래로 가다가 y축이 2.5보다 작아지면 멈춘다.
        if (transform.position.y > 2f)
        {
            rb.velocity = new Vector2(0, -speed * Time.deltaTime);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
