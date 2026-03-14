using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("เป้าหมายที่กล้องจะตาม")]
    public Transform target; // ลาก Player มาใส่ช่องนี้

    private Vector3 offset; // ระยะห่างระหว่างกล้องกับผู้เล่น

    void Start()
    {
        // คำนวณระยะห่างเริ่มต้นตอนเริ่มเกม
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    // ใช้ LateUpdate สำหรับกล้อง เพื่อให้มั่นใจว่าตัวละครขยับเสร็จก่อนแล้วกล้องค่อยตาม
    void LateUpdate()
    {
        if (target != null)
        {
            // อัปเดตตำแหน่งกล้องตามผู้เล่น + ระยะห่างเดิม (โดยไม่ยุ่งกับการหมุน)
            transform.position = target.position + offset;
        }
    }
}