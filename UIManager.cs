using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {
    [SerializeField]
    private Image[] commandImage;
    [SerializeField]
    private Sprite[] ArrowImages;
    [SerializeField]
    private Sprite[] SuccessImages;
    [SerializeField]
    private Sprite[] FailImages;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetCommandImage(KeyCode[] command)
    {
        int index = 0;
        for (index = 0; index < command.Length; index++) //커맨드 이미지 채우기
        {
            switch (command[index])
            {
                case KeyCode.UpArrow:
                    commandImage[index].sprite = ArrowImages[0];
                    break;
                case KeyCode.DownArrow:
                    commandImage[index].sprite = ArrowImages[1];
                    break;
                case KeyCode.LeftArrow:
                    commandImage[index].sprite = ArrowImages[2];
                    break;
                case KeyCode.RightArrow:
                    commandImage[index].sprite = ArrowImages[3];
                    break;
            }
        }
        while(index < commandImage.Length) //커맨드 이미지 남은거 빼는거
        {
            commandImage[index].sprite = ArrowImages[4];
            index++;
        }
    }
}
