using UnityEngine;
using System.Collections;

public class StageManager : Singleton<StageManager> {

    private const int MAX_STAGE = 10;

    public int CurrentStage { set; get; }

    private bool[] _isClear;
    [SerializeField]
    private Sprite[] _background;
    [SerializeField]
    private AudioClip[] _bgm;
	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this.gameObject);

        _isClear = new bool[MAX_STAGE] { true, false, false, false, false, false, false, false, false, false };
    }
	
    public void Clear()
    {
        _isClear[CurrentStage] = true;
    }
    public bool GetIsClear(int stage)
    {
        return _isClear[stage];
    }
    public void SetStageSellect()
    {
        for (int index = 0; index < MAX_STAGE; index++)
        {
            if (_isClear[index] == false)
                return;
            else
            {
                GameObject.Find(string.Format("Stage {0}", index+1)).transform.FindChild("Lock").gameObject.SetActive(false);
            }
        }
    }
    public void SetBackground(int stage)
    {
        GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = _background[stage-1];
    }
    public void SetBGM(int stage)
    {
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = _bgm[stage];
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
    }
}
