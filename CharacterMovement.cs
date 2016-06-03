using UnityEngine;
using System.Collections;

public class CharacterMovement : PlayerState
{

    private Rigidbody2D _playerRigidBody2D;
    private bool _facingRight; //플레이어 바라보는 방향 판정
    private bool _grounded = false;
    private float _attackCoolTime;
    private float _activeFalse;
    [SerializeField]
    private GameObject AttackRange;
    
    public LayerMask Ground;

    //컴포넌트 참조 초기화
    void Awake()
    {
        _playerRigidBody2D = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckGround();
        Attack();
    }

    void Move()
    {
        float movePlayerVector = Input.GetAxis("Horizontal");

        _playerRigidBody2D.velocity = new Vector2(movePlayerVector * Speed, _playerRigidBody2D.velocity.y);

        if (movePlayerVector > 0 && _facingRight)
        {
            Flip();
        }
        else if (movePlayerVector < 0 && !_facingRight)
        {
            Flip();
        }

        if(Input.GetKeyDown(KeyCode.C) && _grounded)
        {
            _playerRigidBody2D.velocity = new Vector2(0, Jump);
        }
    }

    void Flip()
    {
        //플레이어가 바라보는 방향교체
        _facingRight = !_facingRight;
        //플레이어의 x스케일에 -1을 곱함
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void CheckGround()
    {
        Debug.DrawRay(transform.position, Vector2.down * 1.45f, Color.red);

        if (Physics2D.Raycast(transform.position, Vector2.down, 1.45f,Ground))
        {
                _grounded = true;
                return;
        }
        _grounded = false;
    }

    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {

        }
    }
}
