using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // ฟังก์ชันสำหรับปุ่ม Start Game
    public void StartGame()
    {
        // โหลดซีนที่ชื่อว่า Gameplay (อย่าลืมตั้งชื่อซีนให้ตรงกัน)
        SceneManager.LoadScene("Gameplay");
    }

    // ฟังก์ชันสำหรับโหลดหน้า Credit โดยเฉพาะ
    public void LoadCredit()
    {
        SceneManager.LoadScene("CreditScene");
    }

    // ฟังก์ชันสำหรับกลับหน้า Main Menu
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // ฟังก์ชันสำหรับปุ่มออกจากเกม (ใน WebGL อาจจะไม่เห็นผลชัดเจน แต่ใส่ไว้ก่อนได้)
    public void QuitGame()
    {
        Application.Quit();
    }
}