using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMovement : MonoBehaviour
{
    Rigidbody2D rigid;
    public float speed;

    public float startHealth;
    public float health;

    public GameObject healthBar;
    public GameObject itemCoin;
    public GameObject itemPower;
    public GameObject itemSpecialMove;

<<<<<<< HEAD
    PlayerController playerController;
=======
    public List<GameObject> childObject = new List<GameObject>();
>>>>>>> origin/jaeuk

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * speed;
    }
    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);

            Destroy(collision.gameObject);
        }
    }
    void OnHit(int dmg)
    {
        if(health <= 0)
            return;

        health -= dmg;
        healthBar.GetComponent<Image>().fillAmount = health / startHealth;

        if (health <= 0)
        {

            // #.Random Radio Item Drop
            float ran = Random.Range(0, 10);
            if(ran < 5)
            {
                Debug.Log("Not Item");
            }
            else if(ran < 8.8) // Coin 38%
            {
                Instantiate(itemCoin, transform.position, transform.rotation);
                playerController.PlaySound("ITEMDROP");
            }
            else if(ran < 9.5) // Power 10%
            {
                Instantiate(itemPower, transform.position, transform.rotation);
                playerController.PlaySound("ITEMDROP");
            }
            else if(ran < 10) // SpecialMove 5%
            {
                Instantiate(itemSpecialMove, transform.position, transform.rotation);
                playerController.PlaySound("ITEMDROP");
            }

            //for문 childObject 비활성하
            for (int i = 0; i < childObject.Count; i++)
            {
                childObject[i].SetActive(false);
            }

            //애니메이션 트리거 Death
            gameObject.GetComponent<Animator>().SetTrigger("Death");
            Destroy(gameObject,0.5f);
        }
    }

}
