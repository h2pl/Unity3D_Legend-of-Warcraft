using UnityEngine;
using System.Collections;
//当鼠标点击卡牌后，显示卡牌信息
public class DesCard : MonoBehaviour {

    public static DesCard _instance;
    public float showTime = 2f;

    private UISprite sprite;
    // Use this for initialization
    private float timer = 0;
    private bool isShow = false;

    private UILabel hpLabel;
    private UILabel attackLabel;
    private UILabel destroylabel;

    void Awake()
    {
        //单例模式，其他类可以方便使用该类的方法
        _instance =this;
        sprite  = this.GetComponent<UISprite>();
        //
        //this.gameObject.SetActive(false);
        sprite.alpha = 0;
        hpLabel = this.transform.Find("hp_label").GetComponent<UILabel>();
        attackLabel = this.transform.Find("attack_label").GetComponent<UILabel>();
        destroylabel = this.transform.Find("destroylabel").GetComponent<UILabel>(); 
    }

    public void ShowCard(string cardname)
    {
        this.gameObject.SetActive(true);
        sprite.spriteName = cardname;
        //iTween.FadeTo(this.gameObject, 0, 3f);
       //出现后渐隐的参数初始化
        sprite.alpha = 1;
        isShow = true;
        timer = 0;

        initProperty();
    }
	
    public void destory_info()
    {
        destroylabel.text = "DESTROY THIS CARD";
    }
	
	// Update is called once per frame
	void Update () {
	if(isShow) //显示该卡牌，并进行渐变消失，即渐隐
        {
            timer += Time.deltaTime;
            if(timer>showTime)
            {
                sprite.alpha = 0;
            }
            else
            {
               sprite.alpha=((showTime - timer) /showTime);
            }
        }
	}

    public void initProperty()//初始化属性，包括水晶数量，伤害，血量
    {
        //卡牌名字有规律，可以用字符串数组和index取到各个值
        string spriteName = sprite.spriteName;
       
        int attack = spriteName[7] - '0';
        int hp = spriteName[9] - '0';

        attackLabel.text = attack + "";
        hpLabel.text = hp + "";

    }
}
