using UnityEngine;
using System.Collections;

//负责记录英雄血量信息

public class hero : MonoBehaviour
{
    public int maxHp = 30;
    public int minHp = 0;
    protected UISprite sprite;
    private UILabel hpLabel;
    public int hpCount = 30;
    

    void Awake()
    {
        
        sprite = this.GetComponent<UISprite>();
        hpLabel = this.transform.Find("hp").GetComponent<UILabel>();
       
    }

    public void TakeDamage(int damage)//受到伤害
    {
        hpCount -= damage;
        hpLabel.text = hpCount + "";

    }
    public void PlusHp(int hp)//恢复生命
    {
        hpCount += hp;
        if (hpCount >= maxHp)
        {
            hpCount = maxHp;
        }

        hpLabel.text = hpCount + "";
    }
    void Update()
    {
        //用于测试
        if (Input.GetKey(KeyCode.E))
        {
            TakeDamage(Random.Range(1, 5));
        }
      
    }

   
    void Start()
    {
        //game_info.SetActive(false);
    }
}
