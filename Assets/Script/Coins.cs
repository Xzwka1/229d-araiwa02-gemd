using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public float spinSpeed = 100f; // ความเร็วในการหมุน

    void Update()
    {
        // หมุนเหรียญรอบแกน Y ไปเรื่อยๆ
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }
}