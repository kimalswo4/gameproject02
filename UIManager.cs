using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : Singleton<UIManager> {

    [SerializeField]
    private Text _gold;
    [SerializeField]
    private Text _postion;

    [SerializeField]
    private GameObject _commandBox;
    [SerializeField]
    private Animator _commandText;
    [SerializeField]
    private Image[] _arrowList;
    [SerializeField]
    private Sprite[] _arrowImageList;
    [SerializeField]
    private Sprite[] _arrowSuccessImageList;
    [SerializeField]
    private Sprite[] _arrowFailImageList;
    [SerializeField]
    private Sprite _blankImage;

    private int[] _arrowValueList;

    // Use this for initialization
    void Start () {
        _commandBox.SetActive(false);
        _arrowValueList = new int[_arrowList.Length];
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Initialize()
    {
        _commandBox.SetActive(false);
        System.Array.Clear(_arrowValueList, 0, _arrowValueList.Length); // 값 배열 초기화

    }
    public void SetSkillCommand(int[] list)
    {
        _commandBox.SetActive(true);
        _arrowValueList = list;

        for (int index = 0; index < _arrowList.Length; index++)
        {
            if (index < list.Length)
            {
                _arrowList[index].sprite = _arrowImageList[list[index]];
            }
            else
                _arrowList[index].sprite = _blankImage;
        }
    }
    public void SetInputResult(int index, CommandResult result)
    {
        if (result == CommandResult.Success)
            _arrowList[index].sprite = _arrowSuccessImageList[_arrowValueList[index]];
        else if (result == CommandResult.Fail)
            _arrowList[index].sprite = _arrowFailImageList[_arrowValueList[index]];
    }

    public void SetResultText(string text)
    {
        _commandText.SetTrigger(text);
    }
    public void SetPostion(int postion)
    {
        if (_postion == null)
            _postion = GameObject.Find("Potion_Button").transform.FindChild("Text").GetComponent<Text>();
        _postion.text = postion.ToString();
    }
}
