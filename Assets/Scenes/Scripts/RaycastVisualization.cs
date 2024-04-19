using UnityEngine;

public class RaycastVisualization : MonoBehaviour
{
    // Raycast ���nlar�n�n g�r�nme s�resi
    public float rayDuration = 1.0f;

    void Update()
    {
        // Fare t�klamas�n� kontrol et
        if (Input.GetMouseButtonDown(0))
        {
            // Fare pozisyonunu al
            Vector3 mousePosition = Input.mousePosition;
            // Fare pozisyonunu d�nya koordinatlar�na d�n��t�r
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            // Raycast'i yolla ve �arp��ma kontrol� yap
            if (Physics.Raycast(ray, out hit))
            {
                // T�klanan noktada d�rt farkl� y�ne do�ru raycast ���nlar� olu�tur
                CreateRaycasts(hit.point);
            }
        }
    }

    // T�klanan noktada d�rt farkl� y�ne do�ru raycast ���nlar� olu�turan fonksiyon
    void CreateRaycasts(Vector3 point)
    {
        // Yukar�
        Debug.DrawRay(point, Vector3.up * 10f, Color.red, rayDuration);
        // A�a��
        Debug.DrawRay(point, Vector3.down * 10f, Color.green, rayDuration);
        // Sa�a
        Debug.DrawRay(point, Vector3.right * 10f, Color.blue, rayDuration);
        // Sola
        Debug.DrawRay(point, Vector3.left * 10f, Color.yellow, rayDuration);
    }
}
