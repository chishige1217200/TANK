using System;
using System.Threading.Tasks;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    private GameObject[] bullet; //弾オブジェクト情報の格納
    public GameObject prefab; //弾プレハブオブジェクトの格納
    private Bullet bulletScript;
    public int limit; //発射可能弾数
    async void Start ()
    {
        bullet = new GameObject[limit]; //指定個数分のインスタンスを生成
        for(int i = 0; i < 100; i++)
        {
            await Task.Delay(500);
            MakeBullet();
        }

    }

    void MakeBullet ()
    {
        for (int i = 0; i < limit; i++)
        {
            if (bullet[i] == null)
            {
                bullet[i] = Instantiate (prefab, new Vector3 (0, 0, 0), new Quaternion(0, 0, 0, 0));
                bulletScript = bullet[i].GetComponent<Bullet>();
                bulletScript.Shot(30f, 30f, 2);
                Debug.Log("bullet " + i + " instantiated succesfully!");
                break;
            }
        }
    }
}