using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Training : MonoBehaviour
{

    public Text _atkCost;
    public Text _hpCost;
    public Text _skill_A_Cost;
    public Text _skill_S_Cost;
    public Text _skill_D_Cost;
    public Text _skill_F_Cost;

    public Text _atkLevel;
    public Text _hpLevel;
    public Text _skill_A_Level;
    public Text _skill_S_Level;
    public Text _skill_D_Level;
    public Text _skill_F_Level;

    public Text _atkValue;
    public Text _hpValue;
    public Text _skill_A_Value;
    public Text _skill_S_Value;
    public Text _skill_D_Value;
    public Text _skill_F_Value;

    public void SetTraining()
    {
        _atkCost.text = TrainingManager.Instance.GetCost(TrainingManager.Instance._atk_Level).ToString();
        _hpCost.text = TrainingManager.Instance.GetCost(TrainingManager.Instance._hp_Level).ToString();
        _skill_A_Cost.text = TrainingManager.Instance.GetCost(TrainingManager.Instance._skill_A_Level).ToString();
        _skill_S_Cost.text = TrainingManager.Instance.GetCost(TrainingManager.Instance._skill_S_Level).ToString();
        _skill_D_Cost.text = TrainingManager.Instance.GetCost(TrainingManager.Instance._skill_D_Level).ToString();
        _skill_F_Cost.text = TrainingManager.Instance.GetCost(TrainingManager.Instance._skill_F_Level).ToString();

        _atkLevel.text = "Level " + TrainingManager.Instance._atk_Level.ToString();
        _hpLevel.text = "Level " + TrainingManager.Instance._hp_Level.ToString();
        _skill_A_Level.text = "Level " + TrainingManager.Instance._skill_A_Level.ToString();
        _skill_S_Level.text = "Level " + TrainingManager.Instance._skill_S_Level.ToString();
        _skill_D_Level.text = "Level " + TrainingManager.Instance._skill_D_Level.ToString();
        _skill_F_Level.text = "Level " + TrainingManager.Instance._skill_F_Level.ToString();

        _atkValue.text = (TrainingManager.Instance._atkTable[TrainingManager.Instance._atk_Level+1] - TrainingManager.Instance.Atk).ToString() + "+";
        _hpValue.text = (TrainingManager.Instance._hpTable[TrainingManager.Instance._hp_Level + 1] - TrainingManager.Instance.HP).ToString() + "+";
        _skill_A_Value.text = (TrainingManager.Instance._skill_A_Table[TrainingManager.Instance._skill_A_Level + 1] - TrainingManager.Instance.Skill_A).ToString() + "+";
        _skill_S_Value.text = (TrainingManager.Instance._skill_S_Table[TrainingManager.Instance._skill_S_Level + 1] - TrainingManager.Instance.Skill_S).ToString() + "+";
        _skill_D_Value.text = (TrainingManager.Instance._skill_D_Table[TrainingManager.Instance._skill_D_Level + 1] - TrainingManager.Instance.Skill_D).ToString() + "+";
        _skill_F_Value.text = (TrainingManager.Instance._skill_F_Table[TrainingManager.Instance._skill_F_Level + 1] - TrainingManager.Instance.Skill_F).ToString() + "+";
    }

    public void BuyATK()
    {
        if (TrainingManager.Instance.GetCost(TrainingManager.Instance._atk_Level) <= StateManager.Instance.Gold)
        {
            StateManager.Instance.Gold -= TrainingManager.Instance.GetCost(TrainingManager.Instance._atk_Level);
            TrainingManager.Instance._atk_Level++;
            SetTraining();
        }
        
    }
    public void BuyHP()
    {
        if (TrainingManager.Instance.GetCost(TrainingManager.Instance._hp_Level) <= StateManager.Instance.Gold)
        {
            StateManager.Instance.Gold -= TrainingManager.Instance.GetCost(TrainingManager.Instance._hp_Level);
            TrainingManager.Instance._hp_Level++;
            SetTraining();
        }
    }
    public void Buy_A_Skill()
    {
        if (TrainingManager.Instance.GetCost(TrainingManager.Instance._skill_A_Level) <= StateManager.Instance.Gold)
        {
            StateManager.Instance.Gold -= TrainingManager.Instance.GetCost(TrainingManager.Instance._skill_A_Level);
            TrainingManager.Instance._skill_A_Level++;
            SetTraining();
        }
    }
    public void Buy_S_Skill()
    {
        if (TrainingManager.Instance.GetCost(TrainingManager.Instance._skill_S_Level) <= StateManager.Instance.Gold)
        {
            StateManager.Instance.Gold -= TrainingManager.Instance.GetCost(TrainingManager.Instance._skill_S_Level);
            TrainingManager.Instance._skill_S_Level++;
            SetTraining();
        }
    }
    public void Buy_D_Skill()
    {
        if (TrainingManager.Instance.GetCost(TrainingManager.Instance._skill_D_Level) <= StateManager.Instance.Gold)
        {
            StateManager.Instance.Gold -= TrainingManager.Instance.GetCost(TrainingManager.Instance._skill_D_Level);
            TrainingManager.Instance._skill_D_Level++;
            SetTraining();
        }
    }
    public void Buy_F_Skill()
    {
        if (TrainingManager.Instance.GetCost(TrainingManager.Instance._skill_F_Level) <= StateManager.Instance.Gold)
        {
            StateManager.Instance.Gold -= TrainingManager.Instance.GetCost(TrainingManager.Instance._skill_F_Level);
            TrainingManager.Instance._skill_F_Level++;
            SetTraining();
        }
    }
}
