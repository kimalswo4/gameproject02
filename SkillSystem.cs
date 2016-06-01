using UnityEngine;
using System.Collections;

public enum SKILL_TYPE {None,A,B,C,D};

public class SkillSystem : MonoBehaviour {
    private float CurrentTime;
    private float Timer;

    private Skill_A a_skil;
    private Skill_B b_skil;
    private Skill_C c_skil;
    private Skill_D d_skil;

    private Skill currentSkillType;

	// Use this for initialization
	void Start () {
        a_skil = new Skill_A();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void SetSkill(SKILL_TYPE type)
    {
        switch(type)
        {
            case SKILL_TYPE.None:
                currentSkillType = null;
                return;
            case SKILL_TYPE.A:
                currentSkillType = a_skil;
                break;
            case SKILL_TYPE.B:
                currentSkillType = b_skil;
                break;
            case SKILL_TYPE.C:
                currentSkillType = c_skil;
                break;
            case SKILL_TYPE.D:
                currentSkillType = d_skil;
                break;
        }
        currentSkillType.SetCommandNumber();
        currentSkillType.SetCommand();
        GameObject.Find("UIManager").GetComponent<UIManager>().SetCommandImage(currentSkillType.GetQueue());
    }

    public bool EqualCommand(KeyCode key)
    {
        if(currentSkillType == null)
        {
            Debug.Log("에러");
            return false;
        }
        if (currentSkillType.GetCommand() == key)
            return true;
        else 
            return false;
    }
}
