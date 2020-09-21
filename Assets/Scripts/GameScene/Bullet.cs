using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int ricochet = 0; //跳弾可能回数 要初期化
    private Rigidbody rb; //物理演算情報RigidBody
    private AudioSource[] audioSource; //効果音情報の取得 0:Fire 1:Hit

    void Start()
    {
        float force_x; //x方向の力
        float force_z; //z方向の力
        float forceIndex = 30f; //射出力基準値 要初期化 低速15f 高速30f
        float init_radian = 30f; //初期進行角度 要初期化
        Renderer[] rend; //エフェクトレンダラー情報

        rend = new Renderer[5]; //配列の長さは5
        rb = GetComponent<Rigidbody>(); //Rigidbody情報の取得
        audioSource = GetComponents<AudioSource>();
        rend[0] = GameObject.Find("BPS").GetComponent<Renderer>(); //Particle System情報の取得 通常弾エフェクト
        rend[1] = GameObject.Find("BPS2").GetComponent<Renderer>(); //高速弾エフェクト
        rend[2] = GameObject.Find("BPS3").GetComponent<Renderer>(); //反射弾エフェクト
        rend[3] = GameObject.Find("Burst").GetComponent<Renderer>(); //
        rend[4] = GameObject.Find("RPS").GetComponent<Renderer>(); //反射時エフェクト

        this.transform.rotation = Quaternion.Euler(0, -init_radian + 90f, 0); //回転処理（進行方向）
        init_radian = init_radian * (float)(Math.PI / 180); //ラジアン(pi)に変換
        force_x = forceIndex * (float)Math.Cos(init_radian); //x軸方向の力を計算
        force_z = forceIndex * (float)Math.Sin(init_radian); //z軸方向の力を計算
        rb.AddForce(force_x, 0, force_z, ForceMode.VelocityChange); //瞬間的に弾に力を加える(質量無視)

        if (forceIndex > 20f) //速度が20以上の弾エフェクト
        {
            rend[1].enabled = true;
        }

        if (ricochet > 2) //跳弾数が2以上の弾
        {
            rend[2].enabled = true;
        }
        else //跳弾数が1以下の弾
        {
            rend[0].enabled = true;
        }
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
                //反射音
                Debug.Log("Wall!");
                speed_x = rb.velocity.x; //x軸方向速度の取得
                speed_z = rb.velocity.z; //z軸方向速度の取得
                radian = (float)Math.Atan2(speed_z, speed_x); //x-z平面のtanの値計算
                radian = radian * (float)(180 / Math.PI); //角度に変換
                this.transform.rotation = Quaternion.Euler(0, -radian + 90f, 0); //回転処理（進行方向）
            }
        }
        if (collision.gameObject.tag == "Tank") //タンクと衝突した場合
        {
            Debug.Log("Hit!");
            //SoundEffect(1); //外部スクリプトによる命令に変更(消滅すると鳴らせない)
            Destroy(this.gameObject); //消滅
            //発射タンク情報へのアクセス
        }
        if (collision.gameObject.tag == "Bullet") //弾同士で衝突した場合
        {
            Debug.Log("Bullet!");
            //衝突音？
            Destroy(this.gameObject);
            //発射タンク情報へのアクセス
        }
        if (collision.gameObject.tag == "Mine" || collision.gameObject.tag == "Blast") //タンク・他の弾・地雷・爆風との接触
        {
            Destroy(this.gameObject); //消滅
            //発射タンク情報へのアクセス
        }
    }
    void SoundEffect(int num)
    {
        audioSource[num].PlayOneShot(audioSource[num].clip);
        Debug.Log ("SoundEffect Played.");
    }
}
//Destroyは専用関数用意？