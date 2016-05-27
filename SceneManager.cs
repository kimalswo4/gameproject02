using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {

    private SpriteRenderer _currentsprite;
    public SpriteRenderer FadeSprite;
    private float _minimum = 0.0f;
    private float _maximum = 1f;
    private float _speed = 1.0f;
    private float _waittoal = 3.0f;
    private float _starttimewait = 5.0f;
    public float _currenttime;
    
    

    public bool faded;
    public bool StartStory;
    public bool InGame;
    public int SpriteChange;
    public int check;

    public int Click;
    public Sprite Intro;
    public Sprite GameMenu;
    public Sprite[] GameStartIntro;
    public GameObject GameStartUI;
    public GameObject GameExplainUI;

	// Use this for initialization
	void Start () {

        faded = false;
        StartStory = false;
        InGame = false;
        _currentsprite = gameObject.GetComponent<SpriteRenderer>();
        SpriteChange = 0;
        check = 0;
    }
	
	// Update is called once per frame
	void Update () {
        FadeOut();
        ImageChange();
        
        
	}

    void FadeOut() //페이드 인아웃 효과
    {
        float step = _speed * Time.deltaTime;


        if (faded)
        {
            FadeSprite.color = new Color(1f, 1f, 1f, Mathf.Lerp(FadeSprite.color.a, _maximum, step));

        }
        else
        {
            FadeSprite.color = new Color(1f, 1f, 1f, Mathf.Lerp(FadeSprite.color.a, _minimum, step));
        }
    }

    void InGameStart()
    {
        if (InGame == true && _currenttime >= _waittoal && faded == false && SpriteChange == 20)
        {
            _currenttime = 0f;
            _currentsprite.sprite = GameStartIntro[12];               
            
                    
        }
    }

    void ImageChange() //BackGround이미지 교체
    {
        if (StartStory == false && InGame == false)
        {
            _currenttime += Time.deltaTime;
            if(_currenttime >= _waittoal && faded == false && SpriteChange == 0)
            {
                faded = true;
                _currenttime = 0f;
            }
            if (_currenttime >= _waittoal && faded == true && SpriteChange == 0)
            {
                GameStartUI.SetActive(true);
                _currentsprite.sprite = GameMenu;
                faded = false;
                _currenttime = 0f;
                SpriteChange++;
            }
        }
        
        if (StartStory == true && InGame == false)
        {
            if (SpriteChange == 1)
            { 
                faded = true;
                SpriteChange++;
            }
            _currenttime += Time.deltaTime;
            if (_currenttime >= _starttimewait && faded == true && SpriteChange == 2)
            {
                _currentsprite.sprite = GameStartIntro[0];
                faded = false;
                _currenttime = 0f;
                SpriteChange++;
            }
            if (_currenttime >= _starttimewait && faded == false && SpriteChange == 3)
            {
                faded = true;
                _currenttime = 0f;
                SpriteChange++;
            }
            if (_currenttime >= _starttimewait && faded == true && SpriteChange == 4)
            {
                _currentsprite.sprite = GameStartIntro[1];
                faded = false;
                _currenttime = 0f;
                SpriteChange++;
            }
            if (_currenttime >= _starttimewait && faded == false && SpriteChange == 5)
            {
                faded = true;
                _currenttime = 0f;
                SpriteChange++;
            }
            if (_currenttime >= _starttimewait && faded == true && SpriteChange == 6)
            {
                _currentsprite.sprite = GameStartIntro[2];
                faded = false;
                _currenttime = 0f;
                SpriteChange++;
            }
            if (_currenttime >= _starttimewait && faded == false && SpriteChange == 7)
            {
                faded = true;
                _currenttime = 0f;
                SpriteChange++;
            }
            if (_currenttime >= _starttimewait && faded == true && SpriteChange == 8)
            {
                _currentsprite.sprite = GameStartIntro[3];
                faded = false;
                _currenttime = 0f;
                SpriteChange++;
            }
            if (_currenttime >= _starttimewait && faded == false && SpriteChange == 9)
            {
                faded = true;
                _currenttime = 0f;
                SpriteChange++;
            }
            if (_currenttime >= _starttimewait && faded == true && SpriteChange == 10)
            {
                _currentsprite.sprite = GameStartIntro[4];
                faded = false;
                _currenttime = 0f;
                SpriteChange++;
            }
            if (_currenttime >= _starttimewait && faded == false && SpriteChange == 11)
            {
                faded = true;
                _currenttime = 0f;
                SpriteChange++;
            }
            if (_currenttime >= _starttimewait && faded == true && SpriteChange == 12)
            {
                _currentsprite.sprite = GameStartIntro[5];
                faded = false;
                _currenttime = 0f;
                SpriteChange++;
            }
            if (_currenttime >= _starttimewait && faded == false && SpriteChange == 13) //설명부분
            {
                _currentsprite.sprite = GameStartIntro[6];
                _currenttime = 0f;
                SpriteChange++;
            }
            if (_currenttime >= _waittoal && faded == false && SpriteChange == 14)
            {
                _currentsprite.sprite = GameStartIntro[7];
                _currenttime = 0f;
                SpriteChange++;
            }
            if (_currenttime >= _waittoal && faded == false && SpriteChange == 15)
            {
                _currentsprite.sprite = GameStartIntro[8];
                _currenttime = 0f;
                SpriteChange++;
            }
            if (_currenttime >= _waittoal && faded == false && SpriteChange == 16)
            {
                _currentsprite.sprite = GameStartIntro[9];
                _currenttime = 0f;
                SpriteChange++;
            }
            if (_currenttime >= _waittoal && faded == false && SpriteChange == 17)
            {
                _currentsprite.sprite = GameStartIntro[10];
                _currenttime = 0f;
                SpriteChange++;
            }
            if (_currenttime >= _waittoal && faded == false && SpriteChange == 18)
            {
                _currentsprite.sprite = GameStartIntro[11];
                _currenttime = 0f;
                SpriteChange++;
            }
            if (_currenttime >= _waittoal && faded == false && SpriteChange == 19)
            {
                _currentsprite.sprite = GameStartIntro[12];
                _currenttime = 0f;
                SpriteChange++;
                InGame = true;
            }        
        }
        
    }
   
    public void GameStart() //GameStartMenu버튼
    {
        _currenttime = 0;
        StartStory = true;
        SpriteChange = 1;
        GameStartUI.SetActive(false);
        GameExplainUI.SetActive(true);
    }

    public void GameExplainSkip() //게임설명 스킵버튼
    {
        _currenttime = 0;
        GameExplainUI.SetActive(false);
        faded = false;
        SpriteChange = 20;
        InGame = true;
    }
    
    public void GameEnd() //게임 종료
    {
        Application.Quit();
    }
}
