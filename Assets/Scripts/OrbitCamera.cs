using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target; // Dönme merkezini belirleyen transform
    public float sensitivity = 10f; // Fare hassasiyeti
    public float distance = 10f; // Kameranýn hedeften uzaklýðý
    public float zoomSpeed = 2f; // Yakýnlaþtýrma ve uzaklaþtýrma hýzý
    public float minDistance = 2f; // Minimum uzaklýk
    public float maxDistance = 20f; // Maksimum uzaklýk

    private float _rotationX;
    private float _rotationY;

    void Start()
    {
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        _rotationY = rotation.eulerAngles.y;
        _rotationX = rotation.eulerAngles.x;
    }

    void Update()
    {
        // Fare hareketinin yatay ve dikey bileþenlerini al
        float mouseHorizontalInput = Input.GetAxis("Mouse X");
        float mouseVerticalInput = Input.GetAxis("Mouse Y");

        // Yatay ve dikey dönüþ açýlarýný hesapla
        _rotationY += mouseHorizontalInput * sensitivity;
        _rotationX -= mouseVerticalInput * sensitivity;

        // Dikey dönüþ açýsýný sýnýrla (-90 ile 90 derece arasýnda)
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

        // Kamerayý yatay ve dikey eksende döndür
        transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0f);

        // Yakýnlaþtýrma ve uzaklaþtýrmayý gerçekleþtir
        float pinchAmount = 0;
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            pinchAmount = prevTouchDeltaMag - touchDeltaMag;
        }

        // Kamerayý zoomSpeed oranýnda yakýnlaþtýr veya uzaklaþtýr
        distance += pinchAmount * zoomSpeed * Time.deltaTime;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // Kamerayý objenin etrafýnda konumlandýr
        transform.position = target.position - transform.forward * distance;
    }
}

















//using UnityEngine;

//public class OrbitCamera : MonoBehaviour
//{
//    public Transform target; // Dönme merkezini belirleyen transform
//    public float sensitivity = 10f; // Fare hassasiyeti
//    public float distance = 10f; // Kameranýn hedeften uzaklýðý

//    private float _rotationX;
//    private float _rotationY;

//    void Start()
//    {
//        Vector3 relativePos = target.position - transform.position;
//        Quaternion rotation = Quaternion.LookRotation(relativePos);
//        _rotationY = rotation.eulerAngles.y;
//        _rotationX = rotation.eulerAngles.x;
//    }

//    void Update()
//    {
//        // Fare hareketinin yatay ve dikey bileþenlerini al
//        float mouseHorizontalInput = Input.GetAxis("Mouse X");
//        float mouseVerticalInput = Input.GetAxis("Mouse Y");

//        // Yatay ve dikey dönüþ açýlarýný hesapla
//        _rotationY += mouseHorizontalInput * sensitivity;
//        _rotationX -= mouseVerticalInput * sensitivity;

//        // Dikey dönüþ açýsýný sýnýrla (-90 ile 90 derece arasýnda)
//        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

//        // Kamerayý yatay ve dikey eksende döndür
//        transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0f);

//        // Kamerayý objenin etrafýnda konumlandýr
//        transform.position = target.position - transform.forward * distance;
//    }
//}
