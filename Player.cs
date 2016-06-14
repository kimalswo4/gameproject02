using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerAttackType
{
    Skill_A, Skill_S, Skill_D, Skill_F, Basic_Attack
}
public enum SkillCoolTime
{
    Skill_A = 8, Skill_S = 12, Skill_D = 15, Skill_F = 40, Dash = 5, Postion = 10
}

[RequireComponent(typeof(SpriteRenderer))]
public class Player : Singleton<Player> {

    private const float Floor = -1.9f; // 바닥 지형값
    private const float MOVE_SPEED = 5.0f;
    private const float JUMP_SPEED = 2.0f;
    private const float JUMP = 1.0f;
    private const float DASH = 2.0f;
    private const float DASH_SPEED = 10.0f;
    private const float GA = 0.05f;  // 중력가속도
    
    private Dictionary<SkillCoolTime, bool> _coolTime;   

    public bool IsSkill { set; get; }
    public bool IsActive { set; get; }
    public float Skill_Magnification { set; get; }

    private bool _isJump;
    private float _jumpValue;
    private float _ga;  // 중력 가속도

    private Sprite _texture;
    private Animator _animator;
    private Animator _effectAnimator;
    private BoxCollider2D _hitBox;

    private Gauge _hpGauge;

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    public void PlayerUpdate()
    {
        if (IsActive)
            return;

        if (!IsSkill)
        {
            Move();
            InputAction();
            SkillSetting();
        }
        else
        {
            InputCommand();
        }

        if (_isJump)
        {
            if (_jumpValue < JUMP)
            {
                _jumpValue += JUMP_SPEED * Time.deltaTime;
                this.transform.position += new Vector3(0.0f, JUMP_SPEED * Time.deltaTime);
            }
            else
            {
                if (this.transform.position.y <= Floor)
                {
                    _ga = 0.0f;
                    _jumpValue = 0.0f;
                    _isJump = false;
                    this.transform.position = new Vector2(this.transform.position.x, Floor);
                }
                else
                {
                    _ga += GA * Time.deltaTime;
                    this.transform.position -= new Vector3(0.0f, JUMP_SPEED * Time.deltaTime + _ga);
                }
            }
        }

        //
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _animator.SetTrigger("Skill_A");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _animator.SetTrigger("Skill_S");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _animator.SetTrigger("Skill_D");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _animator.SetTrigger("Skill_F");
        }
    }

    public void Initialize()
    {
        _texture = this.GetComponent<SpriteRenderer>().sprite;

        _animator = this.GetComponent<Animator>();
        _effectAnimator = this.transform.FindChild("PlayerEffect").GetComponent<Animator>();
        _hitBox = this.transform.GetComponent<BoxCollider2D>();

        IsSkill = false;
        IsActive = false;
        _hitBox.enabled = true;

        _jumpValue = 0.0f;
        _isJump = false;
        _ga = 0.0f;

        if (_hpGauge != null)
            Destroy(_hpGauge);

        _hpGauge = (Instantiate(Resources.Load("Prefabs/Gauge")) as GameObject).GetComponent<Gauge>();
        _hpGauge.Initialize(300, 40, 0, StateManager.Instance.MaxHP, StateManager.Instance.MaxHP, new Vector2(-310, 305), Color.red);
        _hpGauge.SetActive(true);
        _hpGauge.SetAction(new Action(Dead));

        StateManager.Instance.HP = StateManager.Instance.MaxHP;

        _coolTime = new Dictionary<SkillCoolTime, bool>();
        _coolTime.Add(SkillCoolTime.Dash, true);
        _coolTime.Add(SkillCoolTime.Postion, true);
        _coolTime.Add(SkillCoolTime.Skill_A, true);
        _coolTime.Add(SkillCoolTime.Skill_S, true);
        _coolTime.Add(SkillCoolTime.Skill_D, true);
        _coolTime.Add(SkillCoolTime.Skill_F, true);
    }
    public void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // ?
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            // ?
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);  // 회전

            this.transform.position -= new Vector3(1.0f, 0.0f) * MOVE_SPEED * Time.deltaTime; ;    // 이동

            float pos_x = -GlobalVariable.Resolution_X * 0.01f * 0.5f + _texture.bounds.size.x * 0.5f * this.transform.localScale.x;   // 맵 끝 좌표
            if (pos_x > this.transform.position.x)    // 맵 밖으로 이동시 위치 조정
                this.transform.position = new Vector3(pos_x, this.transform.position.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);    // 회전

            this.transform.position += new Vector3(1.0f, 0.0f) * MOVE_SPEED * Time.deltaTime;

            float pos_x = GlobalVariable.Resolution_X * 0.01f * 0.5f - _texture.bounds.size.x * 0.5f * this.transform.localScale.x;   // 맵 끝 좌표
            if (pos_x < this.transform.position.x)    // 맵 밖으로 이동시 위치 조정
                this.transform.position = new Vector3(pos_x, this.transform.position.y);
        }
    }
    public void InputCommand()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SkillManager.Instance.InputCommad(KeyCode.UpArrow);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SkillManager.Instance.InputCommad(KeyCode.DownArrow);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SkillManager.Instance.InputCommad(KeyCode.LeftArrow);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SkillManager.Instance.InputCommad(KeyCode.RightArrow);
        }
    }
    public void InputAction()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Button_Dash();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Button_Attack();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Button_Jump();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Button_Postion();
        }
    }
    public void Heal(int value)
    {
        if (StateManager.Instance.MaxHP < StateManager.Instance.HP + value)
            value = StateManager.Instance.MaxHP - StateManager.Instance.HP;

        StateManager.Instance.HP += value;

        _hpGauge.ChangeValue(value);
    }
    public void Damage(int damage)
    {
        _animator.SetTrigger("Damage");
        StateManager.Instance.HP -= damage;
        _hpGauge.ChangeValue(-damage);
    }
    public void Dead()
    {
        _animator.SetTrigger("Dead");
        GameManager.Instance.PlayerDead();
        _hitBox.enabled = false;

    }
    public void LevelUp()
    {
        _hpGauge.SetMaxValue(StateManager.Instance.MaxHP);
    }

    public void Button_Postion()
    {
        if (StateManager.Instance.Postion > 0 && StateManager.Instance.MaxHP * 0.8 >= StateManager.Instance.HP)
        {
            StateManager.Instance.Postion--;
            UIManager.Instance.SetPostion(StateManager.Instance.Postion);
            Heal((int)(StateManager.Instance.MaxHP * 0.2f));
        }

    }
    public void Button_Dash()
    {
        if (IsActive || !GameManager.Instance.GetIsInGame() || !_coolTime[SkillCoolTime.Dash])
            return;

        if (_isJump)
            return;
        // 대쉬
        StartCoroutine(Dash(DASH, DASH_SPEED, false));
        _effectAnimator.SetTrigger("Dash");
        StartCoroutine(CoolTime(SkillCoolTime.Dash));
    }
    public void Button_Attack()
    {
        if (IsActive || !GameManager.Instance.GetIsInGame() || _isJump)
            return;

        // 공격   
        StartCoroutine(Attack());
        _animator.SetTrigger("Attack");
        _effectAnimator.SetTrigger("Basic_Attack");
    }
    public void Button_Jump()
    {
        if (IsActive || !GameManager.Instance.GetIsInGame())
            return;

        // 점프
        if (!_isJump)
        {
            _animator.SetTrigger("Jump");
            _isJump = true;
        }
    }
    public void Button_Skill_A()
    {
        if (IsActive || !GameManager.Instance.GetIsInGame() || IsSkill || !_coolTime[SkillCoolTime.Skill_A])
            return;

        IsSkill = true;
        SkillManager.Instance.SetSkill(KeyCode.A);
        StartCoroutine(CoolTime(SkillCoolTime.Skill_A));
    }
    public void Button_Skill_S()
    {
        if (IsActive || !GameManager.Instance.GetIsInGame() || IsSkill || !_coolTime[SkillCoolTime.Skill_S])
            return;

        IsSkill = true;
        SkillManager.Instance.SetSkill(KeyCode.S);
        StartCoroutine(CoolTime(SkillCoolTime.Skill_S));
    }
    public void Button_Skill_D()
    {
        if (IsActive || !GameManager.Instance.GetIsInGame() || IsSkill || !_coolTime[SkillCoolTime.Skill_D])
            return;

        IsSkill = true;
        SkillManager.Instance.SetSkill(KeyCode.D);
        StartCoroutine(CoolTime(SkillCoolTime.Skill_D));
    }
    public void Button_Skill_F()
    {
        if (IsActive || !GameManager.Instance.GetIsInGame() || IsSkill || !_coolTime[SkillCoolTime.Skill_F])
            return;

        IsSkill = true;
        SkillManager.Instance.SetSkill(KeyCode.F);
        StartCoroutine(CoolTime(SkillCoolTime.Skill_F));
    }

    public void SkillSetting()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Button_Skill_A();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Button_Skill_S();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Button_Skill_D();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            Button_Skill_F();
        }
    }
    public void ActiveSkill(PlayerAttackType type)
    {
        _animator.SetTrigger(type.ToString());
        _effectAnimator.SetTrigger(type.ToString());

        Vector2 pos = this.transform.position;
        RaycastHit2D[] hit;
        switch (type)
        {
            case PlayerAttackType.Skill_A:
                if (this.transform.rotation.y == 0)
                    pos += new Vector2(1.18f, 0.0f);
                else if (this.transform.rotation.y == 1)
                    pos -= new Vector2(1.18f, 0.0f);
                hit = Physics2D.BoxCastAll(pos, new Vector2(2.1f, 1.3f), 0.0f, Vector2.zero, 10.0f, 1 << LayerMask.NameToLayer("Monster"));
                break;
            case PlayerAttackType.Skill_S:
                if (this.transform.rotation.y == 0)
                    pos += new Vector2(-0.3f, 0.1f);
                else if (this.transform.rotation.y == 1)
                    pos -= new Vector2(-0.3f, 0.1f);
                hit = Physics2D.BoxCastAll(pos, new Vector2(3.5f, 3.3f), 0.0f, Vector2.zero, 10.0f, 1 << LayerMask.NameToLayer("Monster"));
                break;
            case PlayerAttackType.Skill_D:
                if (this.transform.rotation.y == 0)
                {
                    pos += new Vector2(1.85f, 0.0f);
                    Instantiate(Resources.Load("Prefabs/Skill_D_Effect"), this.transform.position + new Vector3(-0.35f, 0.0f), Quaternion.identity);
                }
                else if (this.transform.rotation.y == 1)
                {
                    pos -= new Vector2(1.85f, 0.0f);
                    Instantiate(Resources.Load("Prefabs/Skill_D_Effect"), this.transform.position + new Vector3(-0.35f, 0.0f), Quaternion.Euler(0.0f, 180.0f, 0.0f));
                }
                hit = Physics2D.BoxCastAll(pos, new Vector2(3.0f, 1.5f), 0.0f, Vector2.zero, 10.0f, 1 << LayerMask.NameToLayer("Monster"));
                StartCoroutine(Dash(3.5f,15.0f, true));
                break;
            case PlayerAttackType.Skill_F:
                hit = Physics2D.BoxCastAll(pos, new Vector2(12.8f, 7.2f), 0.0f, Vector2.zero, 10.0f, 1 << LayerMask.NameToLayer("Monster"));
                Instantiate(Resources.Load("Prefabs/Skill_F_BG"));
                break;
            default:
                hit = null;
                break;
                // -0.35 //3.5
        }

        if (hit != null)
        {
            for (int index = 0; index < hit.Length; index++)
            {
                hit[index].transform.GetComponent<Monster>().Damage((int)(StateManager.Instance.Atk * Skill_Magnification), type);
            }
        }
    }
    IEnumerator Dash(float distance, float speed, bool skill)
    {
        if (!skill)
            IsActive = true;
        _hitBox.enabled = false;    // 무적
        float _distance = 0.0f;
        if (this.transform.rotation.y == 0)
        {
            float pos_x = GlobalVariable.Resolution_X * 0.01f * 0.5f - _texture.bounds.size.x * 0.5f * this.transform.localScale.x;   // 맵 끝 좌표
            while (_distance <= distance)
            {
                _distance += speed * Time.deltaTime;
                this.transform.position += new Vector3(speed, 0.0f) * Time.deltaTime;
                if (pos_x < this.transform.position.x)    // 맵 밖으로 이동시 위치 조정
                {
                    this.transform.position = new Vector3(pos_x, this.transform.position.y);
                    break;
                }

                yield return new WaitForFixedUpdate();
            }
        }
        else if (this.transform.rotation.y == 1)
        {
            float pos_x = -GlobalVariable.Resolution_X * 0.01f * 0.5f + _texture.bounds.size.x * 0.5f * this.transform.localScale.x;   // 맵 끝 좌표
            while (_distance <= distance)
            {
                _distance += speed * Time.deltaTime;
                this.transform.position -= new Vector3(speed, 0.0f) * Time.deltaTime;
                if (pos_x > this.transform.position.x)    // 맵 밖으로 이동시 위치 조정
                {
                    this.transform.position = new Vector3(pos_x, this.transform.position.y);
                    break;
                }

                yield return new WaitForFixedUpdate();
            }
        }
        _hitBox.enabled = true;    // 무적 헤제
        if (!skill)
            IsActive = false;
    }
    IEnumerator Attack()
    {
        IsActive = true;

        Vector2 pos = this.transform.position;

        if (this.transform.rotation.y == 0)
        {
            // ->
            // 0.3 위치 , 1.35, 1.5 범위
            pos += new Vector2(0.3f, 0.0f);
        }
        else if (this.transform.rotation.y == 1)
        {
            // <-
            pos -= new Vector2(0.3f, 0.0f);
        }

        for (int count = 0; count < 3; count++)
        {
            RaycastHit2D[] hit = Physics2D.BoxCastAll(pos, new Vector2(1.35f, 1.5f), 0.0f, Vector2.zero, 10.0f, 1 << LayerMask.NameToLayer("Monster"));

            if (hit.Length != 0)
            {
                for (int index = 0; index < hit.Length; index++)
                {
                    hit[index].transform.GetComponent<Monster>().Damage(StateManager.Instance.Atk, PlayerAttackType.Basic_Attack);
                }
            }
            // 공격 3번
            yield return new WaitForSeconds(0.2f);
        }

        IsActive = false;
    }
    IEnumerator CoolTime(SkillCoolTime skill)
    {
        _coolTime[skill] = false;
        yield return new WaitForSeconds((int)skill);
        _coolTime[skill] = true;
    }
}
