using UnityEngine;

public class MoveAndReturn : MonoBehaviour
{
    // Hareket hýzý
    public float moveSpeed = 20f;
    // Orijinal konum
    private Vector3 originalPosition;
    // Hareket yönü
    private Vector3 direction;

    void Start()
    {
        // Baþlangýçta objenin orijinal konumunu sakla
        originalPosition = transform.position;
    }

    void Update()
    {
        // Hareket et
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    // Çarpýþma algýlama
    void OnCollisionEnter(Collision collision)
    {
        // Çarpýþan nesnenin etiketini kontrol et
        if (!collision.collider.CompareTag("Player"))
        {
            // Eðer çarpýþan nesne "Player" deðilse, týklanan objeyi orijinal konumuna geri döndür
            ReturnToOriginalPosition();
        }
    }

    // Orijinal konuma geri dönme iþlemi
    void ReturnToOriginalPosition()
    {
        // Hareket yönünü tersine çevirerek orijinal konuma doðru hareket ettir
        direction = (originalPosition - transform.position).normalized;
        // Hareket hýzýný belirle
        moveSpeed = 10f; // Örneðin 10 birim/s hýzla dönsün
    }

    // Týklanan objeyi belirli bir yönde hareket ettirmek için bu metodu çaðýrabiliriz
    public void Move(Vector3 moveDirection)
    {
        direction = moveDirection;
    }
}
