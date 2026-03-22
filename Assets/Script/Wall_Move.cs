using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [Header("การตั้งค่าเคลื่อนที่")]
    public float speed = 3f; // ความเร็วในการเลื่อน
    public float distance = 5f; // ระยะทางที่เลื่อนไปซ้าย-ขวา

    [Header("การตั้งค่าฟิสิกส์ (F = ma)")]
    public float targetAcceleration = 50f; // ความเร่งที่ต้องการให้กระเด็น (a)

    private Vector3 startPosition;

    void Start()
    {
        // จำตำแหน่งเริ่มต้นไว้
        startPosition = transform.position;
    }

    void Update()
    {
        // ทำให้วัตถุขยับไปมาซ้าย-ขวา (แกน X) แบบ PingPong
        float newX = startPosition.x + Mathf.Sin(Time.time * speed) * distance;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    // 🎯 จุดเก็บคะแนน: ใช้ OnCollisionEnter เช็คการชน และใช้สูตร F=ma
    private void OnCollisionEnter(Collision collision)
    {
        // ถ้าสิ่งที่ชนคือ Player
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                // 1. หาทิศทางที่จะให้กระเด็นถอยหลัง (สวนทางกับจุดที่ชน)
                Vector3 knockbackDir = collision.contacts[0].normal;

                // 2. ดึงค่ามวล (Mass) ของผู้เล่นมาเป็นตัวแปร m
                float m = playerRb.mass;

                // 3. กำหนดความเร่งเป็นตัวแปร a
                float a = targetAcceleration;

                // 4. เข้าสูตรนิวตัน F = ma เพื่อหาแรงผลักลัพธ์
                float calculatedForce = m * a;

                // 5. นำแรงผลักที่คำนวณได้ ไปใส่ใน AddForce (ใช้ ForceMode.Impulse สำหรับการกระแทกฉับพลัน)
                playerRb.AddForce(-knockbackDir * calculatedForce, ForceMode.Impulse);

                Debug.Log("ผู้เล่นโดนกำแพงชนกระเด็นด้วยแรงตามสูตร F=ma!");
            }
        }
    }
}