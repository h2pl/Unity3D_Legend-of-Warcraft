using UnityEngine;
using System.Collections;
//结束按钮
public class EndButton : MonoBehaviour {

    private UILabel label;
    void Awake()
    {
        label = transform.Find("label").GetComponent<UILabel>();//监听
    }
    public void onEndButtonClick()//更改文字
    {
        if (label.text == "结束回合")
        {
            label.text = "对方回合";
            GameController._instance.TransformPlayer();//点击回合结束，转变游戏角色控制方为对方。调用gamecontroller里的转换角色脚本
        }
    }
	// Use this for initialization
	void Start () {
        GameController._instance.OnNewRound += this.OnNewRound;//脚本控制事件，回合数加一，并且此时是英雄一的回合，文字显示为结束回合。
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnNewRound(string heroName)
    {
        if (heroName == "hero1")
        {
            label.text = "结束回合";
        }
      
    }
}
