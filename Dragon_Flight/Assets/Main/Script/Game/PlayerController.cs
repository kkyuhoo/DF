using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    private bool isTouchTop;
    private bool isTouchBottom;
    private bool isTouchLeft;
    private bool isTouchRight;

    public float speed;
    public int power;
    public int maxPower;

    public int specialMove;
    public int maxSpecialMove;

    public float maxShotDelay;
    public float curShotDelay;
    public float curSpecialMoveDelay;
    public float maxSpecialMoveDelay;

    public float playerHp = 3f;
    public GameObject hp1;
    public GameObject hp2;
    public GameObject hp3;
    public GameObject mainCamera;
    public GameObject panel;

    public GameObject bulletSpecialMove;
    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject bulletObjC;
    public GameObject bulletObjD;

    private bool isSpecialMoveTime = false;
    public bool isFire;

    // Manager
    public GameManager gameManager;
    public ObjectManager objectManager;

    Animator anim;

    // 오디오 변수 선언
    
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;
    public AudioClip audioMonsterDeath;
    public AudioClip audioItemDrop;




    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string action)
    {
        switch (action)
        {
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                break;
            case "MONSTER":
                audioSource.clip = audioMonsterDeath;
                break;
            case "ITEMDROP":
                audioSource.clip = audioItemDrop;
                break;

        }
        audioSource.Play();
    }

    void Update()
    {
        Move();
        Fire();
        BulletSpecialMove();
        Reload();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Monster 태그와 충돌시 HP--
        if (collision.gameObject.tag == "Monster")
        {
            print("Player HP--");
            if(!isFire == false)
            {
                playerHp--;
                mainCamera.GetComponent<Animator>().SetTrigger("CameraShake");
            }
            
            if (playerHp == 2)
            {
                print("hp3 false");
                hp3.SetActive(false);
            }
            else if (playerHp == 1)
            {
                print("hp2 false");
                hp2.SetActive(false);
            }
            else if (playerHp == 0)
            {
                // 오디오 
                PlaySound("Die");
                print("hp1 false");
                hp1.SetActive(false);
                //일시정지
                Time.timeScale = 0;
                //panel setactive
                panel.SetActive(true);
            }

            // 오디오 피격
            PlaySound("DAMAGED");
        }

        // 플레이어 범위 제한 설정
        if (collision.gameObject.tag == "Border")
        {
            switch(collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
            }
        }

        // 아이템 충돌 처리
        if(collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Coin":

                    //gold +1000
                    gold.score += 1000;
                    //scroe += 1000;
                    break;
                case "Power":
                    if(power == maxPower)
                    {
                        //scroe += 500;
                    }
                    else
                    {
                        power++;    
                    }
                    break;

                case "Invincible":
                    // 무적 효과
                    break;

                case "Specialmove":
                    // 필살기
                    if (specialMove == maxSpecialMove)
                    {
                        Debug.Log("필살기 더이상 축적 불가능");
                    }
                        //scroe += 500;
                    else
                    {
                        specialMove++;
                    }

                    break;

            }
            // 오디오 아이템
            PlaySound("ITEM");

            Destroy(collision.gameObject);
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        // 플레이어 범위 제한 설정
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;                    
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
            }
        }
    }

    void Move()
    {
        // 플레이어 이동
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
        {
            h = 0;
        }
        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == -1) || (isTouchBottom && v == 1))
        {
            v = 0;
        }
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
    }

    void Fire()
    {
        if (curShotDelay < maxShotDelay)
            return;

        if (!isFire)
            return;

        switch (power)
        {
            case 1:
                Vector3 positionVector_A = new Vector3(transform.position.x, transform.position.y, -1);
                GameObject bullet_A = Instantiate(bulletObjA, positionVector_A, transform.rotation);
                Rigidbody2D rigid_A = bullet_A.GetComponent<Rigidbody2D>();
                rigid_A.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                Vector3 positionVector_B = new Vector3(transform.position.x, transform.position.y, -1);
                GameObject bullet_B = Instantiate(bulletObjB, positionVector_B, transform.rotation);
                Rigidbody2D rigid_B = bullet_B.GetComponent<Rigidbody2D>();
                rigid_B.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                break;
            case 3:
                Vector3 positionVector_C = new Vector3(transform.position.x, transform.position.y, -1);
                GameObject bullet_C = Instantiate(bulletObjC, positionVector_C, transform.rotation);
                Rigidbody2D rigid_C = bullet_C.GetComponent<Rigidbody2D>();
                rigid_C.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                // Follower 오브젝트 활설화
                GameObject.Find("Player").transform.Find("Follewer_01").gameObject.SetActive(true);

                break;
            case 4:
                Vector3 positionVector_D = new Vector3(transform.position.x, transform.position.y, -1);
                GameObject bullet_D = Instantiate(bulletObjD, positionVector_D, transform.rotation);
                Rigidbody2D rigid_D = bullet_D.GetComponent<Rigidbody2D>();
                rigid_D.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                // Follower 오브젝트 활설화
                GameObject.Find("Player").transform.Find("Follewer_02").gameObject.SetActive(true);

                break;
        }
        PlaySound("ATTACK");
        curShotDelay = 0;

    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void OffBulletSpecialMove()
    {
        bulletSpecialMove.SetActive(false);
    }
    void BulletSpecialMove()
    {
        if (!Input.GetButton("Jump"))
            return;

        if (specialMove == 0)
            return;

        if (isSpecialMoveTime == false)
        {
            StopFire();
            isSpecialMoveTime = true;
            InvokeRepeating("bulletInvoke", 0, 0.125f);
            StartCoroutine(PlayMove());
            specialMove--;
        }
        else
        {
            Debug.Log("아직 필살기를 사용할 수 없습니다. ");
        }
    }

    void bulletInvoke()
    {
        Vector3 specialVector = new Vector3(transform.position.x, transform.position.y, -1);
        GameObject Special = Instantiate(bulletSpecialMove, specialVector, transform.rotation);
        Rigidbody2D specialMoveRigid = Special.GetComponent<Rigidbody2D>();
        specialMoveRigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }

    IEnumerator PlayMove()
    {
        yield return new WaitForSeconds(3.0f);
        CancelInvoke("bulletInvoke");
        isSpecialMoveTime = false;
        isFire = true;
    }
    void StopFire()
    {
        isFire = false;
    }
}
