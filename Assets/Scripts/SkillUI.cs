using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public Image cooldownOverlay;
    public GameObject player;
    public int skillIndex = 0;
    
    private PlayerSkill playerSkill;

    void Start()
    {
        playerSkill = player.GetComponent<PlayerSkills>().Skill(skillIndex);;
        GetComponent<Image>().sprite = playerSkill.SkillData.SkillIcon;
        GetComponent<Image>().color = Color.white;

        cooldownOverlay.GetComponent<Image>().sprite = playerSkill.SkillData.SkillIcon;
        cooldownOverlay.GetComponent<Image>().color = new Color(38/255f, 38/255f, 38/255f, 240/255f);
    }

    // Update is called once per frame
    void Update()
    {
        float remainingCooldown = playerSkill.GetCooldownRemaining();
        float totalCooldown = playerSkill.SkillData.CooldownTime;

        if (remainingCooldown > 0)
        {
            cooldownOverlay.fillAmount = remainingCooldown / totalCooldown;
        }
        else
        {
            cooldownOverlay.fillAmount = 0f;
        }
    }
}
