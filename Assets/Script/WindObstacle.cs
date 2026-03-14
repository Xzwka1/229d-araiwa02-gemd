using UnityEngine;

// 🎯 แก้ชื่อตรงนี้จาก WindZone เป็น WindObstacle ให้ตรงกับชื่อไฟล์
public class WindObstacle : MonoBehaviour
{
    [Header("Wind Settings")]
    public Vector3 windDirection = new Vector3(1, 0, 0);
    public float windStrength = 15f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.AddForce(windDirection * windStrength, ForceMode.Force);
            }
        }
    }
}