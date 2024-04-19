using UnityEngine;

public class ChildObjectCountChecker : MonoBehaviour
{
    public Transform targetParent; // Hedef nesnenin transformunu bu alana ata
    public float checkInterval = 1f; // Kontrol aralýðý (saniye)

    private void Start()
    {
        InvokeRepeating("CheckChildObjectCount", 0f, checkInterval);
    }

    private void CheckChildObjectCount()
    {
        if (targetParent.childCount == 0)
        {
            Debug.Log("Tüm objeler yok edildi!");
            // Ýþlemi durdurmak istiyorsanýz aþaðýdaki satýrý kullanabilirsiniz:
            // CancelInvoke("CheckChildObjectCount");
        }
    }
}