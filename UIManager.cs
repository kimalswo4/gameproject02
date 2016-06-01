using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {
    [SerializeField]
    private Image[] commandImage;
    [SerializeField]
    private Sprite[] ArrowImages;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetCommandImage(Queue<KeyCode> command)
    {
        int index = 0;
        while(command.Count != 0) //커맨드 이미지 채우기
        {
            switch (command.Dequeue())
            {
                case KeyCode.UpArrow:
                    commandImage[index].sprite = ArrowImages[0];
                    break;
                case KeyCode.DownArrow:
                    commandImage[index].sprite = ArrowImages[1];
                    break;
                case KeyCode.RightArrow:
                    commandImage[index].sprite = ArrowImages[2];
                    break;
                case KeyCode.LeftArrow:
                    commandImage[index].sprite = ArrowImages[3];
                    break;
            }
            index++;
        }
        while(index < commandImage.Length) //커맨드 이미지 남은거 빼는거
        {
            commandImage[index].sprite = ArrowImages[4];
            index++;
        }
    }
}
