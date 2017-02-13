using UnityEngine;
using System.Collections;
//用于存储某单张卡牌信息的脚本，放在card的prefab中，对所有卡牌使用
public class card : MonoBehaviour {
    public int needCrystal;
    public int hp;
    public int attack;
    

    private UISprite sprite;
    private UILabel attackLabel;
    private UILabel hpLabel;

    void Awake()
    {
        //监听图片和标签
        sprite = this.GetComponent<UISprite>();
        //card脚本与label同级，都是card物件的子类，可以调用transform的引用
        attackLabel = transform.Find("attack_label").GetComponent<UILabel>();
        hpLabel = transform.Find("hp_label").GetComponent<UILabel>();
    }

  

    private string CardName
        //尝试get
    {
        get{
            
            return sprite.spriteName;
        }
    }
    public string getspritename()
    {
        return sprite.spriteName;
    }
    

    void OnPress(bool isPressed)
    {
        //调用descard的方法，并调用显示卡牌的方法，当鼠标点击时
        if(isPressed)
        {
            DesCard._instance.ShowCard(CardName);
        }
    }
   
	// Use this for initialization
	public void setDepth(int depth)//设置卡牌深度，防止卡牌点击时的bug
    {
        //设置深度的方法在卡牌生成时会调用
        sprite.depth = depth;
        hpLabel.depth = depth + 1;
        attackLabel.depth = depth + 1;

    }

    public void ResetPos()//更新label位置，保证自适应
    {
        attackLabel.GetComponent<UIAnchor>().enabled = true;
        hpLabel.GetComponent<UIAnchor>().enabled = true;
    }

    public void ResetShow()//更新血量和伤害
    {
        attackLabel.text = attack+"";
        hpLabel.text = hp + "";

    }

    public int getCrystal()
    {
        string spriteName = sprite.spriteName;
        needCrystal = spriteName[5] - '0';
        return needCrystal;
    }
    
    public int initProperty()//初始化属性，包括水晶数量，伤害，血量
    {
        //卡牌名字有规律，可以用字符串数组和index取到各个值
        string spriteName = sprite.spriteName;
        needCrystal = spriteName[5] - '0';
        attack = spriteName[7] - '0';
        hp= spriteName[9] - '0';

        attackLabel.text = attack + "";
        hpLabel.text = hp + "";

        return attack;
       
    }
}
