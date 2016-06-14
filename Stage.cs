using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour {

    private bool _isSellect;
    [SerializeField]
    private GameObject[] _stageInfo;

	// Use this for initialization
	void Start () {
        StageManager.Instance.SetStageSellect();
        _isSellect = false;
    }

    public void StageExit()
    {
        GameManager.Instance.NextScene("Lobby");
    }
    public void OnStageInfo(int stage)
    {
        if (_isSellect == true)
            return;

        _isSellect = true;

        _stageInfo[stage - 1].SetActive(true);
    }
    public void StageSellect(int stage)
    {
        if (StageManager.Instance.GetIsClear(stage - 1))
        {
            GameManager.Instance.NextScene("InGame");
            StageManager.Instance.CurrentStage = stage;
        }
    }
}
