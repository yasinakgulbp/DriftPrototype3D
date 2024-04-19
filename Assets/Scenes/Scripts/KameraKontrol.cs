using UnityEngine;

public class KameraKontrol : MonoBehaviour
{
    public Transform hedef; // Kameranýn döneceði hedef obje
    public float hiz = 3f; // Kameranýn dönme hýzý

    void Update()
    {
        Debug.Log("Update fonksiyonu çaðrýldý.");

        // Fare giriþini kontrol et (fare için)
        float fareInput = Input.GetAxis("Mouse X");

        // Mobil dokunmatik giriþini kontrol et (mobil için)
        float dokunmatikInput = Input.acceleration.x;

        Debug.Log("Fare Input: " + fareInput);
        Debug.Log("Dokunmatik Input: " + dokunmatikInput);

        float yatayInput = Mathf.Clamp(fareInput + dokunmatikInput, -1f, 1f);

        Debug.Log("Toplam Yatay Input: " + yatayInput);

        // Kamerayý yatay eksende döndür
        transform.RotateAround(hedef.position, Vector3.up, yatayInput * hiz * Time.deltaTime);

        // Kameranýn hedefe bakmasýný saðla
        transform.LookAt(hedef);
    }
}

