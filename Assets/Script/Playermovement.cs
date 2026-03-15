using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Physics Settings")]
    public float acceleration = 15f; // ความเร่งตอนออกตัว
    public float maxSpeed = 8f;      // 🛑 จำกัดความเร็วสูงสุดตรงนี้! 🛑
    public float brakingForce = 10f; // ความหนืดตอนเบรก

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
            // 1. ถ้ามีการกดปุ่ม ให้รวบรวมแรงผลัก (ใช้สูตร F = ma เก็บคะแนน)
            Vector3 calculatedForce = movementDir * rb.mass * acceleration;
            rb.AddForce(calculatedForce, ForceMode.Force);
        }
        else
        {
            // 2. ถ้าไม่ได้กดปุ่ม ให้ทำงานระบบเบรก
            if (isFlatGround && !isOnIce)
            {
                Vector3 currentVel = rb.linearVelocity; // Unity 6 ใช้ linearVelocity
                Vector3 oppositeForce = new Vector3(-currentVel.x, 0, -currentVel.z) * brakingForce * rb.mass;

                rb.AddForce(oppositeForce, ForceMode.Force);

                // สั่งให้หยุดกลิ้งไวๆ
                rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, Time.deltaTime * 10f);
            }
        }

        // 3. 🛑 เรียกใช้งานระบบจำกัดความเร็ว
        LimitSpeed();
    }

    // ฟังก์ชันสำหรับจำกัดไม่ให้วิ่งเร็วเกินไป
    private void LimitSpeed()
    {
        // ดึงความเร็วเฉพาะแกน X และ Z (แนวราบ) มาตรวจสอบ แกน Y ปล่อยไว้เผื่อตกจากที่สูง
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // ถ้าความเร็วแนวราบ ทะลุลิมิต maxSpeed ที่เราตั้งไว้
        if (flatVelocity.magnitude > maxSpeed)
        {
            // ทำการตัดความเร็วให้เหลือแค่เท่ากับ maxSpeed
            Vector3 limitedVelocity = flatVelocity.normalized * maxSpeed;

            // จับค่ายัดกลับเข้าไปในตัวละคร (บวกกับค่าความสูง Y เดิม)
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }

    // ฟังก์ชันเช็คการชนกับพื้น
    private void OnCollisionStay(Collision collision)
    {
        Vector3 surfaceNormal = collision.contacts[0].normal;
        isFlatGround = surfaceNormal.y > 0.9f;

        // เช็คน้ำแข็ง
        if (collision.gameObject.CompareTag("IceFloor")) isOnIce = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isFlatGround = false;
        if (collision.gameObject.CompareTag("IceFloor")) isOnIce = false;
    }

    // ฟังก์ชันเก็บเหรียญและเข้าเส้นชัย
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