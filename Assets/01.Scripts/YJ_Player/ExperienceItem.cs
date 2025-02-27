using UnityEngine;

public class ExperienceItem : MonoBehaviour
{
    public int experienceAmount = 10; // ����ġ ��

    public AudioClip ItemClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.PlayClip(ItemClip);
            collision.GetComponent<PlayerStats>().GainExperience(experienceAmount);
            Destroy(gameObject);
        }
    }
}
