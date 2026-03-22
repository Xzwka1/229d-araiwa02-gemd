using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // อย่าลืมบรรทัดนี้ เพื่อให้รู้จัก TextMeshPro

public class CreditManager : MonoBehaviour
{
    [Header("UI โชว์คะแนนตอนจบ")]
    public TextMeshProUGUI finalScoreText; // ช่องสำหรับใส่ตัวหนังสือคะแนน

    void Start()
    {
        // พอเริ่มฉากนี้ปุ๊บ ให้ไปดึงคะแนนจากกล่อง "FinalScore" มา (ถ้าไม่มีให้เป็น 0)
        int score = PlayerPrefs.GetInt("FinalScore", 0);

        // เปลี่ยนข้อความบนจอให้โชว์คะแนน
        if (finalScoreText != null)
        {
            finalScoreText.text = "FINAL SCORE: " + score.ToString();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GamePlay"); // กลับไปหน้าเล่นเกม (อย่าลืมเช็คชื่อด่านให้ตรงนะครับ)
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("ออกจากเกม!");
    }
}