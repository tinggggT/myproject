using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public Text resultText;               // ��ܹC�����G����r
    public GameObject enemy;              // �ĤHPrefab
    public GameObject nullEnemy;          // �ĤH��m���Ū���Prefab
    private bool playerIsAlive = true;    // ���a�O�_�٬���
    private bool hasGameEnded = false;    // �C���O�_�w�g����

    float StartX;
    float StartY;
    Vector3 ePos;

    void Start()
    {
        resultText.text = "";             // �M�ŵ��G��r
        StartX = -3f;
        StartY = 10f;
        genSquare(5, 5);                  // �ͦ��ĤH��}
    }

    void genSquare(int col, int row)
    {
        int limitX = Mathf.CeilToInt(col / 2);
        int limitY = Mathf.CeilToInt(row / 2);
        ePos.x = 0;

        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                ePos.y = StartY - i * 1.5f;
                ePos.x = -3 + (j * 1.5f);
                GameObject nEne = Instantiate(nullEnemy, ePos, Quaternion.identity);
                nEne.transform.name = "null" + i.ToString() + j.ToString();
                nEne.transform.tag = "enemyPos";
            }
        }
        showEnemies(col, row);
    }

    void showEnemies(int col, int row)
    {
        int middle = Mathf.CeilToInt(col / 2);
        string findPath = "";

        for (int i = 0; i < col; i++)
        {
            int catchLeft = (i <= middle) ? middle - i : i - middle;
            int catchRight = (i <= middle) ? middle + i : i + middle - 2 * (i - middle);

            catchLeft = Mathf.Max(catchLeft, 0);

            for (int j = catchLeft; j <= catchRight; j++)
            {
                findPath = "null" + i.ToString() + j.ToString();
                foreach (GameObject ene in GameObject.FindGameObjectsWithTag("enemyPos"))
                {
                    if (ene.name == findPath)
                    {
                        Instantiate(enemy, ene.transform.position, Quaternion.identity).name = "enemy" + i.ToString() + j.ToString();
                    }
                }
            }
        }
    }

    void Update()
    {
        if (hasGameEnded) return; // �קK�����˴��C�����G

        if (!GameObject.FindGameObjectWithTag("Player") && playerIsAlive)
        {
            playerIsAlive = false;
            hasGameEnded = true;
            playerLost();
            StopEnemyShooting();
        }

        if (GameObject.FindGameObjectWithTag("enemy") == null && playerIsAlive)
        {
            hasGameEnded = true;
            playerWon();
        }
    }

    public void playerLost()
    {
        resultText.text = "�A��F!";
    }

    public void playerWon()
    {
        resultText.text = "�AĹ�F!";
        StopEnemyShooting(); // ����ĤH�g��
    }

    void StopEnemyShooting()
    {
        enemyMove[] enemies = FindObjectsOfType<enemyMove>();
        if (enemies != null)
        {
            foreach (enemyMove enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.CancelInvoke("shoot");
                }
            }
        }
    }
}