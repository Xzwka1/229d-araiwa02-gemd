using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Physics Settings")]
    public float acceleration = 10f;
    public float brakingForce = 5f; // เพิ่มตัวแปร: ความแรงในการเบรกเมื่อปล่อยปุ่ม

    // ตัวแปรเช็คสถานะพื้น
    private bool isFlatGround = false;
    private bool isOnIce = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movementDir = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (movementDir.magnitude > 0.1f)
        {
            // 1. ถ้ามีการกดปุ่ม (เดินปกติ)
            Vector3 calculatedForce = movementDir * rb.mass * acceleration;
            rb.AddForce(calculatedForce, ForceMode.Force);
        }
        else
        {
            // 2. ถ้าไม่ได้กดปุ่ม ให้เช็คว่าต้องเบรกไหม?
            // จะเบรกก็ต่อเมื่อ: อยู่บน "พื้นเรียบ" และ "ไม่ใช่น้ำแข็ง"
            if (isFlatGround && !isOnIce)
            {
                // สร้างแรงต้าน (สวนทางกับทิศทางที่กำลังไถลไป) เฉพาะแกน X และ Z
                Vector3 currentVel = rb.linearVelocity;
                Vector3 oppositeForce = new Vector3(-currentVel.x, 0, -currentVel.z) * brakingForce * rb.mass;

                rb.AddForce(oppositeForce, ForceMode.Force);

                // สั่งให้หยุดกลิ้ง (ลดค่าการหมุนให้เป็น 0 ไวๆ)
                rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, Time.deltaTime * 10f);
            }
        }
    }

    // ฟังก์ชันเช็คการชนกับพื้น
    private void OnCollisionStay(Collision collision)
    {
        // 🎯 จุดเก็บคะแนนฟิสิกส์: เช็คความเอียงของพื้นด้วย Surface Normal (Vector ตั้งฉาก)
        Vector3 surfaceNormal = collision.contacts[0].normal;

        // ถ้าค่าแกน Y ของ Normal เข้าใกล้ 1 แปลว่าเป็นพื้นราบ (ถ้าพื้นเอียง ค่า Y จะน้อยกว่านี้)
        if (surfaceNormal.y > 0.9f)
        {
            isFlatGround = true;
        }
        else
        {
            isFlatGround = false; // รู้ทันทีว่านี่คือทางลาดเอียง!
        }

        // เช็คว่าพื้นนี้คือน้ำแข็งหรือไม่ (โดยดูจาก Tag)
        if (collision.gameObject.CompareTag("IceFloor"))
        {
            isOnIce = true;
        }
        else
        {
            isOnIce = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // เมื่อลอยสกลางอากาศ หรือหลุดจากพื้น ให้รีเซ็ตค่า
        isFlatGround = false;
        isOnIce = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            GameManager.instance.AddScore(10);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            GameManager.instance.GameWin();
        }
    }
}