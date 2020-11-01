using System;
using System.Threading.Tasks;
using UnityEngine;

public class Mine : MonoBehaviour {
    private Renderer rend;
    void Start () {
        //rend = GameObject.Find ("Big Explosion").GetComponent<Renderer> ();
        Gobomb ();
    }

    void Update () {

    }

    async void Gobomb () {
        Prediction ();
        await Task.Delay (5000);
        Explosion ();
    }

    void Prediction () {
        Debug.Log ("Predict!");

    }

    void Explosion () {
        Debug.Log ("Bomb!");
    }

}