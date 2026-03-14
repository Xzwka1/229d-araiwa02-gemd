using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Physics Settings")]
    public float acceleration = 10f; // ตัวแปรความเร่ง (a)

    void Start()
    {
        // ดึงคอมโพเนนต์ Rigidbody ที่ติดอยู่กับตัวผู้เล่นมาใช้งาน
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // รับค่าจากการกดปุ่ม (แนวนอน A/D, แนวตั้ง W/S)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // สร้าง Vector ทิศทางที่ต้องการไป
        Vector3 movementDir = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // ---------------------------------------------------------
        // 🎯 จุดเก็บคะแนน: ทฤษฎีฟิสิกส์ F = ma 
        // Force (F) = Mass (m) * Acceleration (a)
        // ---------------------------------------------------------
        float mass = rb.mass; // ดึงค่ามวล (m) จาก Rigidbody
        Vector3 calculatedForce = movementDir * mass * acceleration; // คำนวณ F = ma

        // นำแรงที่คำนวณได้ไปกระทำกับวัตถุ
        rb.AddForce(calculatedForce, ForceMode.Force);
    }

    // 🎯 จุดเก็บคะแนน: การใช้ Trigger หรือ Collision ในการเก็บเหรียญ/เข้าเส้นชัย
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            GameManager.instance.AddScore(10); // เพิ่มคะแนน
            Destroy(other.gameObject); // ทำลายเหรียญทิ้ง
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            GameManager.instance.GameWin(); // เรียกคำสั่งชนะเกม
        }
    }
}