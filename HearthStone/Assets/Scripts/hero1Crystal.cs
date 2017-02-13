using UnityEngine;
using System.Collections;

public class hero1Crystal : MonoBehaviour {
    public int usableNumber = 1;
    public int totalNumber = 1;
    public int maxNumber;
    public UISprite[] crystals;//用图片集数组来存储水晶图片

    private UILabel label;
    // Use this for initialization
    void Awake()
    {

        maxNumber = crystals.Length;
        label = this.GetComponent<UILabel>();
    }
    void UpdateShow() {

        for(int i=totalNumber;i<maxNumber;i++)//总共有totalnumber个水晶已点亮，剩余的水晶还没出现，故设为false
        {
            crystals[i].gameObject.SetActive(false);
        }
        for(int i =0; i < totalNumber; i++)//共有totalnumber个水晶点亮，故设为对应的水晶图片
        {
            crystals[i].gameObject.SetActive(true);
        }

        for(int i=usableNumber;i<totalNumber;i++)//可用水晶数为usablenumber,将已使用的水晶置为暗色。
        {
            crystals[i].spriteName = "TextInlineImages_normal";//暗色图片
        }
        for(int i=0;i<usableNumber;i++)//将所有可用水晶分别置为其序号的图片
        {
            if (i == 9) crystals[i].spriteName = "TextInlineImages_" + (i + 1);
            else crystals[i].spriteName = "TextInlineImages_0" + (i + 1);
        }

        label.text = usableNumber + "/" + totalNumber;//文字显示水晶数
    }

    public void RefreshCrystalNumber()
    {
        if (totalNumber < maxNumber)
        { totalNumber++; }
        usableNumber = totalNumber;
        UpdateShow();
    }

    public bool UseCryStal(int number)
    {
        if (usableNumber >= number)
        {
            usableNumber -= number;
            UpdateShow();
            return true;
        }
        else
        {
            return false;
            
        }


    }
    void Start () {
        GameController._instance.OnNewRound += this.OnNewRound;//脚本控制事件，回合数加一，并且此时是英雄一的回合。
	}
	
	// Update is called once per frame
	void Update () {
        UpdateShow();
	}

    public void OnNewRound(string heroName)
    {
        if(heroName=="hero1")//说明会和转到了hero1
        {
            RefreshCrystalNumber();
        }
    }
}
