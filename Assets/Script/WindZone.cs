using UnityEngine;

public class WindZone : MonoBehaviour
{
    [Header("Wind Settings")]
    public Vector3 windDirection = new Vector3(1, 0, 0); // ทิศทางลมพัด (แกน X)
    public float windStrength = 15f; // ความแรงลม

    // เมื่อผู้เล่นอยู่ในโซนนี้ จะโดนลมพัดตลอดเวลา
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                // ดันผู้เล่นไปตามทิศทางลม
                playerRb.AddForce(windDirection * windStrength, ForceMode.Force);
            }
        }
    }
}