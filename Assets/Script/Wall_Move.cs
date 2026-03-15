using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [Header("การตั้งค่าเคลื่อนที่")]
    public float speed = 3f; // ความเร็วในการเลื่อน
    public float distance = 5f; // ระยะทางที่เลื่อนไปซ้าย-ขวา

    [Header("ความแรงตอนชนผู้เล่น")]
    public float knockbackForce = 500f; // แรงกระเด็นเมื่อชน

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

    // 🎯 จุดเก็บคะแนน: ใช้ OnCollisionEnter เช็คการชน
    private void OnCollisionEnter(Collision collision)
    {
        // ถ้าสิ่งที่ชนคือ Player
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                // คำนวณทิศทางให้ผู้เล่นกระเด็นถอยหลัง (สวนทางกับที่วิ่งมา)
                Vector3 knockbackDir = collision.contacts[0].normal;

                // ออกแรงผลักผู้เล่นกระเด็น
                playerRb.AddForce(-knockbackDir * knockbackForce, ForceMode.Impulse);

                Debug.Log("ผู้เล่นโดนกำแพงชนกระเด็น!");
            }
        }
    }
}