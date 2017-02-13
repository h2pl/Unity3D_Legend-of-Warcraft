using UnityEngine;
using System.Collections;

//负责开始游戏时生成黑幕
public class BlackMask : MonoBehaviour {
    public static BlackMask _instance;//单例模式，将该类实例化成一个单独的接口，其他类要调用它时不需要实例化新的对象
    //只需要调用现有的接口即可
     void Awake()
    {
        _instance = this;
        this.gameObject.SetActive(false);//一开始隐藏黑幕
    }
	void Update () {
		}
    //两个公共方法，负责显示和隐藏迷雾
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
    public void Show()
    {
        this.gameObject.SetActive(true);
    }
}
