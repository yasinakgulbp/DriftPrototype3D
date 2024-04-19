using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    // Raycast i�leminin yap�laca�� mesafe
    public float raycastDistance = 100f;

    void Update()
    {
        // Fare t�klamas�n� kontrol et
        if (Input.GetMouseButtonDown(0))
        {
            // Fare pozisyonunu al
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Raycast'i yolla ve �arp��ma kontrol� yap
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                // �arp��ma noktas�ndaki objenin scriptini al
                ObjectMovement objectMovement = hit.collider.GetComponent<ObjectMovement>();

                // Script var m� kontrol et
                if (objectMovement != null)
                {
                    // Objenin hareket etmesini iste
                    objectMovement.StartMovement();
                }
            }
        }
    }
}


































//using System.Collections;
//using UnityEngine;

//public class RaycastManager : MonoBehaviour
//{
//    // Raycast i�leminin yap�laca�� mesafe
//    public float raycastDistance = 100f;
//    // Hareket h�z�
//    public float moveSpeed = 20f;

//    void Update()
//    {
//        // Fare t�klamas�n� kontrol et
//        if (Input.GetMouseButtonDown(0))
//        {
//            // Fare pozisyonunu al
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hit;

//            // Raycast'i yolla ve �arp��ma kontrol� yap
//            if (Physics.Raycast(ray, out hit, raycastDistance))
//            {
//                // �arp��ma noktas�ndaki objeyi kontrol et
//                if (hit.collider != null)
//                {
//                    // Objenin tag'ini kontrol et
//                    if (hit.collider.CompareTag("LeftObject"))
//                    {
//                        // E�er t�klanan objenin tag'i "LeftObject" ise hareketi ba�lat
//                        StartCoroutine(MoveAndDestroy(hit.collider.gameObject, Vector3.left));
//                    }
//                    else if (hit.collider.CompareTag("RightObject"))
//                    {
//                        // E�er t�klanan objenin tag'i "RightObject" ise hareketi ba�lat
//                        StartCoroutine(MoveAndDestroy(hit.collider.gameObject, Vector3.right));
//                    }
//                    else if (hit.collider.CompareTag("FrontObject"))
//                    {
//                        // E�er t�klanan objenin tag'i "FrontObject" ise hareketi ba�lat
//                        StartCoroutine(MoveAndDestroy(hit.collider.gameObject, Vector3.forward));
//                    }
//                    else if (hit.collider.CompareTag("BackObject"))
//                    {
//                        // E�er t�klanan objenin tag'i "BackObject" ise hareketi ba�lat
//                        StartCoroutine(MoveAndDestroy(hit.collider.gameObject, Vector3.back));
//                    }
//                    // Di�er durumlar i�in ek kontroller buraya eklenebilir
//                }
//            }
//        }
//    }

//    IEnumerator MoveAndDestroy(GameObject obj, Vector3 direction)
//    {
//        // Hareket etme d�ng�s�
//        float timer = 0f;
//        while (timer < 1f)
//        {
//            // Hareket et
//            obj.transform.Translate(direction * moveSpeed * Time.deltaTime);
//            // Zaman� art�r
//            timer += Time.deltaTime;
//            yield return null;
//        }

//        // Objenin yok edilmesi
//        Destroy(obj);
//    }
//}
