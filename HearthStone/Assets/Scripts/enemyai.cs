using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class enemyai : MonoBehaviour
{

    public Transform card01;
    public Transform card02;

    private float xOffset = 0;//两张卡片的偏移量
    private List<GameObject> cardList = new List<GameObject>();//卡牌存放在战斗区域使用的容器
                                                               // Use this for initialization
                                                               //用来添加卡牌，用来把卡牌放到战斗区域

    void Start()
    {
        xOffset = card02.position.x - card01.position.x;


    }
    public void AddCard()
    {
      
            GameObject go= GameObject.Find("EnemyCard").GetComponent<EnemyCard>().RemoveCard();
            go.transform.parent = this.transform;//使卡牌的父类为战斗区域，就可以进行控制了。
            cardList.Add(go);//容器中添加对象，方便计算卡牌所放位置
            UpdateShow();

            Vector3 targetPos = CalcPosition();
            iTween.MoveTo(go, targetPos, 0.5f);

        int attack = go.GetComponent<card>().initProperty();
        GameObject.Find("hero1").GetComponent<hero1>().TakeDamage(attack);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            AddCard();
        }
    }
    Vector3 CalcPosition()
    {
        int index = cardList.Count;


        float myxOffset = index * xOffset;
        Vector3 pos = new Vector3(card01.position.x + myxOffset, card01.position.y, card01.position.z);
        return pos;

    }
    public void UpdateShow()//更新显示函数
    {
        //当手牌发生一些改变时，进行重新排列和显示
        for (int i = 0; i < cardList.Count; i++)//按照容器中的顺序排列
        {
            Vector3 toPosition = card01.position + new Vector3(xOffset, 0, 0) * i;
            //水平偏移
            iTween.MoveTo(cardList[i], toPosition, 0.5f);
            //发牌动画

            //重排时也需要设置深度，同发牌时一样。发牌时要设置深度是因为刚开始不需要重排
        }

    }
}

