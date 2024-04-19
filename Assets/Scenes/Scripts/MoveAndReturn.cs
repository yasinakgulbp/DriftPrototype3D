using UnityEngine;

public class MoveAndReturn : MonoBehaviour
{
    // Hareket h�z�
    public float moveSpeed = 20f;
    // Orijinal konum
    private Vector3 originalPosition;
    // Hareket y�n�
    private Vector3 direction;

    void Start()
    {
        // Ba�lang��ta objenin orijinal konumunu sakla
        originalPosition = transform.position;
    }

    void Update()
    {
        // Hareket et
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    // �arp��ma alg�lama
    void OnCollisionEnter(Collision collision)
    {
        // �arp��an nesnenin etiketini kontrol et
        if (!collision.collider.CompareTag("Player"))
        {
            // E�er �arp��an nesne "Player" de�ilse, t�klanan objeyi orijinal konumuna geri d�nd�r
            ReturnToOriginalPosition();
        }
    }

    // Orijinal konuma geri d�nme i�lemi
    void ReturnToOriginalPosition()
    {
        // Hareket y�n�n� tersine �evirerek orijinal konuma do�ru hareket ettir
        direction = (originalPosition - transform.position).normalized;
        // Hareket h�z�n� belirle
        moveSpeed = 10f; // �rne�in 10 birim/s h�zla d�ns�n
    }

    // T�klanan objeyi belirli bir y�nde hareket ettirmek i�in bu metodu �a��rabiliriz
    public void Move(Vector3 moveDirection)
    {
        direction = moveDirection;
    }
}
