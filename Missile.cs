using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

    private int _atk;
    private float _speed;
    private Vector3 _targetDirection;

	// Use this for initialization
	void Awake () {

    }
	
	// Update is called once per frame
	void Update () {
        if (GlobalVariable.Resolution_X * 0.5f < this.transform.position.x ||
            -GlobalVariable.Resolution_X * 0.5f > this.transform.position.x ||
            GlobalVariable.Resolution_Y * 0.5f < this.transform.position.y ||
            -GlobalVariable.Resolution_Y * 0.5f > this.transform.position.y)
        {
            Destroy(this.gameObject);
        }
        else
            this.transform.position += _targetDirection * _speed * Time.deltaTime;


    }
    public void Initiailze(Vector3 dir, int atk, float speed)
    {
        SetTargetDirection(dir);
        SetAtk(atk);
        SetSpeed(speed);
    }
    public void SetTargetDirection(Vector3 dir)
    {
        this.transform.rotation = Quaternion.Euler(0,0, -Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg + 270);
        _targetDirection = dir.normalized;
    }
    public void SetAtk(int value)
    {
        _atk = value;
    }
    public void SetSpeed(float value)
    {
        _speed = value;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            if (other.CompareTag("Player"))
            {
                Player.Instance.Damage(_atk);
                Destroy(this.gameObject);
            }
        }
    }
}
