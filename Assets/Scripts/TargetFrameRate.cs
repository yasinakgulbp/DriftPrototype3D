using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
    // Hedeflenen kare h�z�
    public int targetFrameRate = 90;

    void Start()
    {
        // Hedef kare h�z�n� ayarla
        Application.targetFrameRate = targetFrameRate;
    }
}
