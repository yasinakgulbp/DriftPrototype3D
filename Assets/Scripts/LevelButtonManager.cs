using UnityEngine;
using UnityEngine.UI;

public class LevelButtonManager : MonoBehaviour
{
    public Button[] levelButtons; // Dizideki her düðme, bir seviye butonunu temsil eder.
    public int lastPlayedLevel = 0;

    void Start()
    {
        // Oyuncunun son oynadýðý seviyeyi al
        lastPlayedLevel = GameManager.Instance.GetCurrentLevel();

        // Seviye butonlarýný döngüye alarak durumlarýný ayarla
        for (int i = 0; i < levelButtons.Length; i++)
        {
            // Eðer butonun indeksi son oynadýðý seviyeden küçük veya eþitse, butonu etkinleþtir
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
