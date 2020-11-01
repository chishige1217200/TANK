using System;
using UnityEngine;

public class Tank : MonoBehaviour
{
    private Transform bodyTransform; //BodyオブジェクトのTransformコンポーネント情報
    private Transform batteryTransform; //BatteryオブジェクトのTransformコンポーネント情報

    void Start()
    {
        bodyTransform = GameObject.Find("Body").GetComponent<Transform>(); //BodyオブジェクトのTransformコンポーネント情報を取得
        batteryTransform = GameObject.Find("Battery").GetComponent<Transform>(); //BatteryオブジェクトのTransformコンポーネント情報を取得
    }

    void Update()
    {
        this.transform.position += new Vector3(-0.01f, 0, 0); //このスクリプトがアタッチされているオブジェクトのTransform.positionをx軸方向に-0.01ずつ移動する
    }
}