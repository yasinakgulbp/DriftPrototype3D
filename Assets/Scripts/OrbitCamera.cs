using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target; // D�nme merkezini belirleyen transform
    public float sensitivity = 10f; // Fare hassasiyeti
    public float distance = 10f; // Kameran�n hedeften uzakl���
    public float zoomSpeed = 2f; // Yak�nla�t�rma ve uzakla�t�rma h�z�
    public float minDistance = 2f; // Minimum uzakl�k
    public float maxDistance = 20f; // Maksimum uzakl�k

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
        // Fare hareketinin yatay ve dikey bile�enlerini al
        float mouseHorizontalInput = Input.GetAxis("Mouse X");
        float mouseVerticalInput = Input.GetAxis("Mouse Y");

        // Yatay ve dikey d�n�� a��lar�n� hesapla
        _rotationY += mouseHorizontalInput * sensitivity;
        _rotationX -= mouseVerticalInput * sensitivity;

        // Dikey d�n�� a��s�n� s�n�rla (-90 ile 90 derece aras�nda)
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

        // Kameray� yatay ve dikey eksende d�nd�r
        transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0f);

        // Yak�nla�t�rma ve uzakla�t�rmay� ger�ekle�tir
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

        // Kameray� zoomSpeed oran�nda yak�nla�t�r veya uzakla�t�r
        distance += pinchAmount * zoomSpeed * Time.deltaTime;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // Kameray� objenin etraf�nda konumland�r
        transform.position = target.position - transform.forward * distance;
    }
}

















//using UnityEngine;

//public class OrbitCamera : MonoBehaviour
//{
//    public Transform target; // D�nme merkezini belirleyen transform
//    public float sensitivity = 10f; // Fare hassasiyeti
//    public float distance = 10f; // Kameran�n hedeften uzakl���

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
//        // Fare hareketinin yatay ve dikey bile�enlerini al
//        float mouseHorizontalInput = Input.GetAxis("Mouse X");
//        float mouseVerticalInput = Input.GetAxis("Mouse Y");

//        // Yatay ve dikey d�n�� a��lar�n� hesapla
//        _rotationY += mouseHorizontalInput * sensitivity;
//        _rotationX -= mouseVerticalInput * sensitivity;

//        // Dikey d�n�� a��s�n� s�n�rla (-90 ile 90 derece aras�nda)
//        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

//        // Kameray� yatay ve dikey eksende d�nd�r
//        transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0f);

//        // Kameray� objenin etraf�nda konumland�r
//        transform.position = target.position - transform.forward * distance;
//    }
//}
