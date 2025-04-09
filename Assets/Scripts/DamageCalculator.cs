using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCalculator : MonoBehaviour
{
    /*
     기본 공격

​       대상의 적용 방어력 = ( 대상의 방어력 * ( 1 - 방어 관통(%) ) ) - 방어 관통

       공격력 계산 피해 = (( 공격력 * 100 / (100 + 대상의 적용 방어력) * (1 + 치명타 발생 시, (0.65 + 치명타 피해 증가(%))))

       피해 증가 = 공격력 계산 피해 + ( 공격력 계산 피해 * ( 피해 증가(%) - 대상의 피해 감소(%) ))

       기본 공격(%) = 피해 증가 * ( 1+ 기본공격 추가 피해(%) - 대상의 기본공격 피해 감소(%) )

       기본 공격 추가 = 기본 공격(%) + ( 기본공격 추가 피해 - 대상의 기본공격 피해 감소 )

       최종 피해량 = ( 기본 공격 추가 * 최종피해 추가(%) ) + 최종피해 추가 )

       모드 보정 최종 피해량 = 최종 피해량 * ( 모드별 주는 피해 보정(%) - 대상의 모드별 받는 피해 보정(%) )

    1. 대상의 적용 방어력 2. 대상의 방어력 3. 방어 관통(%) 4. 방어 관통 5. 공격력 계산 피해 6. 공격력 7. 치명타 발생 시 8. 치명타 피해 증가(%)
    9. 피해 증가 10. 피해 증가(%) 11. 대상의 피해 감소(%) 12. 기본 공격(%) 13.  기본공격 추가 피해(%) 14. 대상의 기본공격 피해 감소(%)
    15. 기본 공격 추가 16. 기본공격 추가 피해 17. 대상의 기본공격 피해 감소 18. 최종 피해량 19. 최종피해 추가(%) 20. 최종피해 추가
    
    스킬 공격

       대상의 적용 방어력 = ( 대상의 방어력 * ( 1 - 방어 관통(%) ) ) - 방어 관통

       스킬의 기본 피해 = ( 스킬에 정의된 피해* 100 / (100 + 대상의 적용 방어력))

       피해 증가 = 스킬의 기본 피해 + ( 스킬의 기본 피해 * ( 피해 증가(%) - 대상의 피해 감소(%) ))

       스킬 피해 감소(%) = 피해 증가 * ( 1 - 대상의 스킬 피해 감소(%))

       최종 피해량 = ( 스킬 피해 감소(%) * 최종 피해 추가(%) ) + 최종 피해 추가

       모드 보정 최종 피해량 = 최종 피해량 * ( 모드별 주는 피해 보정(%) - 대상의 모드별 받는 피해 보정(%) )

    1. 대상의 적용 방어력 2. 대상의 방어력 3. 방어 관통(%) 4. 방어 관통 5. 스킬의 기본 피해 6. 스킬에 정의된 피해 
    7. 피해 증가 8. 피해 증가(%) 9. 대상의 피해 감소(%) 10. 스킬 피해 감소(%) 11. 대상의 스킬 피해 감소(%) 
    12. 최종 피해량 13. 최종 피해 추가(%) 14. 최종 피해 추가

    방숙작
       기본 공격 피해 감소 1%
       스킬 피해 감소 0.8%
    */

    float applied_defense;                                  // 대상의 적용 방어력
    float defense;                                          // 대상의 방어력
    float percentage_defense_penetration;                   // 방어 관통(%)
    float defense_penetration;                              // 방어 관통
    float skill_base_damage;                                // 스킬의 기본 피해
    float skill_defined_damage;                             // 스킬에 정의된 피해
    float damage_increase;                                  // 피해 증가(%)
    float damage_reduction;                                 // 대상의 피해 감소(%)
    float increased_skill_damage;                           // 증가된 스킬 피해
    float skill_damage_reduction;                           // 대상의 스킬 피해 감소(%)
    float reduced_skill_damage;                             // 감소된 스킬 피해
    float percentage_final_damage_addition;                 // 최종 피해 추가(%)
    float final_damage_addition;                            // 최종 피해 추가
    float final_skill_damage;                               // 최종 스킬 피해

    public InputField[] inputFields;
    public InputField outputDamage;

    // Start is called before the first frame update
    void Start()
    {
        Initialize_Variables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialize_Variables()
    {
        applied_defense = 0;
        defense = 0;
        percentage_defense_penetration = 0;
        defense_penetration = 0;
        skill_base_damage = 0;
        skill_defined_damage = 0;
        damage_increase = 0;
        damage_reduction = 0;
        increased_skill_damage = 0;
        skill_damage_reduction = 0;
        reduced_skill_damage = 0;
        percentage_final_damage_addition = 0;
        final_damage_addition = 0;
        final_skill_damage = 0;
    }

    void Calculate_Applied_Defense()
    {
        applied_defense = defense * (1 - percentage_defense_penetration) - defense_penetration;
    }

    void Calculate_Skill_Base_Damage()
    {
        skill_base_damage = Mathf.Floor(skill_defined_damage * 100 / (100 + applied_defense));
    }

    void Calculate_Increased_Skill_Damage()
    {
        increased_skill_damage = skill_base_damage + (skill_base_damage * (damage_increase - damage_reduction));
    }

    void Calculate_Reduced_Skill_Damage()
    {
        reduced_skill_damage = increased_skill_damage * (1 - skill_damage_reduction);
    }

    void Calculate_Final_Skill_Damage()
    {
        final_skill_damage = reduced_skill_damage * percentage_final_damage_addition + final_damage_addition;
    }

    void Calculate_Skill_Damage()
    {
        Calculate_Applied_Defense();
        Calculate_Skill_Base_Damage();
        Calculate_Increased_Skill_Damage();
        Calculate_Reduced_Skill_Damage();
        Calculate_Final_Skill_Damage();
        outputDamage.text = final_skill_damage.ToString();
    }

    public void Input_Value(int number, float value)
    {
        switch (number)
        {
            case 0:
                defense = value;
                break;
            case 1:
                percentage_defense_penetration = value;
                break;
            case 2:
                defense_penetration = value;
                break;
            case 3:
                skill_defined_damage = value;
                break;
            case 4:
                damage_increase = value;
                break;
            case 5:
                damage_reduction = value;
                break;
            case 6:
                skill_damage_reduction = value;
                break;
            case 7:
                percentage_final_damage_addition = value;
                break;
            case 8:
                final_damage_addition = value;
                break;
            default:
                break;
        }
        Calculate_Skill_Damage();
    }
}
