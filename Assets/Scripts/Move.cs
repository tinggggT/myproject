using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    public Camera mainCamera;      // ���w�D��v��
    public GameObject bulletPrefab; // �l�u���w�s��
    public float moveSpeed = 5f;    // ���ʳt��
    public float fireInterval = 0.2f; // �۰ʵo�g���j
    private bool isFiring = false;  // ����۰ʵo�g

    void Start()
    {
        InvokeRepeating("AutoFire", 0f, fireInterval);
    }

    void Update()
    {
        // �ˬd�ƹ�����O�_�Q���U
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // �q�ƹ���m�o�gRay
            RaycastHit hit;

            // �ˬdRay�O�_��������
            if (Physics.Raycast(ray, out hit))
            {
                // �]�w�ؼЦ�m����������m
                Vector3 targetPosition = hit.point;
                targetPosition.z = 0; // �N z �b�]�w�� 0s
                this.transform.position = targetPosition;
            }
        }
    }

    

    // �۰ʵo�g�l�u����{
    private void AutoFire()
    {
        // �Ыؤl�u�õo�g
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // �]�w�l�u��V�M�t��
        bulletRb.velocity = transform.forward * moveSpeed;
    }

}