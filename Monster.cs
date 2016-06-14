using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Monster : MonoBehaviour {

    public enum AttackType
    {
        Direct, Standoff
    }

    private delegate void Action();
    private Action _action;
    private Animator _animator;
    private Animator _effectAnimator;
    private float _delayTime;

    [SerializeField]
    private AttackType _attackType;
    [SerializeField]
    private int _hp;
    [SerializeField]
    private int _atk;
    [SerializeField]
    private int _exp;
    [SerializeField]
    private int _gold;
    [SerializeField]
    private float _attackDelay;
    [SerializeField]
    private float _attackSpeed;
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _floor;

    [SerializeField]
    private Vector2 _attackPos;
    [SerializeField]
    private Vector2 _attackScale;

    private bool _isAttack;

    // Use this for initialization
    void Start () {
        _animator = this.GetComponent<Animator>();
        _effectAnimator = transform.FindChild("MonsterEffect").GetComponent<Animator>();
        _delayTime = 0.0f;
        _isAttack = false;
    }

    // Update is called once per frame
    public void MonsterUpdate() {
        if (_attackRange <= GetDistanceFromPlayer())
        {
            _delayTime = _attackDelay;
            _action = new Action(Move);
            _animator.SetBool("IsMove", true);
        }
        else
        {
            _action = new Action(Attack);
        }

        if (_isAttack != true)
            _action();
    }

    public Monster Create(float x)
    {
        this.transform.position = new Vector3(x, _floor);
        Instantiate(Resources.Load("Prefabs/Monster_Create"), this.transform.position, Quaternion.identity);

        return this.GetComponent<Monster>();
    }
    private void Move()
    {
        if (this.transform.position.x - Player.Instance.transform.position.x >= 0)
        {
            this.transform.position += Vector3.left * _moveSpeed * Time.deltaTime;
            this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
        }
        else
        {
            this.transform.position += Vector3.right * _moveSpeed * Time.deltaTime;
            this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
        }
    }

    protected virtual void Attack()
    {
        if (_delayTime >= _attackDelay)
        {
            _delayTime = 0.0f;
            _animator.SetTrigger("Attack");
        }
        else
        {
            _delayTime += Time.deltaTime;
        }
    }

    private float GetDistanceFromPlayer()
    {
        return Vector3.Distance(this.transform.position, Player.Instance.transform.position); 
    }
    public int GetGold()
    {
        return _gold;
    }
    public int GetExp()
    {
        return _exp;
    }
    public void Damage(int damage, PlayerAttackType type)
    {
        _hp -= damage;

        if (type != PlayerAttackType.Skill_F)
            _effectAnimator.SetTrigger(type.ToString() + "_Hit");

        if (_hp <= 0)
            MonsterManager.Instance.Destroy(this);
    }
    private void AttackEvent()
    {
        if (_attackType == AttackType.Direct)
        {
            Vector2 pos = this.transform.position;
            if (this.transform.rotation.y == 0)
            {
                pos += _attackPos;
            }
            else if (this.transform.rotation.y == 1)
            {
                pos -= _attackPos;
            }

            RaycastHit2D[] hit = Physics2D.BoxCastAll(pos, _attackScale, 0.0f, Vector2.zero, 10.0f, 1 << LayerMask.NameToLayer("Player"));

            if (hit.Length != 0)
            {
                for (int index = 0; index < hit.Length; index++)
                {
                    hit[index].transform.GetComponent<Player>().Damage(_atk);
                }
            }
        }
        else if (_attackType == AttackType.Standoff)
        {
            Vector3 dir = Player.Instance.transform.position - this.transform.position;
            (Instantiate(Resources.Load("Prefabs/Missile"), this.transform.position, Quaternion.identity) as GameObject).GetComponent<Missile>().Initiailze(dir, _atk, _attackSpeed); 
        }
    }

    public void OnAttack( )
    {
        _isAttack = true;
    }
    public void OffAttack()
    {
        _isAttack = false;
    }
}
