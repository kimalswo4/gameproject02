using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    public enum GameState
    {
        Tutorial, Lobby, Stage, Ready, Start, Wave1, Wave2, Wave3,InGame, Clear, Fail, Pause
    }

    [SerializeField]
    private Animator _noticeAnimator;

    private GameState _gameState;
    private int _currentWave;
    private bool _isPause;

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this.gameObject);

        _gameState = GameState.Tutorial;
        _isPause = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (_isPause)
            return;

        switch (_gameState)
        {
            case GameState.Tutorial:
                TutorialManager.Instance.TutorialUpdate();
                break;
            case GameState.Lobby:
                break;
            case GameState.Stage:
                break;
            case GameState.Ready:
                MonsterManager.Instance.SetStage(StageManager.Instance.CurrentStage);
                UIManager.Instance.SetPostion(StateManager.Instance.Postion);
                StateManager.Instance.Gold += 0;
                StageManager.Instance.SetBackground(StageManager.Instance.CurrentStage);
                StageManager.Instance.SetBGM(StageManager.Instance.CurrentStage);
                StartCoroutine(ChangeState(1.0f,GameState.Start));
                break;
            case GameState.Start:
                StartCoroutine(ChangeState(1.0f, GameState.Wave1));
                _currentWave = 1;
                break;
            case GameState.Wave1:
            case GameState.Wave2:
            case GameState.Wave3:
                StartCoroutine(ChangeState(2.0f, GameState.InGame));
                break;
            case GameState.Pause:
                break;
            case GameState.InGame:
                InGameUpdate();
                break;
            case GameState.Clear:
                StageManager.Instance.Clear();
                StartCoroutine(ChangeState(3.0f, GameState.Lobby));
                break;
            case GameState.Fail:
                StartCoroutine(ChangeState(3.0f, GameState.Lobby));
                break;
        }
    }

    public void SetWave(int number)
    {
        _noticeAnimator.SetTrigger(string.Format("Wave{0}", number));
    }
    public void SetNotice(GameState state)
    {
        switch (state)
        {
            case GameState.Ready:
            case GameState.Start:
            case GameState.Wave1:
            case GameState.Wave2:
            case GameState.Wave3:
            case GameState.Clear:
            case GameState.Fail:
                _noticeAnimator.SetTrigger(state.ToString());
                break;
        }
    }
    private IEnumerator ChangeState(float time, GameState changingState)
    {
        _isPause = true;

        SetNotice(_gameState);
        yield return new WaitForSeconds(time);
        _gameState = changingState;

        if (_gameState == GameState.Lobby)
            SceneManager.LoadScene("Lobby");
        else if (_gameState == GameState.Stage)
            SceneManager.LoadScene("Stage");
        else if (_gameState == GameState.Ready)
            SceneManager.LoadScene("InGame");

        _isPause = false;
    }
    private void InGameUpdate()
    {
        Player.Instance.PlayerUpdate();
        MonsterManager.Instance.MonsterUpdate();
        MonsterManager.Instance.MonsterManagerUpdate();
        SkillManager.Instance.SkillManagerUpdate();

        if (MonsterManager.Instance.GetIsClear())
        {
            switch (_currentWave)
            {
                case 1:
                    MonsterManager.Instance.Clear();
                    _currentWave++;
                    StartCoroutine(ChangeState(2.0f, GameState.Wave2));
                    break;
                case 2:
                    MonsterManager.Instance.Clear();
                    _currentWave++;
                    StartCoroutine(ChangeState(2.0f, GameState.Wave3));
                    break;
                case 3:
                    StartCoroutine(ChangeState(0.00f, GameState.Clear));
                    break;
            }
        }
    }

    public void PlayerDead()
    {
        StartCoroutine(ChangeState(1.00f, GameState.Fail));
    }
    public bool GetIsInGame()
    {
        if (_gameState == GameState.InGame)
            return true;
        else
            return false;
    }
    public void NextScene(string sceneName)
    {
        if(sceneName == "Lobby")
            StartCoroutine(ChangeState(0.0f, GameState.Lobby));
        else if (sceneName == "Stage")
            StartCoroutine(ChangeState(0.0f, GameState.Stage));
        else if (sceneName == "InGame")
            StartCoroutine(ChangeState(0.0f, GameState.Ready));
    }
}
