using UnityEngine;

public class RaycastVisualization : MonoBehaviour
{
    // Raycast ýþýnlarýnýn görünme süresi
    public float rayDuration = 1.0f;

    void Update()
    {
        // Fare týklamasýný kontrol et
        if (Input.GetMouseButtonDown(0))
        {
            // Fare pozisyonunu al
            Vector3 mousePosition = Input.mousePosition;
            // Fare pozisyonunu dünya koordinatlarýna dönüþtür
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            // Raycast'i yolla ve çarpýþma kontrolü yap
            if (Physics.Raycast(ray, out hit))
            {
                // Týklanan noktada dört farklý yöne doðru raycast ýþýnlarý oluþtur
                CreateRaycasts(hit.point);
            }
        }
    }

    // Týklanan noktada dört farklý yöne doðru raycast ýþýnlarý oluþturan fonksiyon
    void CreateRaycasts(Vector3 point)
    {
        // Yukarý
        Debug.DrawRay(point, Vector3.up * 10f, Color.red, rayDuration);
        // Aþaðý
        Debug.DrawRay(point, Vector3.down * 10f, Color.green, rayDuration);
        // Saða
        Debug.DrawRay(point, Vector3.right * 10f, Color.blue, rayDuration);
        // Sola
        Debug.DrawRay(point, Vector3.left * 10f, Color.yellow, rayDuration);
    }
}
