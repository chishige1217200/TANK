using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed_x = 0; //x軸方向移動速度 temporary value
    private float speed_z = 0; //z軸方向移動速度 temporary value
    private float speedIndex = 0.05f; //速度基準値 temporary value
    private float radian = 0f; //進行角度
    private int ricochet = 1; //跳弾可能回数
    private Rigidbody rb; //物理演算情報RigidBody

    private float force_x = 10f; //x方向の力 temporary value
    private float force_z = 10f; //y方向の力 temporary value

    private float forceIndex = 15f; //射出力基準値 temporary value

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Rigidbody情報の取得
        rb.AddForce(force_x, 0, force_z, ForceMode.VelocityChange); //瞬間的に弾に力を加える
        //Movetest2(30f); //Movetest2を角度0で実行
    }

    void FixedUpdate() //物理的な挙動はFixedUpdate関数を使用
    {
        Debug.Log(rb.velocity);
        //this.transform.position += new Vector3(speed_x, 0, speed_z); //移動処理
        //this.transform.rotation = Quaternion.Euler(0, radian, 0); //回転処理（進行方向）
    }

    void OnCollisionEnter(Collision collision) //物体の衝突を見る関数(非貫通)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "WeakWall") //壁と衝突した場合
        {
            if (ricochet == 0) //跳弾可能回数が0の場合，消滅
            {
                Destroy(this.gameObject);
                Debug.Log("Broke!");
            }
            else //それ以外の場合，跳弾可能回数を1減らす
            {
                ricochet--;
                Debug.Log("Hit!");
            }
        }
        if (collision.gameObject.tag == "Tank" || collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Mine" || collision.gameObject.tag == "Blast") //タンク・他の弾・地雷・爆風との接触
        {
            Destroy(this.gameObject); //消滅
        }
    }

    void Movetest() //始点と終点のx, z座標により制御
    {
        int x1 = 1; //temporary value
        int x2 = 2; //temporary value
        int z1 = 1; //temporary value
        int z2 = 2; //temporary value

        int dx = x2 - x1; //変化量x
        int dz = z2 - z1; //変化量z

        radian = (float)Math.Atan2(dz, dx); //tanの値計算
        radian = radian * (float)(180 / Math.PI); //角度(ラジアン)の計算

        speed_x = speedIndex * (float)Math.Cos(radian); //x軸方向速度
        speed_z = speedIndex * (float)Math.Sin(radian); //z軸方向速度
    }

    void Movetest2(float angle) //角度の値により制御
    {
        radian = angle; //角度をradianに代入
        angle = angle * (float)(Math.PI / 180); //ラジアンに変換
        speed_x = speedIndex * (float)Math.Sin(angle); //x軸方向速度
        speed_z = speedIndex * (float)Math.Cos(angle); //z軸方向速度
    }
}