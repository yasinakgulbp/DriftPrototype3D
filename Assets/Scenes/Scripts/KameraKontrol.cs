using UnityEngine;

public class KameraKontrol : MonoBehaviour
{
    public Transform hedef; // Kameran�n d�nece�i hedef obje
    public float hiz = 3f; // Kameran�n d�nme h�z�

    void Update()
    {
        Debug.Log("Update fonksiyonu �a�r�ld�.");

        // Fare giri�ini kontrol et (fare i�in)
        float fareInput = Input.GetAxis("Mouse X");

        // Mobil dokunmatik giri�ini kontrol et (mobil i�in)
        float dokunmatikInput = Input.acceleration.x;

        Debug.Log("Fare Input: " + fareInput);
        Debug.Log("Dokunmatik Input: " + dokunmatikInput);

        float yatayInput = Mathf.Clamp(fareInput + dokunmatikInput, -1f, 1f);

        Debug.Log("Toplam Yatay Input: " + yatayInput);

        // Kameray� yatay eksende d�nd�r
        transform.RotateAround(hedef.position, Vector3.up, yatayInput * hiz * Time.deltaTime);

        // Kameran�n hedefe bakmas�n� sa�la
        transform.LookAt(hedef);
    }
}

