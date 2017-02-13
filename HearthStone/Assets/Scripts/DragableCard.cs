using UnityEngine;
using System.Collections;

public class DragableCard : UIDragDropItem {
    //控制卡牌拖拽的脚本

    protected override void OnDragDropRelease(GameObject surface)//复写父类方法，实现继承，防止出错
    {
        base.OnDragDropRelease(surface);
        if (surface != null && surface.tag == "DragArea")//若将卡牌拖到了正确的位置
        {
            //拖到可发牌区域

            //首先需要的水晶够不够
            
            
            int needCrystal = this.GetComponent<card>().getCrystal();
            hero1Crystal hero1Crystal = GameObject.Find("hero1_crystal").GetComponent<hero1Crystal>();
            bool isSuccess = hero1Crystal.UseCryStal(needCrystal);

            //如果够可以出牌,调用mycard里的删除方法，将手牌中的牌移动到战斗区域中
            if (isSuccess)
            {
                this.transform.parent.GetComponent<myCard>().RemoveCard(this.gameObject);//脚本的父类是card_01，它的父类又是mycard，找到mycard中的脚本mycard，使用其方法
                surface.GetComponent<FightCard>().AddCard(this.gameObject);
                GameObject.Find("myCard").GetComponent<myCard>().UpdateShow();//脚本存放位置即战斗区域，取到脚本fightcard，使用方法添加一张卡片。
            }
            else
            {
                transform.parent.GetComponent<myCard>().UpdateShow();
                transform.parent.GetComponent<FightCard>().UpdateShow();
            }
            }

            else if (surface != null && surface.tag == "EnemyArea")
        {
            int attack = this.GetComponent<card>().initProperty();
            
            GameObject.Find("hero2").GetComponent<hero2>().TakeDamage(attack);
            transform.parent.GetComponent<FightCard>().UpdateShow();
            transform.parent.GetComponent<myCard>().UpdateShow();
            
        }
        else
        //若卡牌拖放错误，则
        //将卡牌重新排列
        {
            transform.parent.GetComponent<myCard>().UpdateShow();
            transform.parent.GetComponent<FightCard>().UpdateShow();

        }
        //若卡牌拖放错误，则
        //将卡牌重新排列

    }
    // Use this for initialization
    
}
