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
        // 아이템을 생성하고 위치를 설정
        GameObject item = Instantiate(itemPrefab, dropPosition, Quaternion.identity);

        // 랜덤한 방향으로 흩뿌리기
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float force = Random.Range(1f, 3f); // 흩뿌리는 힘을 랜덤으로 설정

        // Rigidbody2D 컴포넌트를 추가하여 물리 효과를 적용
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = item.AddComponent<Rigidbody2D>();
        }

        // 아이템에 힘을 가하여 흩뿌리기
        rb.AddForce(randomDirection * force, ForceMode2D.Impulse);
    }

    //몬스터에 추가
    /*public void Die()
    {
        // 몬스터가 죽은 위치에서 아이템을 생성
        dropItemManager.Drop(transform.position);
        Destroy(gameObject);
    }*/
}
