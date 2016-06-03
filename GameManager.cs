using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private SkillSystem skillSystem;
    [SerializeField]
    private GameObject Character;
    private bool isSkill;
    private float skillCoolTimeLeft;
    [SerializeField]
    private float[] skillCoolTimeSeconds;

	// Use this for initialization
	void Start () {
        isSkill = false;
        skillCoolTimeLeft = 0.0f;
        skillCoolTimeSeconds = new float[4]; 
	}
	
	// Update is called once per frame
	void Update () {
        ActionSkill();
        CheckCommand();
	}

    public void ActionSkill()
    {
        if (isSkill == true)
            return;
        if(Input.GetKeyDown(KeyCode.A))
        {
            skillSystem.SetSkill(SKILL_TYPE.A);
            isSkill = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            skillSystem.SetSkill(SKILL_TYPE.B);
            isSkill = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            skillSystem.SetSkill(SKILL_TYPE.C);
            isSkill = true;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            skillSystem.SetSkill(SKILL_TYPE.D);
            isSkill = true;
        }
    }

    public void CheckCommand()
    {
        if (isSkill != true)
            return;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (skillSystem.EqualCommand(KeyCode.UpArrow))
            {

            }
            else
            {
                Debug.Log("실패");
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (skillSystem.EqualCommand(KeyCode.DownArrow))
            {
                Debug.Log("성공");
            }
            else
            {
                Debug.Log("실패");
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (skillSystem.EqualCommand(KeyCode.LeftArrow))
            {
                Debug.Log("성공");
            }
            else
            {
                Debug.Log("실패");
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (skillSystem.EqualCommand(KeyCode.RightArrow))
            {
                Debug.Log("성공");
            }
            else
            {
                Debug.Log("실패");
            }
        }
    }
}
