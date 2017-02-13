using UnityEngine;
using System.Collections;

//负责开始游戏时的动画播放
public class VSShow : MonoBehaviour {

    public static VSShow _instance;
    //单例模式，将该类实例化成一个单独的接口，其他类要调用它时不需要实例化新的对象
    //只需要调用现有的接口即可

    public TweenScale vsTween;
    public TweenPosition hero1Tween;
    public TweenPosition hero2Tween;
    //监听三个动画，在方法启动时开始播放三个动画
    void Awake()
    {
        _instance = this;
        //this.gameObject.SetActive(true);
    }
	// Use this for initialization
	void Start () {
        //Show("hero1","hero2");
	}
	
	// Update is called once per frame
	
    public void Show(string hero1Name,string hero2Name)
    {
        //存储两个英雄名称到游戏配置中，用hero1和hero2两个字符串存储。
        PlayerPrefs.SetString("hero1",hero1Name);
        PlayerPrefs.SetString("hero2",hero2Name);

        //改变两个动画中的英雄图片
        hero1Tween.GetComponent<UISprite>().spriteName = hero1Name;
        hero2Tween.GetComponent<UISprite>().spriteName = hero2Name;

        BlackMask._instance.Show();//直接调用黑幕的显示方法

        //播放三个动画
        vsTween.PlayForward();
        hero1Tween.PlayForward();
        hero2Tween.PlayForward();
    }
}
