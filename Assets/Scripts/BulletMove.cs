using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    float 速度 = 10;
    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 速度);

        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.up*5*Time.deltaTime);
    }
}
