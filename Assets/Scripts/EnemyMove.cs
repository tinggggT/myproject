using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    Vector3 ePos;
    float enemyX;
    float startX;

    int hitCount = 0;
    int hitTotal = 3;

    public GameObject eBullet;
    float rnd = 0f;

    float myTime;
    float attkTime;
    Vector3 myPos;
    Vector3 playerPos;
    bool attack = false;
    bool backPos = false;
    float attackRND;

    private bool playerIsAlive = true; // 玩家是否存活

    void Start()
    {
        attackRND = Random.Range(3f, 20f);
        ePos = transform.position;
        startX = ePos.x;
        rnd = Random.Range(2f, 10f);

        InvokeRepeating("shoot", rnd, rnd);
    }

    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            playerIsAlive = false;
            CancelInvoke("shoot"); // 停止發射子彈
        }

        if (playerIsAlive)
        {
            myTime += Time.deltaTime;

            if (myTime > attackRND && !attack)
            {
                attack = true;
                myTime = 0;
                myPos = transform.position;
            }

            if (attack)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    playerPos = player.transform.position;
                    Vector3 direction = (playerPos - myPos).normalized;
                    transform.Translate(direction * Time.deltaTime * 0.5f);
                }

                attkTime += Time.deltaTime;
                if (attkTime > 5)
                {
                    attkTime = 0;
                    myTime = 0;
                    attack = false;
                    backPos = true;
                }
            }

            if (backPos)
            {
                transform.Translate((myPos - transform.position) * Time.deltaTime * 10);
                if (Vector3.Distance(transform.position, myPos) < 0.1f)
                {
                    transform.position = myPos;
                    backPos = false;
                }
            }

            enemyX = Mathf.PingPong(Time.time, 6) - 3 + startX;
            ePos = new Vector3(enemyX, transform.position.y, ePos.z);
            transform.position = ePos;
        }

        if (transform.position.y < -300)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bullet(Clone)")
        {
            hitCount++;
            if (hitCount >= hitTotal)
            {
                Destroy(gameObject);
            }
        }
    }

    void shoot()
    {
        if (playerIsAlive)
        {
            Instantiate(eBullet, transform.position, Quaternion.identity);
        }
    }
}
