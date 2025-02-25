using UnityEngine;

public class PlayerStats : MonoBehaviour, ICharacter
{
    [SerializeField] private string playerName = "Player";
    [SerializeField] private int level = 1;
    [SerializeField] private int attackDamage = 10; // 공격력 
    [SerializeField] private float attackSpeed = 1f; // 공격속도
    [SerializeField] private float moveSpeed = 5f;  // 이동속도
    [SerializeField] private int currentHealth = 100; // 현재 체력
    [SerializeField] private int maxHealth = 100; // 최대 체력
    [SerializeField] private float experience = 0f; // 경험치
    [SerializeField] private float experienceToNextLevel = 100f; // 다음 레벨 경험치
    [SerializeField] private int extraProjectiles = 0; // 추가 발사체
    [SerializeField] private int criticalChance = 10; //크리티컬 확률
    [SerializeField] private int criticalDamage = 100; // 크리티컬 데미지
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
        experienceToNextLevel *= 1.2f; // 다음 레벨업 필요 경험치 증가
        //skillUI.ShowSkillSelection();
    }
}
