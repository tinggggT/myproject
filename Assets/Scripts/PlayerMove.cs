using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float mySpeed = 3f;           // 玩家移動速度
    public GameObject myBullet;          // 子彈的Prefab
    public float 子彈間距 = 0.5f;         // 子彈發射的間隔

    private int hitCount = 0;            // 玩家被擊中的次數
    private int hitTotal = 10;           // 玩家可承受的最大擊中次數
    private bool isGameOver = false;     // 避免多次銷毀或執行

    void Start()
    {
        // 開始自動發射子彈
        InvokeRepeating(nameof(發射子彈), 0.5f, 子彈間距);
    }

    void Update()
    {
        if (isGameOver) return;

        // 玩家左右移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(Vector3.left * mySpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(Vector3.right * mySpeed * Time.deltaTime);
        }
    }

    // 子彈發射邏輯
    void 發射子彈()
    {
        if (!isGameOver)
        {
            Instantiate(myBullet, this.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGameOver) return;

        // 如果碰到敵人的子彈
        if (other.transform.tag == "enemy")
        {
            hitCount++;
            Debug.Log($"玩家被擊中次數: {hitCount}");

            if (hitCount >= hitTotal)
            {
                Debug.Log("玩家失敗！");
                Destroy(gameObject);
                isGameOver = true;
                CancelInvoke(nameof(發射子彈)); // 停止發射子彈
            }

            Destroy(other.gameObject); // 銷毀敵人子彈
        }
    }
}