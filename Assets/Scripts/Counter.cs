using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Counter : MonoBehaviour
{
    private int failedMoves = 0;
    private int remainingObjects = 0;
    public int maxFailedMoves = 3; // Hata yapma limiti
    public float secondsPerCube = 1f; // Her küp için 1 saniye

    // TMPro text nesneleri için referanslar
    public TMP_Text failedMovesText;
    public TMP_Text remainingObjectsText;
    public TMP_Text timerText;

    private float currentTime;

    private void Start()
    {
        currentTime = GetInitialTime();
        // Baþlangýçta bir gecikme ekleyerek obje sayýsýný al
        Invoke("GetRemainingObjects", 0.2f);
    }

    private float GetInitialTime()
    {
        // MazeGenerator scriptine eriþimi al
        MazeGenerator mazeGenerator = Object.FindFirstObjectByType<MazeGenerator>();
        if (mazeGenerator != null)
        {
            // MazeGenerator'dan obje sayýsýný al
            remainingObjects = mazeGenerator.GetObjectCount();
            // Geri sayým süresini küp sayýsýna göre hesapla
            return remainingObjects * secondsPerCube;
        }
        else
        {
            Debug.LogError("MazeGenerator not found in the scene!");
            return 0f;
        }
    }

    private void GetRemainingObjects()
    {
        // MazeGenerator scriptine eriþimi al
        MazeGenerator mazeGenerator = Object.FindFirstObjectByType<MazeGenerator>();
        if (mazeGenerator != null)
        {
            // MazeGenerator'dan obje sayýsýný al ve kalan obje sayýsýný güncelle
            remainingObjects = mazeGenerator.GetObjectCount();
            // Baþlangýçta metinleri güncelle
            UpdateTexts();
        }
        else
        {
            Debug.LogError("MazeGenerator not found in the scene!");
        }
    }

    public void IncrementSuccessfulMoves()
    {
        // Kalan obje sayýsýný azalt
        remainingObjects--;
        // Baþarýlý hareket sayýsýný güncelle ve metni yenile
        UpdateTexts();

        // Oyun bitti mi kontrol et
        CheckGameEnd();
    }

    public void IncrementFailedMoves()
    {
        failedMoves++;
        // Baþarýsýz hareket sayýsýný güncelle ve metni yenile
        UpdateTexts();

        // Hata yapma limitini kontrol et
        CheckFailureLimit();
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        timerText.text = currentTime.ToString("F0");

        if (currentTime <= 0)
        {
            // Süre bittiðinde oyunu yeniden baþlat
            RestartScene();
        }

        // Geri sayýmýn son 5 saniyesinde uyarý mesajý göster
        if (currentTime <= 5f)
        {
            timerText.color = Color.red;
        }
    }

    // TextMesh Pro nesnelerini güncelleyen yardýmcý fonksiyon
    private void UpdateTexts()
    {
        if (failedMovesText != null)
        {
            failedMovesText.text = (maxFailedMoves - failedMoves).ToString(); // Kalan hak sayýsýný göster
        }

        if (remainingObjectsText != null)
        {
            remainingObjectsText.text = remainingObjects.ToString();
        }
    }

    // Oyunun bitip bitmediðini kontrol eden fonksiyon
    private void CheckGameEnd()
    {
        if (remainingObjects <= 0)
        {
            Debug.Log("Zafer!"); // Zafer durumunu konsola yazdýr
            // Bir sonraki seviyeye geç
            GoToNextLevel();
        }
    }

    // Hata yapma limitini kontrol eden fonksiyon
    private void CheckFailureLimit()
    {
        if (maxFailedMoves - failedMoves == 0) // Kalan hak sýfýra eþit ise
        {
            Debug.Log("Maðlubiyet!"); // Maðlubiyet durumunu konsola yazdýr
                                      // Sahneyi yeniden baþlat
            RestartScene();
        }
    }

    // Sahneyi yeniden baþlatan fonksiyon
    private void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    // Bir sonraki seviyeye geçen fonksiyon
    private void GoToNextLevel()
    {
        // Aktif sahnenin bir sonraki build index'ini al
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Eðer bir sonraki sahne mevcutsa, o seviyeye geç
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Sonraki geçilmek için gönderilen level : " + nextSceneIndex);
            GameManager.Instance.SetCurrentLevel(nextSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("There is no next level available!");
            RestartScene();
        }
    }
}
