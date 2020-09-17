using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int ricochet = 10; //跳弾可能回数 要初期化
    private Rigidbody rb; //物理演算情報RigidBody

    void Start()
    {
        float force_x; //x方向の力
        float force_z; //z方向の力
        float forceIndex = 15f; //射出力基準値 要初期化
        float init_radian = 30f; //初期進行角度 要初期化

        rb = GetComponent<Rigidbody>(); //Rigidbody情報の取得
        this.transform.rotation = Quaternion.Euler(0, -init_radian + 90f, 0); //回転処理（進行方向）
        init_radian = init_radian * (float)(Math.PI / 180); //ラジアン(pi)に変換
        force_x = forceIndex * (float)Math.Cos(init_radian); //x軸方向の力を計算
        force_z = forceIndex * (float)Math.Sin(init_radian); //z軸方向の力を計算
        rb.AddForce(force_x, 0, force_z, ForceMode.VelocityChange); //瞬間的に弾に力を加える(質量無視)
    }

    void OnCollisionEnter(Collision collision) //物体の衝突を見る関数(非貫通)
    {
        float speed_x; //x軸方向移動速度
        float speed_z; //z軸方向移動速度
        float radian; //反射後の角度を計算

        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "WeakWall") //壁と衝突した場合
        {
            if (ricochet == 0) //跳弾可能回数が0のとき
            {
                Destroy(this.gameObject); //消滅
                Debug.Log("Broke!");
                //発射タンク情報へのアクセス
            }
            else //それ以外のとき
            {
                ricochet--; //跳弾可能回数を1減らす
                Debug.Log("Hit!");
                speed_x = rb.velocity.x; //x軸方向速度の取得
                speed_z = rb.velocity.z; //z軸方向速度の取得
                radian = (float)Math.Atan2(speed_z, speed_x); //x-z平面のtanの値計算
                radian = radian * (float)(180 / Math.PI); //角度に変換
                this.transform.rotation = Quaternion.Euler(0, -radian + 90f, 0); //回転処理（進行方向）
            }
        }
        if (collision.gameObject.tag == "Tank" || collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Mine" || collision.gameObject.tag == "Blast") //タンク・他の弾・地雷・爆風との接触
        {
            Destroy(this.gameObject); //消滅
            //発射タンク情報へのアクセス
        }
    }
}