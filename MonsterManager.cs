using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class MonsterManager : Singleton<MonsterManager> {

    private const float LEFT_CREATE_POSTION_X = -5.0f;
    private const float RIGHT_CREATE_POSTION_X = 5.0f;

    private Queue<string> _directionList;
    private Queue<string> _monsterList;
    private Queue<float> _timeList;

    private List<Monster> _currentMonster;

    private int[] _waveCount;
    private int _currentWaveNumber;
    private int _currentWaveCount;
    private float _currentMonsterTime;
    private float _time;
    private bool _isStop;

    // Use this for initialization
    void Start () {
        _directionList = new Queue<string>();
        _monsterList = new Queue<string>();
        _timeList = new Queue<float>();

        _currentMonster = new List<Monster>();

        _isStop = false;
        _waveCount = new int[3];
        _currentWaveNumber = 0;
        _time = 0.0f;
        _currentMonsterTime = 0.0f;
    }

    // Update is called once per frame
    public void MonsterManagerUpdate ()
    {
        if (_isStop || _monsterList.Count == 0 || _directionList.Count == 0)
            return;

        _time += Time.deltaTime;
        if (_time >= _currentMonsterTime)
        {
            CreateMonster(_monsterList.Dequeue(), _directionList.Dequeue());
            if (++_currentWaveCount >= _waveCount[_currentWaveNumber])
            {
                _isStop = true;
                _currentWaveNumber++;
                return;
            }

            _currentMonsterTime = _timeList.Dequeue();
        }
    }

    public void MonsterUpdate()
    {
        for (int index = 0; index < _currentMonster.Count; index++)
        {
            _currentMonster[index].MonsterUpdate();
        }
    }

    public void Initialzie()
    {
        _directionList.Clear();
        _timeList.Clear();
        _monsterList.Clear();

        _waveCount = new int[3];
        _currentWaveNumber = 0;
        _time = 0.0f;
        _currentMonsterTime = 0.0f;
    }
    public void ReStart(bool value)
    {
        _isStop = value;
        _currentMonsterTime = _timeList.Dequeue();
    }
    public void ReadData(int stageNumber)
    {
        FileStream fs = new FileStream(string.Format("Assets/Resources/Data/Stage{0}.txt", stageNumber), FileMode.Open);
        StreamReader sr = new StreamReader(fs);
        string value;
        string[] data;
        int waveNumber = 0;
        while (sr.Peek() > -1)
        {
            value = sr.ReadLine();
            if (value == "Wave1" || value == "Wave2" || value == "Wave3")
            {
                waveNumber++;
                continue;
            }
            else
            {
                _waveCount[waveNumber-1] += 1;
                data = value.Split('\t');

                _monsterList.Enqueue(data[0]);
                _directionList.Enqueue(data[1]);
                _timeList.Enqueue(float.Parse(data[2]));
            }
        }

        sr.Close();
        fs.Close();
    }
    public void CreateMonster(string name, string direction)
    {
        float pos = 0.0f;
        if (direction == "L")
            pos = LEFT_CREATE_POSTION_X;
        else if (direction == "R")
            pos = RIGHT_CREATE_POSTION_X;

        _currentMonster.Add(Instantiate(Resources.Load("Prefabs/" + name) as GameObject).GetComponent<Monster>().Create(pos));
    }
    public void Destroy(Monster monster)
    {
        StateManager.Instance.Exp += monster.GetExp();
        StateManager.Instance.Gold += monster.GetGold();
        _currentMonster.Remove(monster);
        Destroy(monster.gameObject);
    }

    public bool GetIsClear()
    {
        if (_currentMonster.Count == 0 && _isStop == true)
            return true;
        else
            return false;
    }

    public void SetStage(int stage)
    {
        ReadData(stage);
        _currentMonsterTime = _timeList.Dequeue();
    }

    public void Clear()
    {
        _isStop = false;
        _time = 0.0f;
        _currentWaveCount = 0;
        _currentMonsterTime = _timeList.Dequeue();
    }
}
