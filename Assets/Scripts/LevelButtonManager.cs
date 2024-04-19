using UnityEngine;
using UnityEngine.UI;

public class LevelButtonManager : MonoBehaviour
{
    public Button[] levelButtons; // Dizideki her d��me, bir seviye butonunu temsil eder.
    public int lastPlayedLevel = 0;

    void Start()
    {
        // Oyuncunun son oynad��� seviyeyi al
        lastPlayedLevel = GameManager.Instance.GetCurrentLevel();

        // Seviye butonlar�n� d�ng�ye alarak durumlar�n� ayarla
        for (int i = 0; i < levelButtons.Length; i++)
        {
            // E�er butonun indeksi son oynad��� seviyeden k���k veya e�itse, butonu etkinle�tir
            if (i + 1 <= lastPlayedLevel)
            {
                levelButtons[i].interactable = true;
            }
            // Sonraki seviyeleri kilitli hale getir
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }
}
