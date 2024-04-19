using System.Collections;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float speed = 1f; // Hýzý ayarlamak için bir deðiþken

    private bool isMoving = false; // Nesne hareket halinde mi?

    void Update()
    {
        // Eðer kullanýcý sol mause tuþuna týklarsa ve nesne henüz hareket etmiyorsa...
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            // Nesne hareket halinde
            isMoving = true;

            // Hareket etme iþlemini baþlat
            StartCoroutine(MoveAndDestroy());
        }
    }

    IEnumerator MoveAndDestroy()
    {
        // Baþlangýç pozisyonunu al
        Vector3 startPosition = transform.position;

        // Hedef pozisyonunu belirle
        Vector3 targetPosition = startPosition + Vector3.right * speed;

        // Hareket süresi
        float moveDuration = 1f;

        // Baþlangýç zamaný
        float startTime = Time.time;

        // Hareket etme döngüsü
        while (Time.time - startTime < moveDuration)
        {
            // Hareket et
            float t = (Time.time - startTime) / moveDuration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null; // Bir sonraki frame'e geç
        }

        // Hareket bittiðinde nesneyi yok et
        Destroy(gameObject);
    }
}