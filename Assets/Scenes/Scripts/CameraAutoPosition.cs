using System;
using UnityEngine;

public class CameraAutoPosition : MonoBehaviour
{
    void Start()
    {
        // Sahnedeki t�m objeleri al
        GameObject[] objects = FindObjectsByType<GameObject>();

        // Sahnedeki t�m objeleri kapsayan bir s�n�rlay�c� olu�tur
        Bounds bounds = new Bounds(objects[0].transform.position, Vector3.zero);
        foreach (GameObject obj in objects)
        {
            bounds.Encapsulate(obj.transform.position);
        }

        // Kameray� s�n�rlay�c�y� merkez alacak �ekilde konumland�r
        transform.position = bounds.center + new Vector3(0, 0, -10); // E�er kamera 2D ise Z ekseninde -10 kullan�labilir.
    }

    private T[] FindObjectsByType<T>()
    {
        throw new NotImplementedException();
    }
}
