using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateManager : Singleton<StateManager> {

    private const int MAX_LEVEL = 20;

    public int Gold
    {
        set
        {
            _gold = value;
            if (_goldText == null)
                _goldText = GameObject.Find("Gold").transform.FindChild("Text").GetComponent<Text>();

            _goldText.text = _gold.ToString();
        }
        get
        {
            return _gold;
        }
    }
    public int MaxHP { get { return TrainingManager.Instance.HP + _hpTable[_level - 1]; } }
    public int Atk { get { return TrainingManager.Instance.Atk + _atkTable[_level - 1]; } }
    public int HP { set; get; }
    public int Exp
    {
        set
        {
            if (_level >= MAX_LEVEL)
                return;
            _exp = value;

            if (_exp >= _expTable[_level - 1])
            {
                LevelUp();
            }
        }
        get { return _exp; }
    }
    public int Level { set { _level = Mathf.Clamp(value, 1, MAX_LEVEL); } get { return _level; } }
    public int Postion { set; get; }
    public int MaxExp { get { return _expTable[Level - 1]; } }

    private int _gold;
    private int _level;     // 레벨
    private int _exp;       // 경험치

    private int[] _hpTable;
    private int[] _expTable;
    private int[] _atkTable;

    private Text _goldText;

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this.gameObject);

        _hpTable = new int[] { 500, 520, 545, 575, 610, 650, 695, 745, 800, 860, 930, 1010, 1100, 1200, 1310, 1430, 1560, 1700, 1850, 2050 };
        _atkTable = new int[] { 10, 12, 14, 16, 18, 21, 24, 27, 30, 33, 38, 43, 48, 53, 58, 66, 74, 82, 90, 100 };
        _expTable = new int[] { 100, 200, 250, 350, 400, 500, 700, 900, 1200, 1500, 2000, 2500, 3500, 4000, 5000, 5500, 6100, 6600, 7700, 0 };

        Level = 1;
        Postion = 1;
        _gold = 200;

        HP = _hpTable[Level - 1];
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void LevelUp()
    {
        _exp -= _expTable[_level - 1];
        
        Level++;

        Player.Instance.LevelUp();
        Player.Instance.Heal(MaxHP - HP);
    }
}
