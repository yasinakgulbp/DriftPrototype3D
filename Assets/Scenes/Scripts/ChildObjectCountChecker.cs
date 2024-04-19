using UnityEngine;

public class ObjectCountChecker : MonoBehaviour
{
    public GameObject targetObject; // Hedef nesneyi bu alana ata
    public float checkInterval = 1f; // Kontrol aralýðý (saniye)

    private GameObject[] objectsInScene;

    void Start()
    {
        InvokeRepeating("CheckObjectCount", 0f, checkInterval);
    }

    void CheckObjectCount()
    {
        if (objectsInScene == null || objectsInScene.Length == 0)
        {
            objectsInScene = GameObject.FindGameObjectsWithTag(targetObject.tag);
        }
        else
        {
            if (objectsInScene.Length == 0)
            {
                Debug.Log("Tüm objeler yok edildi!");
            }
        }
    }
}
