using UnityEngine;
using System.Collections;
//与水晶槽1显示方法一样，代码基本相同
public class hero2Crystal : MonoBehaviour {
    public int usableNumber = 1;
    public int totalNumber = 1;
    public int maxNumber=10;
    public UISprite[] crystals;

    private UILabel label;

    // Use this for initialization
    void Awake()
    {

        maxNumber = crystals.Length;
        label = this.GetComponent<UILabel>();
    }

    void Start()
    {
        GameController._instance.OnNewRound += this.OnNewRound;
    }
    void UpdateShow() {

        for(int i=totalNumber;i<maxNumber;i++)
        {
            crystals[i].gameObject.SetActive(false);
        }
        for(int i =0; i < totalNumber; i++)
        {
            crystals[i].gameObject.SetActive(true);
        }

        for(int i=usableNumber;i<totalNumber;i++)
        {
            crystals[i].spriteName = "TextInlineImages_normal";
        }
        for(int i=0;i<usableNumber;i++)
        {
            if (i == 9) crystals[i].spriteName = "TextInlineImages_" + (i + 1);
            else crystals[i].spriteName = "TextInlineImages_0" + (i + 1);
        }

        label.text = usableNumber + "/" + totalNumber;
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
 
	
	// Update is called once per frame
	void Update () {
        UpdateShow();
	}

    public void OnNewRound(string heroName)
    {
        if (heroName == "hero2")//说明会和转到了hero1
        {
        //    int needCrystal = this.GetComponent<card>().needCrystal;
           // hero2Crystal hero2Crystal = GameObject.Find("hero2_crystal").GetComponent<hero2Crystal>();
            //bool isSuccess = hero2Crystal.UseCryStal(needCrystal);
            //如果够可以出牌,调用mycard里的删除方法，将手牌中的牌移动到战斗区域中
            //if (isSuccess)
            // {
            // this.transform.parent.GetComponent<myCard>().RemoveCard(this.gameObject);//脚本的父类是card_01，它的父类又是mycard，找到mycard中的脚本mycard，使用其方法
            // surface.GetComponent<FightCard>().AddCard(this.gameObject);//脚本存放位置即战斗区域，取到脚本fightcard，使用方法添加一张卡片。
            // }
            //GameObject cardGo = GameObject.Find("GameController").GetComponent<CardGenerator>().RandomGenerateCard();
            //GameObject.Find("EnemyCard").GetComponent<EnemyCard>().RemoveCard(cardGo);
            //GameObject.Find("EnemyArea").GetComponent<FightCard>().AddCard(cardGo);
            
               GameObject.Find("EnemyCard").GetComponent<EnemyCard>().UpdateShow();
            
            if (GameController._instance.roundIndex>=2)
            { RefreshCrystalNumber(); }
            
        }
    }
}
