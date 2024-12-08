using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float mySpeed = 3f;           // ���a���ʳt��
    public GameObject myBullet;          // �l�u��Prefab
    public float �l�u���Z = 0.5f;         // �l�u�o�g�����j

    private int hitCount = 0;            // ���a�Q����������
    private int hitTotal = 10;           // ���a�i�Ө����̤j��������
    private bool isGameOver = false;     // �קK�h���P���ΰ���

    void Start()
    {
        // �}�l�۰ʵo�g�l�u
        InvokeRepeating(nameof(�o�g�l�u), 0.5f, �l�u���Z);
    }

    void Update()
    {
        if (isGameOver) return;

        // ���a���k����
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(Vector3.left * mySpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(Vector3.right * mySpeed * Time.deltaTime);
        }
    }

    // �l�u�o�g�޿�
    void �o�g�l�u()
    {
        if (!isGameOver)
        {
            Instantiate(myBullet, this.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGameOver) return;

        // �p�G�I��ĤH���l�u
        if (other.transform.tag == "enemy")
        {
            hitCount++;
            Debug.Log($"���a�Q��������: {hitCount}");

            if (hitCount >= hitTotal)
            {
                Debug.Log("���a���ѡI");
                Destroy(gameObject);
                isGameOver = true;
                CancelInvoke(nameof(�o�g�l�u)); // ����o�g�l�u
            }

            Destroy(other.gameObject); // �P���ĤH�l�u
        }
    }
}