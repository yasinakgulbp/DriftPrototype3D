using UnityEngine;

public class ChildObjectCountChecker : MonoBehaviour
{
    public Transform targetParent; // Hedef nesnenin transformunu bu alana ata
    public float checkInterval = 1f; // Kontrol aral��� (saniye)

    private void Start()
    {
        InvokeRepeating("CheckChildObjectCount", 0f, checkInterval);
    }

    private void CheckChildObjectCount()
    {
        if (targetParent.childCount == 0)
        {
            Debug.Log("T�m objeler yok edildi!");
            // ��lemi durdurmak istiyorsan�z a�a��daki sat�r� kullanabilirsiniz:
            // CancelInvoke("CheckChildObjectCount");
        }
    }
}