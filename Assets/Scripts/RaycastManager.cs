using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    // Raycast iþleminin yapýlacaðý mesafe
    public float raycastDistance = 100f;

    void Update()
    {
        // Fare týklamasýný kontrol et
        if (Input.GetMouseButtonDown(0))
        {
            // Fare pozisyonunu al
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Raycast'i yolla ve çarpýþma kontrolü yap
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                // Çarpýþma noktasýndaki objenin scriptini al
                ObjectMovement objectMovement = hit.collider.GetComponent<ObjectMovement>();

                // Script var mý kontrol et
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
//    // Raycast iþleminin yapýlacaðý mesafe
//    public float raycastDistance = 100f;
//    // Hareket hýzý
//    public float moveSpeed = 20f;

//    void Update()
//    {
//        // Fare týklamasýný kontrol et
//        if (Input.GetMouseButtonDown(0))
//        {
//            // Fare pozisyonunu al
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hit;

//            // Raycast'i yolla ve çarpýþma kontrolü yap
//            if (Physics.Raycast(ray, out hit, raycastDistance))
//            {
//                // Çarpýþma noktasýndaki objeyi kontrol et
//                if (hit.collider != null)
//                {
//                    // Objenin tag'ini kontrol et
//                    if (hit.collider.CompareTag("LeftObject"))
//                    {
//                        // Eðer týklanan objenin tag'i "LeftObject" ise hareketi baþlat
//                        StartCoroutine(MoveAndDestroy(hit.collider.gameObject, Vector3.left));
//                    }
//                    else if (hit.collider.CompareTag("RightObject"))
//                    {
//                        // Eðer týklanan objenin tag'i "RightObject" ise hareketi baþlat
//                        StartCoroutine(MoveAndDestroy(hit.collider.gameObject, Vector3.right));
//                    }
//                    else if (hit.collider.CompareTag("FrontObject"))
//                    {
//                        // Eðer týklanan objenin tag'i "FrontObject" ise hareketi baþlat
//                        StartCoroutine(MoveAndDestroy(hit.collider.gameObject, Vector3.forward));
//                    }
//                    else if (hit.collider.CompareTag("BackObject"))
//                    {
//                        // Eðer týklanan objenin tag'i "BackObject" ise hareketi baþlat
//                        StartCoroutine(MoveAndDestroy(hit.collider.gameObject, Vector3.back));
//                    }
//                    // Diðer durumlar için ek kontroller buraya eklenebilir
//                }
//            }
//        }
//    }

//    IEnumerator MoveAndDestroy(GameObject obj, Vector3 direction)
//    {
//        // Hareket etme döngüsü
//        float timer = 0f;
//        while (timer < 1f)
//        {
//            // Hareket et
//            obj.transform.Translate(direction * moveSpeed * Time.deltaTime);
//            // Zamaný artýr
//            timer += Time.deltaTime;
//            yield return null;
//        }

//        // Objenin yok edilmesi
//        Destroy(obj);
//    }
//}
