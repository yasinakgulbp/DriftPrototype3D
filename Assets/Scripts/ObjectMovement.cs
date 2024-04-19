using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ObjectMovement : MonoBehaviour
{
    // Hareket hýzý
    public float moveSpeed = 20f;
    public Vector3 moveDirection;

    // Sayaç referansý
    private Counter counter;

    public float duration;
    public float strenght;
    public int vibrato;
    public float randomness;

    public Material[] materials; // Malzeme dizisi

    public AudioSource positiveSound; // Olumlu ses efekti
    public AudioSource negativeSound; // Olumsuz ses efekti

    private void Start()
    {
        // Counter scriptine eriþimi al
        counter = FindFirstObjectByType<Counter>(); // Bu satýrý güncelle

        // Rastgele bir malzeme atama
        int randomIndex = Random.Range(0, materials.Length);
        GetComponent<Renderer>().material = materials[randomIndex];
    }

    public void StartMovement()
    {
        // 10 uzunluðunda bir raycast ýþýný oluþturuyoruz.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDirection, out hit, 10f))
        {
            // Olumsuz ses efekti oynat
            negativeSound.Play(); // Olumsuz sesi çal
            // Iþýn bir nesneye çarptýysa, nesnenin adýný konsola yazdýrýyoruz.
            Debug.Log("Raycast bir nesneye çarptý: " + hit.collider.gameObject.name);
            transform.DOShakePosition(duration, strenght, vibrato, randomness);
            counter.IncrementFailedMoves(); // Baþarýsýz hareketi say
        }
        else
        {
            // Olumlu ses efekti oynat
            positiveSound.Play(); // Olumlu sesi çal
            // Iþýn herhangi bir nesneye çarpmadýysa, "Önünde engel yok" yazdýrýyoruz.
            Debug.Log("Önünde engel yok");
            // Engel yoksa collider'ý kapat ve sonra hareket et
            GetComponent<Collider>().enabled = false;
            StartCoroutine(MoveAndDestroy(moveDirection));
            counter.IncrementSuccessfulMoves(); // Baþarýlý hareketi say
        }
    }

    IEnumerator MoveAndDestroy(Vector3 direction)
    {
        // Hareket etme döngüsü
        float timer = 0f;
        while (timer < 0.5f)
        {
            // Hareket et
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            // Zamaný artýr
            timer += Time.deltaTime;
            yield return null;
        }

        // Objenin yok edilmesi
        Destroy(gameObject);
    }
}
