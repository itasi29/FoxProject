using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class JumpTest : MonoBehaviour
{
    private Rigidbody rb; // Rigidbodyを使うための変数
    private bool Grounded; // 地面に着地しているか判定する変数
    public float Jumppower; // ジャンプ力

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        print(Grounded);

        if (Input.GetKeyDown(KeyCode.Space))//  もし、スペースキーがおされたなら、  
        {
            if (Grounded == true)//  もし、Groundedがtrueなら、
            {
                Grounded = false;
                rb.AddForce(Vector3.up * Jumppower);//  上にJumpPower分力をかける
            }
        }
    }

    void OnCollisionEnter(Collision other)//  地面に触れた時の処理
    {
        if (other.gameObject.tag == "Stage")//  もしGroundというタグがついたオブジェクトに触れたら、
        {
            Grounded = true;//  Groundedをtrueにする
        }
    }
}