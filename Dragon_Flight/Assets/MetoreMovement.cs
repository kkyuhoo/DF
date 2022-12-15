using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetoreMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 9f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.down * Time.deltaTime * speed;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    //?¥Îèô Í≤ΩÎ°ú???åÎ??†Ïù¥ Í∑∏Î†§ÏßÄÍ≤??òÎäî ÏΩîÎìú
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 10f);
    }
}