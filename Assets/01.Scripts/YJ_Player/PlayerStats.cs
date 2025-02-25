using UnityEngine;

public class PlayerStats : MonoBehaviour, ICharacter
{
    [SerializeField] private string playerName = "Player";
    [SerializeField] private int level = 1;
    [SerializeField] private float attackDamage = 10; // 공격력 
    [SerializeField] private float attackSpeed = 1f; // 공격속도
    [SerializeField] private float moveSpeed = 5f;  // 이동속도
    [SerializeField] private float currentHealth = 100; // 현재 체력
    [SerializeField] private float maxHealth = 100; // 최대 체력
    [SerializeField] private float experience = 0f; // 경험치
    [SerializeField] private float experienceToNextLevel = 100f; // 다음 레벨 경험치
    [SerializeField] private int extraProjectiles = 0; // 추가 발사체
    [SerializeField] private float criticalChance = 10; //크리티컬 확률
    [SerializeField] private float criticalDamage = 100; // 크리티컬 데미지
    [SerializeField] private float headShotRate = 0f; // 즉사확률
    [SerializeField] private float recoveryRate = 0f; // 추가 회복률
    [SerializeField] private float bloodAbsorptionRate = 0f; //흡혈률
    [SerializeField] private float recoveryDropRate = 0f; // 회복드랍률
    [SerializeField] private float evasionRate = 0f; //회피률
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
        experienceToNextLevel *= 1.2f; // 다음 레벨업 필요 경험치 증가
        SkillUI.Instance.ShowSkillSelection();
    }
}