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
        // ��ų ȿ�� ���� ���� (��: ���ݷ� ����)
    }
}
