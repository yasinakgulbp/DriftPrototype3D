using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ObjectMovement : MonoBehaviour
{
    // Hareket h�z�
    public float moveSpeed = 20f;
    public Vector3 moveDirection;

    // Saya� referans�
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
        // Counter scriptine eri�imi al
        counter = FindFirstObjectByType<Counter>(); // Bu sat�r� g�ncelle

        // Rastgele bir malzeme atama
        int randomIndex = Random.Range(0, materials.Length);
        GetComponent<Renderer>().material = materials[randomIndex];
    }

    public void StartMovement()
    {
        // 10 uzunlu�unda bir raycast ���n� olu�turuyoruz.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDirection, out hit, 10f))
        {
            // Olumsuz ses efekti oynat
            negativeSound.Play(); // Olumsuz sesi �al
            // I��n bir nesneye �arpt�ysa, nesnenin ad�n� konsola yazd�r�yoruz.
            Debug.Log("Raycast bir nesneye �arpt�: " + hit.collider.gameObject.name);
            transform.DOShakePosition(duration, strenght, vibrato, randomness);
            counter.IncrementFailedMoves(); // Ba�ar�s�z hareketi say
        }
        else
        {
            // Olumlu ses efekti oynat
            positiveSound.Play(); // Olumlu sesi �al
            // I��n herhangi bir nesneye �arpmad�ysa, "�n�nde engel yok" yazd�r�yoruz.
            Debug.Log("�n�nde engel yok");
            // Engel yoksa collider'� kapat ve sonra hareket et
            GetComponent<Collider>().enabled = false;
            StartCoroutine(MoveAndDestroy(moveDirection));
            counter.IncrementSuccessfulMoves(); // Ba�ar�l� hareketi say
        }
    }

    IEnumerator MoveAndDestroy(Vector3 direction)
    {
        // Hareket etme d�ng�s�
        float timer = 0f;
        while (timer < 0.5f)
        {
            // Hareket et
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            // Zaman� art�r
            timer += Time.deltaTime;
            yield return null;
        }

        // Objenin yok edilmesi
        Destroy(gameObject);
    }
}
