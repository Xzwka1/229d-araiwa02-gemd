using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // สำหรับใช้งาน UI พื้นฐาน

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // ทำเป็น Singleton เพื่อให้สคริปต์อื่นเรียกใช้ง่ายๆ

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    [Header("Game Settings")]
    public float timeLimit = 60f; // ดูให้แน่ใจว่าบรรทัดนี้เป็น = 60f; ไม่ใช่ [60]
    private int currentScore = 0;
    private bool isGameOver = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (isGameOver) return;

        // ระบบนับเวลาถอยหลัง
        timeLimit -= Time.deltaTime;

        // อัปเดต UI หน้าจอ
        timeText.text = "Time: " + Mathf.RoundToInt(timeLimit).ToString();
        scoreText.text = "Score: " + currentScore.ToString();

        // เช็คว่าหมดเวลาหรือยัง
        if (timeLimit <= 0)
        {
            GameOver();
        }
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
    }

    public void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over! หมดเวลา");
        // สั่งให้เริ่มด่านใหม่ หรือไปหน้า Game Over ได้ตรงนี้
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameWin()
    {
        isGameOver = true;
        Debug.Log("You Win!");
        // โหลดหน้า Credit เมื่อเข้าเส้นชัย
        SceneManager.LoadScene("CreditScene");
    }
}