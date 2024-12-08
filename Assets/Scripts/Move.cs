using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    public Camera mainCamera;      // 指定主攝影機
    public GameObject bulletPrefab; // 子彈的預製件
    public float moveSpeed = 5f;    // 移動速度
    public float fireInterval = 0.2f; // 自動發射間隔
    private bool isFiring = false;  // 控制自動發射

    void Start()
    {
        InvokeRepeating("AutoFire", 0f, fireInterval);
    }

    void Update()
    {
        // 檢查滑鼠左鍵是否被按下
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // 從滑鼠位置發射Ray
            RaycastHit hit;

            // 檢查Ray是否擊中物體
            if (Physics.Raycast(ray, out hit))
            {
                // 設定目標位置為擊中的位置
                Vector3 targetPosition = hit.point;
                targetPosition.z = 0; // 將 z 軸設定為 0s
                this.transform.position = targetPosition;
            }
        }
    }

    

    // 自動發射子彈的協程
    private void AutoFire()
    {
        // 創建子彈並發射
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // 設定子彈方向和速度
        bulletRb.velocity = transform.forward * moveSpeed;
    }

}