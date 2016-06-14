using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TrainingManager : Singleton<TrainingManager>
{

    private const int BASIC_COST = 100;
    private const float COST_VALUE = 1.05f;

    public int Atk { set { _atk_Level = value; } get { return _atkTable[_atk_Level]; } }
    public int HP { set { _hp_Level = value; } get { return _hpTable[_hp_Level]; } }
    public int Skill_A { set { _skill_A_Level = value; } get { return _skill_A_Table[_skill_A_Level]; } }
    public int Skill_S { set { _skill_S_Level = value; } get { return _skill_S_Table[_skill_S_Level]; } }
    public int Skill_D { set { _skill_D_Level = value; } get { return _skill_D_Table[_skill_D_Level]; } }
    public int Skill_F { set { _skill_F_Level = value; } get { return _skill_F_Table[_skill_F_Level]; } }

    public int _atk_Level;
    public int _hp_Level;
    public int _skill_A_Level;
    public int _skill_S_Level;
    public int _skill_D_Level;
    public int _skill_F_Level;

    public int[] _atkTable;
    public int[] _hpTable;
    public int[] _skill_A_Table;
    public int[] _skill_S_Table;
    public int[] _skill_D_Table;
    public int[] _skill_F_Table;

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        _atkTable = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 33, 36, 39, 42, 45, 48, 51, 54, 57, 60, 64, 68, 72, 76, 80, 84, 88, 92, 96, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145, 150 };
        _hpTable = new int[] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 120, 140, 160, 180, 200, 220, 240, 260, 280, 300, 330, 360, 390, 420, 450, 480, 510, 540, 570, 600, 640, 680, 720, 760, 800, 840, 880, 920, 960, 1000, 1050, 1100, 1150, 1200, 1250, 1300, 1350, 1400, 1450, 1500 };
        _skill_A_Table = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 33, 36, 39, 42, 45, 48, 51, 54, 57, 60, 64, 68, 72, 76, 80, 84, 88, 92, 96, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145, 150 };
        _skill_S_Table = new int[] { 0, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 23, 26, 29, 32, 35, 38, 41, 44, 47, 50, 54, 58, 62, 66, 70, 74, 78, 82, 86, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140, 146, 152, 158, 164, 170, 176, 182, 188, 194, 200 };
        _skill_D_Table = new int[] { 0, 4, 8, 12, 16, 20, 24, 28, 32, 36, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 96, 102, 108, 114, 120, 126, 132, 138, 144, 150, 157, 164, 171, 178, 185, 192, 199, 206, 213, 220, 226, 232, 238, 244, 250, 256, 262, 268, 274, 280 };
        _skill_F_Table = new int[] { 0, 8, 16, 24, 32, 40, 48, 56, 64, 72, 80, 86, 92, 98, 104, 110, 116, 122, 128, 134, 140, 144, 148, 152, 156, 160, 164, 168, 172, 176, 180, 182, 184, 186, 188, 190, 192, 194, 196, 198, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210 };

        _atk_Level = 0;
        _hp_Level = 0;
        _skill_A_Level = 0;
        _skill_S_Level = 0;
        _skill_D_Level = 0;
        _skill_F_Level = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetCost(int level)
    {
        if (level == 0)
            return 0;
        else
            return (int)(BASIC_COST * Mathf.Pow(COST_VALUE, level - 1));
    }
}
