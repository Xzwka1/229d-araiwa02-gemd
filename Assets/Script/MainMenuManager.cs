using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // ฟังก์ชันนี้จะผูกกับปุ่ม Play Game
    public void StartGame()
    {
        // 🛑 ใส่ชื่อฉากด่านแรกของคุณให้เป๊ะๆ (เช่น "GamePlay")
        SceneManager.LoadScene("GamePlay");
    }

    // ฟังก์ชันนี้จะผูกกับปุ่ม Exit
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("ปิดเกมแล้วจ้า!");
    }
}