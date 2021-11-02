using UnityEngine;
using System.Collections;

public class bl_InstantiateHud : MonoBehaviour {

    public GameObject[] Hud;
    public Transform Point;


    public void Instantiate()
    {
        Vector3 v;
        float i = Random.Range(-300, 300);
        float e = Random.Range(1, 300);
        float b = Random.Range(-300, 300);
        v = new Vector3(i, e, b);

        int g = Random.Range(0, Hud.Length);
        Object.Instantiate(Hud[g], Point.position + v, Quaternion.identity);
    }
}