using UnityEngine;

public class ExperienceItem : MonoBehaviour
{
    public int experienceAmount = 10; // ����ġ ��

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().GainExperience(experienceAmount);
            Destroy(gameObject);
        }
    }
}
