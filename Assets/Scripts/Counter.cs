using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Counter : MonoBehaviour
{
    private int failedMoves = 0;
    private int remainingObjects = 0;
    public int maxFailedMoves = 3; // Hata yapma limiti
    public float secondsPerCube = 1f; // Her k�p i�in 1 saniye

    // TMPro text nesneleri i�in referanslar
    public TMP_Text failedMovesText;
    public TMP_Text remainingObjectsText;
    public TMP_Text timerText;

    private float currentTime;

    private void Start()
    {
        currentTime = GetInitialTime();
        // Ba�lang��ta bir gecikme ekleyerek obje say�s�n� al
        Invoke("GetRemainingObjects", 0.2f);
    }

    private float GetInitialTime()
    {
        // MazeGenerator scriptine eri�imi al
        MazeGenerator mazeGenerator = Object.FindFirstObjectByType<MazeGenerator>();
        if (mazeGenerator != null)
        {
            // MazeGenerator'dan obje say�s�n� al
            remainingObjects = mazeGenerator.GetObjectCount();
            // Geri say�m s�resini k�p say�s�na g�re hesapla
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
        // MazeGenerator scriptine eri�imi al
        MazeGenerator mazeGenerator = Object.FindFirstObjectByType<MazeGenerator>();
        if (mazeGenerator != null)
        {
            // MazeGenerator'dan obje say�s�n� al ve kalan obje say�s�n� g�ncelle
            remainingObjects = mazeGenerator.GetObjectCount();
            // Ba�lang��ta metinleri g�ncelle
            UpdateTexts();
        }
        else
        {
            Debug.LogError("MazeGenerator not found in the scene!");
        }
    }

    public void IncrementSuccessfulMoves()
    {
        // Kalan obje say�s�n� azalt
        remainingObjects--;
        // Ba�ar�l� hareket say�s�n� g�ncelle ve metni yenile
        UpdateTexts();

        // Oyun bitti mi kontrol et
        CheckGameEnd();
    }

    public void IncrementFailedMoves()
    {
        failedMoves++;
        // Ba�ar�s�z hareket say�s�n� g�ncelle ve metni yenile
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
            // S�re bitti�inde oyunu yeniden ba�lat
            RestartScene();
        }

        // Geri say�m�n son 5 saniyesinde uyar� mesaj� g�ster
        if (currentTime <= 5f)
        {
            timerText.color = Color.red;
        }
    }

    // TextMesh Pro nesnelerini g�ncelleyen yard�mc� fonksiyon
    private void UpdateTexts()
    {
        if (failedMovesText != null)
        {
            failedMovesText.text = (maxFailedMoves - failedMoves).ToString(); // Kalan hak say�s�n� g�ster
        }

        if (remainingObjectsText != null)
        {
            remainingObjectsText.text = remainingObjects.ToString();
        }
    }

    // Oyunun bitip bitmedi�ini kontrol eden fonksiyon
    private void CheckGameEnd()
    {
        if (remainingObjects <= 0)
        {
            Debug.Log("Zafer!"); // Zafer durumunu konsola yazd�r
            // Bir sonraki seviyeye ge�
            GoToNextLevel();
        }
    }

    // Hata yapma limitini kontrol eden fonksiyon
    private void CheckFailureLimit()
    {
        if (maxFailedMoves - failedMoves == 0) // Kalan hak s�f�ra e�it ise
        {
            Debug.Log("Ma�lubiyet!"); // Ma�lubiyet durumunu konsola yazd�r
                                      // Sahneyi yeniden ba�lat
            RestartScene();
        }
    }

    // Sahneyi yeniden ba�latan fonksiyon
    private void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    // Bir sonraki seviyeye ge�en fonksiyon
    private void GoToNextLevel()
    {
        // Aktif sahnenin bir sonraki build index'ini al
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // E�er bir sonraki sahne mevcutsa, o seviyeye ge�
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Sonraki ge�ilmek i�in g�nderilen level : " + nextSceneIndex);
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
