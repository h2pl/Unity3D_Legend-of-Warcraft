using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//用于管理手中的卡牌
public class EnemyCard : MonoBehaviour
{

    public GameObject cardPrefab;
    public Transform card01;//用于定位两张卡牌的位置，在手牌中体现，无实际意义。
    public Transform card02;
    // Use this for initialization
    private float xOffset;//横向偏移量
    private List<GameObject> cards = new List<GameObject>();//新建游戏物件容器，存放新的卡牌游戏物体

    private int startDepth = 6;//设置深度，防止卡牌重叠时点击出现bug
    public string[] CardNames;//卡牌名称数组，用于更改sprite名称

    public void AddCard(GameObject cardGo = null)
    {
        GameObject go = null;
        if (cardGo == null)
        {
            //如果该方法没有传递过来卡牌的话，则自己创建（测试使用）
            go = NGUITools.AddChild(this.gameObject, cardPrefab);//将go生成并放入当前游戏物体即mycard，作为子类。
            go.GetComponent<UISprite>().spriteName = CardNames[Random.Range(0, CardNames.Length)];//随机生成一张卡牌并存入go中
        }
        else
        //卡牌已传递过来
        {
            go = cardGo;
            go.transform.parent = this.transform;//手动将go生成，并且使go的父类为当前游戏物体，自己则作为子类。
        }
        //以下为卡牌进入手牌的动画
        go.GetComponent<UISprite>().width = 80;//将原卡牌调小并放入手牌

        go.GetComponent<card>().ResetPos();//调节宽度后，卡牌的自适应会出错，需要调用函数调整回来
        Vector3 toPosition = card01.position + new Vector3(xOffset, 0, 0) * cards.Count;
        //进入手牌后，按照卡牌顺序从左向右排列，间隔相同。
        iTween.MoveTo(go, toPosition, 1f);//播放发牌到手中的动画

        cards.Add(go);//在cards容器中加入go，
        go.GetComponent<card>().setDepth(startDepth + (cards.Count - 1) * 2);
        //找到游戏物体go中的card脚本，并调用它的设置深度方法，递增加二，保证深度相差二
    }

    //卡牌出牌之后，把这个卡牌移出管理，交给fightcard；

    public GameObject RemoveCard()
    {
        int index = Random.Range(0, cards.Count);
        GameObject tmp = cards[index];

        int needCrystal = tmp.GetComponent<card>().needCrystal;
        hero2Crystal hero2Crystal = GameObject.Find("hero2_crystal").GetComponent<hero2Crystal>();
        bool isSuccess = hero2Crystal.UseCryStal(needCrystal);
        while (!isSuccess)
        {
            index = Random.Range(0, cards.Count);
            tmp = cards[index];
            needCrystal = tmp.GetComponent<card>().needCrystal;
            hero2Crystal = GameObject.Find("hero2_crystal").GetComponent<hero2Crystal>();
            isSuccess = hero2Crystal.UseCryStal(needCrystal);
        }

        cards.Remove(cards[index]);
       
        return tmp;
        
    }

    public void loseCard()//当失去一张卡牌时
    {
        
        int index = Random.Range(1, cards.Count);//测试为随机丢弃
       // Destroy(cards[index]);
        //cards.RemoveAt(index);
        UpdateShow();//删除后更新显示，卡牌重新按顺序排列
        
        


    }

    void Start()
    {
        xOffset = card02.position.x - card01.position.x;//得到水平偏移量
        
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddCard();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            loseCard();
        }
    }

    public void UpdateShow()//更新显示函数
    {
        //当手牌发生一些改变时，进行重新排列和显示
        for (int i = 0; i < cards.Count; i++)//按照容器中的顺序排列
        {
            Vector3 toPosition = card01.position + new Vector3(xOffset, 0, 0) * i;
            //水平偏移
            iTween.MoveTo(cards[i], toPosition, 0.5f);
            //发牌动画
            cards[i].GetComponent<card>().setDepth(startDepth + i * 2);
            //重排时也需要设置深度，同发牌时一样。发牌时要设置深度是因为刚开始不需要重排
        }

    }


}
