using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Info : MonoBehaviour {

    [SerializeField]
    private Text _level;
    [SerializeField]
    private Text _exp;
    [SerializeField]
    private Text _hp;
    [SerializeField]
    private Text _atk;
    [SerializeField]
    private Text _skill_A;
    [SerializeField]
    private Text _skill_S;
    [SerializeField]
    private Text _skill_D;
    [SerializeField]
    private Text _skill_F;

    public void Start()
    {
        StateManager.Instance.Gold += 0;
    }

    public void SetInfo()
    {
        _level.text = StateManager.Instance.Level.ToString();
        _exp.text = StateManager.Instance.Exp.ToString() + " / " + StateManager.Instance.MaxExp.ToString();
        _hp.text = StateManager.Instance.MaxHP.ToString();
        _atk.text = StateManager.Instance.Atk.ToString();
        _skill_A.text = (TrainingManager.Instance.Skill_A + 100).ToString() + "%" ;
        _skill_S.text = (TrainingManager.Instance.Skill_S + 100).ToString() + "%";
        _skill_D.text = (TrainingManager.Instance.Skill_D + 100).ToString() + "%";
        _skill_F.text = (TrainingManager.Instance.Skill_F + 100).ToString() + "%";
    }
}
