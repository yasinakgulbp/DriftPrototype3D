using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
    // Hedeflenen kare hýzý
    public int targetFrameRate = 90;

    void Start()
    {
        // Hedef kare hýzýný ayarla
        Application.targetFrameRate = targetFrameRate;
    }
}
