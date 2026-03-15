using UnityEngine;
using TMPro; // สำหรับใช้ TextMeshPro
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    [Header("Game Settings")]
    public float timeLimit = 60f;
    private int currentScore = 0;
    private bool isGameOver = false;

    void Awake()
    {
        // ตั้งค่าให้ Player เรียกใช้ GameManager ได้จากทุกที่
        if (instance == null) instance = this;
    }

    void Start()
    {
        // เริ่มเกมมาให้แสดงคะแนนเป็น 0 ทันที
        UpdateScoreUI();
    }

    void Update()
    {
        // ระบบนับเวลาถอยหลัง (ถ้าเกมยังไม่จบ)
        if (!isGameOver && timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
            UpdateTimeUI();

            if (timeLimit <= 0)
            {
                timeLimit = 0;
                GameOver();
            }
        }
    }

    // 💰 ฟังก์ชันรับคะแนน (Player จะส่งมาให้ตอนชนเหรียญ)
    public void AddScore(int scoreToAdd)
    {
        if (isGameOver) return;

        currentScore += scoreToAdd; // บวกคะแนนเพิ่ม
        UpdateScoreUI(); // สั่งอัปเดตตัวหนังสือบนหน้าจอ
    }

    // ฟังก์ชันเปลี่ยนข้อความ UI คะแนน
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }

    // ฟังก์ชันเปลี่ยนข้อความ UI เวลา
    private void UpdateTimeUI()
    {
        if (timeText != null)
        {
            // ปัดเศษทศนิยมทิ้ง จะได้เห็นเวลาเป็นตัวเลขกลมๆ
            timeText.text = "Time: " + Mathf.CeilToInt(timeLimit).ToString();
        }
    }

    public void GameWin()
    {
        isGameOver = true;
        Debug.Log("เข้าเส้นชัยแล้ว! เตรียมโหลดหน้า Credit...");

        // เอา // ออก เพื่อให้คำสั่งนี้ทำงาน
        SceneManager.LoadScene("CreditScene");
    }

    public void GameOver()
    {
        isGameOver = true;
        Debug.Log("หมดเวลา! เกมโอเวอร์!");
    }
}