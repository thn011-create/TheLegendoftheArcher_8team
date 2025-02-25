using UnityEngine;

public class PlayerStats : MonoBehaviour, ICharacter
{
    [SerializeField] private string playerName = "Player";
    [SerializeField] private int level = 1;
    [SerializeField] private int attackDamage = 10; // ���ݷ� 
    [SerializeField] private float attackSpeed = 1f; // ���ݼӵ�
    [SerializeField] private float moveSpeed = 5f;  // �̵��ӵ�
    [SerializeField] private int currentHealth = 100; // ���� ü��
    [SerializeField] private int maxHealth = 100; // �ִ� ü��
    [SerializeField] private float experience = 0f; // ����ġ
    [SerializeField] private float experienceToNextLevel = 100f; // ���� ���� ����ġ
    [SerializeField] private int extraProjectiles = 0; // �߰� �߻�ü
    [SerializeField] private int criticalChance = 10; //ũ��Ƽ�� Ȯ��
    [SerializeField] private int criticalDamage = 100; // ũ��Ƽ�� ������
    //[SerializeField] private SkillUI skillUI;
    public string Name { get => playerName; set => playerName = value; }
    public int Level { get => level; set => level = value; }
    public int AttackDamage { get => attackDamage; set => attackDamage = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Experience { get => experience; set => experience = value; }
    public int ExtraProjectiles { get => extraProjectiles; set => extraProjectiles = value; }  
    public int CriticalChance {  get => criticalChance; set => criticalChance = value; }
    public int CriticalDamage {  get => criticalDamage; set => criticalDamage = value; }

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
        //skillUI.ShowSkillSelection();
    }
}
