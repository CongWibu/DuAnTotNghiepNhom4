using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Người chơi
    public float speed = 2f; // Tốc độ di chuyển
    public float detectionRange = 5f; // Phạm vi phát hiện

    private bool isChasing = false; // Kiểm tra có đang đuổi không

    void Update()
    {
        if (isChasing)
        {
            // Di chuyển quái về phía người chơi
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true; // Khi phát hiện người chơi, bắt đầu đuổi
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = false; // Khi người chơi rời khỏi vùng phát hiện, dừng đuổi
        }
    }
}
