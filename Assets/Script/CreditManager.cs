using UnityEngine;
using UnityEngine.SceneManagement; // ต้องมีบรรทัดนี้เพื่อสลับฉาก

public class CreditManager : MonoBehaviour
{
    // ฟังก์ชันนี้จะเอาไปผูกกับปุ่ม Play Again
    public void RestartGame()
    {
        // ใส่ชื่อฉากหลักของคุณลงไป (สมมติว่าชื่อ GamePlay)
        SceneManager.LoadScene("GamePlay");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("ออกจากเกม!");
    }
}