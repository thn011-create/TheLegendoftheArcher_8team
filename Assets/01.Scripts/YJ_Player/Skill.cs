[System.Serializable]
public class Skill
{
    public string skillName;
    public string description;

    public Skill(string name, string desc)
    {
        skillName = name;
        description = desc;
    }

    public void ApplyEffect(PlayerStats player)
    {
        // 스킬 효과 적용 로직 (예: 공격력 증가)
    }
}
