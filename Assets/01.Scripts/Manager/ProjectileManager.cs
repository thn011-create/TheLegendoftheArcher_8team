using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private static ProjectileManager instance;
    public static ProjectileManager Instance { get { return instance; } }

    [SerializeField] GameObject projectilePrefab;


    private void Awake()
    {
        instance = this;
    }



    /// <summary>
    /// �Ѿ��� �߻��ϴ� �Լ�
    /// </summary>
    /// <param name="rangeWeaponHandler">����</param>
    /// <param name="startPosition">���� ��ġ</param>
    /// <param name="direction">����</param>
    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPosition, Vector2 direction, int key, int bouncing)
    {
        // ���� �ڵ鷯�� źȯ �ε����� ����Ͽ� �߻��� źȯ �������� ������
        GameObject origin = projectilePrefab;

        // źȯ ������Ʈ�� ���� (��ġ: startPosition, ȸ��: ����)
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);

        // ������ źȯ�� ProjectileController ������Ʈ�� ������
        ProjectileController projectileController = obj.GetComponent<ProjectileController>();

        // źȯ �ʱ�ȭ (�̵� ���� �� ���� �ڵ鷯 ���� ����)
        projectileController.Init(direction, rangeWeaponHandler, this, key, bouncing, obj);
    }


}
