using UnityEngine;

public class PlayerStats : MonoBehaviour, ICharacter
{
    [SerializeField] private string playerName = "Player";
    [SerializeField] private int level = 1;
    [SerializeField] private float attackDamage = 10; // ���ݷ� 
    [SerializeField] private float attackSpeed = 1f; // ���ݼӵ�
    [SerializeField] private float moveSpeed = 5f;  // �̵��ӵ�
    [SerializeField] private float currentHealth = 100; // ���� ü��
    [SerializeField] private float maxHealth = 100; // �ִ� ü��
    [SerializeField] private float experience = 0f; // ����ġ
    [SerializeField] private float experienceToNextLevel = 100f; // ���� ���� ����ġ
    [SerializeField] private int extraProjectiles = 0; // �߰� �߻�ü
    [SerializeField] private float criticalChance = 10; //ũ��Ƽ�� Ȯ��
    [SerializeField] private float criticalDamage = 100; // ũ��Ƽ�� ������
    [SerializeField] private float headShotRate = 0f; // ���Ȯ��
    [SerializeField] private float recoveryRate = 0f; // �߰� ȸ����
    [SerializeField] private float bloodAbsorptionRate = 0f; //������
    [SerializeField] private float recoveryDropRate = 0f; // ȸ�������
    [SerializeField] private float evasionRate = 0f; //ȸ�Ƿ�
    [SerializeField] private SkillUI skillUI;
    public string Name { get => playerName; set => playerName = value; }
    public int Level { get => level; set => level = value; }
    public float AttackDamage { get => attackDamage; set => attackDamage = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Experience { get => experience; set => experience = value; }
    public int ExtraProjectiles { get => extraProjectiles; set => extraProjectiles = value; }
    public float CriticalChance { get => criticalChance; set => criticalChance = value; }
    public float CriticalDamage { get => criticalDamage; set => criticalDamage = value; }
    public float HeadShotRate { get => headShotRate; set => headShotRate = value; }
    public float RecoveryRate { get => recoveryRate; set => recoveryRate = value; }
    public float BloodAbsorptionRate { get => bloodAbsorptionRate; set => bloodAbsorptionRate = value; }
    public float RecoveryDropRate { get => recoveryDropRate; set => recoveryDropRate = value; }
    public float Evasionrate { get => Evasionrate; set => Evasionrate = value; }

    public void GainExperience(float amount)
    {
        experience += amount;
        if (experience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        experience -= experienceToNextLevel;
        experienceToNextLevel *= 1.2f; // ���� ������ �ʿ� ����ġ ����
        SkillUI.Instance.ShowSkillSelection();
    }
}