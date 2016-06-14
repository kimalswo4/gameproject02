using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CommandResult
{
    Success, Fail, End
}

public class SkillManager : Singleton<SkillManager> {

    private const float REMAINING_TIME = 3.0f;
    private const float CASTING_TIME = 1.2f;

    private float _remainingTime;
    private bool _isSkill;
    private Skill _currentSkill;
    private Dictionary<KeyCode, Skill> _skillList;
    private int _currentCommandIndex;
    private Gauge _inputTimeGauge;

    // Use this for initialization
    void Start () {
        _skillList = new Dictionary<KeyCode, Skill>();
        _skillList.Add(KeyCode.A, new Skill_A());
        _skillList.Add(KeyCode.S, new Skill_S());
        _skillList.Add(KeyCode.D, new Skill_D());
        _skillList.Add(KeyCode.F, new Skill_F());

        _currentSkill = _skillList[KeyCode.F];

        _currentCommandIndex = 0;
        _inputTimeGauge = (Instantiate(Resources.Load("Prefabs/Gauge")) as GameObject).GetComponent<Gauge>();
        _inputTimeGauge.Initialize(600, 20, 0, REMAINING_TIME, REMAINING_TIME, new Vector2(-100, 60), Color.yellow);
        _inputTimeGauge.SetActive(false);
        _inputTimeGauge.SetAction(new Action(TimeFail));
    }
	
	// Update is called once per frame
	public void SkillManagerUpdate () {
        if (_isSkill && !Player.Instance.IsActive)
        {
            _remainingTime -= Time.deltaTime;
            _inputTimeGauge.ChangeValue(-Time.deltaTime);
        }
	}

    public void Initialize()
    {
        _isSkill = false;
        _inputTimeGauge.SetActive(false);
        _inputTimeGauge.Reset();
    } 
    public void SetSkill(KeyCode key)
    {
        _remainingTime = REMAINING_TIME;

        _isSkill = true;
        _currentSkill = _skillList[key];
        _currentSkill.SetCommandNumber();
        _currentSkill.SetCommand();

        _inputTimeGauge.SetActive(true);
    }
    public void InputCommad(KeyCode key)
    {
        switch (_currentSkill.EqualCommand(key))
        {
            case CommandResult.Success: // 한단계 성공
                UIManager.Instance.SetInputResult(_currentCommandIndex, CommandResult.Success);
                _currentCommandIndex++;
                break;
            case CommandResult.Fail:    // 실패
                UIManager.Instance.SetInputResult(_currentCommandIndex, CommandResult.Fail);
                UIManager.Instance.SetResultText("Fail");
                Player.Instance.Skill_Magnification = 0.5f;
                StartCoroutine(Fail());
                _currentCommandIndex = 0;
                break;
            case CommandResult.End:    // 끝까지 성공
                UIManager.Instance.SetInputResult(_currentCommandIndex, CommandResult.Success);
                if (_remainingTime >= 2.5f)
                {
                    UIManager.Instance.SetResultText("Perfect");
                    Player.Instance.Skill_Magnification = 2.0f;
                }
                else if (_remainingTime >= 2.0f)
                {
                    UIManager.Instance.SetResultText("Good");
                    Player.Instance.Skill_Magnification = 1.5f;
                }
                else if (_remainingTime >= 1.0f)
                {
                    UIManager.Instance.SetResultText("Nice");
                    Player.Instance.Skill_Magnification = 1.2f;
                }
                else
                {
                    UIManager.Instance.SetResultText("OK");
                    Player.Instance.Skill_Magnification = 1.0f;
                }
                StartCoroutine(Success());
                _currentCommandIndex = 0;
                break;
        }
    }

    public IEnumerator Success()
    {
        _currentSkill.Action();
        Player.Instance.IsActive = true;
        yield return new WaitForSeconds(CASTING_TIME);
        Player.Instance.IsActive = false;
        Player.Instance.IsSkill = false;
        UIManager.Instance.Initialize();

        Initialize();
    }
    public IEnumerator Fail()
    {
        _currentSkill.Action();
        Player.Instance.IsActive = true;
        yield return new WaitForSeconds(CASTING_TIME);
        Player.Instance.IsActive = false;
        Player.Instance.IsSkill = false;
        UIManager.Instance.Initialize();

        Initialize();
    }

    public void TimeFail()
    {
        UIManager.Instance.SetResultText("Fail");
        Player.Instance.Skill_Magnification = 0.5f;
        StartCoroutine(Fail());
        _currentCommandIndex = 0;
    }
}
