using System;
using UnityEngine;

public class CameraAutoPosition : MonoBehaviour
{
    void Start()
    {
        // Sahnedeki tüm objeleri al
        GameObject[] objects = FindObjectsByType<GameObject>();

        // Sahnedeki tüm objeleri kapsayan bir sýnýrlayýcý oluþtur
        Bounds bounds = new Bounds(objects[0].transform.position, Vector3.zero);
        foreach (GameObject obj in objects)
        {
            bounds.Encapsulate(obj.transform.position);
        }

        // Kamerayý sýnýrlayýcýyý merkez alacak þekilde konumlandýr
        transform.position = bounds.center + new Vector3(0, 0, -10); // Eðer kamera 2D ise Z ekseninde -10 kullanýlabilir.
    }

    private T[] FindObjectsByType<T>()
    {
        throw new NotImplementedException();
    }
}
