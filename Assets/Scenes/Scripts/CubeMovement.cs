using System.Collections;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float speed = 1f; // H�z� ayarlamak i�in bir de�i�ken

    private bool isMoving = false; // Nesne hareket halinde mi?

    void Update()
    {
        // E�er kullan�c� sol mause tu�una t�klarsa ve nesne hen�z hareket etmiyorsa...
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            // Nesne hareket halinde
            isMoving = true;

            // Hareket etme i�lemini ba�lat
            StartCoroutine(MoveAndDestroy());
        }
    }

    IEnumerator MoveAndDestroy()
    {
        // Ba�lang�� pozisyonunu al
        Vector3 startPosition = transform.position;

        // Hedef pozisyonunu belirle
        Vector3 targetPosition = startPosition + Vector3.right * speed;

        // Hareket s�resi
        float moveDuration = 1f;

        // Ba�lang�� zaman�
        float startTime = Time.time;

        // Hareket etme d�ng�s�
        while (Time.time - startTime < moveDuration)
        {
            // Hareket et
            float t = (Time.time - startTime) / moveDuration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null; // Bir sonraki frame'e ge�
        }

        // Hareket bitti�inde nesneyi yok et
        Destroy(gameObject);
    }
}