using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemManager : MonoBehaviour
{
    [SerializeField] protected GameObject healpack;
    [SerializeField] protected GameObject exp;
    private DropItem HealPack;
    private DropItem Exp;

    public void Drop(Vector2 dropPosition)
    {
        if (healpack == null) 
        {
            HealPack = DataManager.Instance.DropItemLoader.GetByKey(100);
        }
        if (exp == null)
        {
            Exp = DataManager.Instance.DropItemLoader.GetByKey(101);
        }

        if (Random.Range(0, 101) < 30)
        {
            DropItem(healpack, dropPosition);
        }

        for (int i = 0; i < 5; i++)
        {
            DropItem(exp, dropPosition);
        }
    }
    private void DropItem(GameObject itemPrefab, Vector2 dropPosition)
    {
        // �������� �����ϰ� ��ġ�� ����
        GameObject item = Instantiate(itemPrefab, dropPosition, Quaternion.identity);

        // ������ �������� ��Ѹ���
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float force = Random.Range(1f, 3f); // ��Ѹ��� ���� �������� ����

        // Rigidbody2D ������Ʈ�� �߰��Ͽ� ���� ȿ���� ����
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = item.AddComponent<Rigidbody2D>();
        }

        // �����ۿ� ���� ���Ͽ� ��Ѹ���
        rb.AddForce(randomDirection * force, ForceMode2D.Impulse);
    }

    //���Ϳ� �߰�
    /*public void Die()
    {
        // ���Ͱ� ���� ��ġ���� �������� ����
        dropItemManager.Drop(transform.position);
        Destroy(gameObject);
    }*/
}
