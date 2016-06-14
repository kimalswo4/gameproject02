using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject Character_info;
    public GameObject Shop_info;
    public GameObject Train_info;

    //캐릭터정보창띄우고 닫기
    public void CharacterClick()
    {
        Character_info.SetActive(true);
    }
    public void CharacterInfoOut()
    {
        Character_info.SetActive(false);
    }
    //상점 정보창 띄우고 닫기
    public void ShopClick()
    {
        Shop_info.SetActive(true);
    }
    public void ShopInfoOut()
    {
        Shop_info.SetActive(false);
    }
    //훈련 정보창 띄우고 닫기
    public void TrainClick()
    {
        Train_info.SetActive(true);
    }
    public void TrainInfoOut()
    {
        Train_info.SetActive(false);
    }
    //게임 시작버튼
    public void GameStart()
    {
        GameManager.Instance.NextScene("Stage");
    }
    //X종료버튼 누르기
    public void GameExit()
    {
        Application.Quit();
    }
}
