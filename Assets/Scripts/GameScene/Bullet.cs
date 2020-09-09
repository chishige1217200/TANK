using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed_x = 0; //x軸方向移動速度
    private float speed_z = 0; //z軸方向移動速度
    private float speedIndex = 0.05f; //速度基準値
    private float radian = 0f; //進行角度
    private int ricochet = 1; //跳弾回数

    async void Start()
    {
        Movetest2(30f);
    }

    void Update()
    {
        this.transform.position += new Vector3(speed_x, 0, speed_z); //移動処理
        this.transform.rotation = Quaternion.Euler(0, radian, 0); //回転処理（進行方向）
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
        speed_x = speedIndex * (float)Math.Cos(angle); //x軸方向速度
        speed_z = speedIndex * (float)Math.Sin(angle); //z軸方向速度
    }
}