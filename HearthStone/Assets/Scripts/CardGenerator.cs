using UnityEngine;
using System.Collections;

//负责生成卡牌的脚本，放在gamecontroller中       
public class CardGenerator : MonoBehaviour {

    public GameObject cardPrefab;
    public Transform fromCard;
    public Transform toCard;//发牌动画，从发牌处到手中
    public string[] cardNames;//定义卡牌名称数组
    public float transformTime = 0.5f;//
    public UISprite nowGenerateCard;//目前生成的卡牌图片
    public int transformspeed = 20;
    private float timer = 0;
    private bool isTransforming = false;//标识符
    //负责随机动画播放，生成卡牌；
    public GameObject RandomGenerateCard()//持有生成卡牌的引用
    {
        GameObject go = NGUITools.AddChild(this.gameObject, cardPrefab);//生成一张卡牌，并播放发牌动画
        go.transform.position = fromCard.position;//出现在起初位置
        nowGenerateCard = go.GetComponent<UISprite>();//得到该卡牌的ui，方便后面确定卡牌时使用
        iTween.MoveTo(go, toCard.position, 1f);//移动到出牌处动画
        isTransforming = true;
        return go;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (isTransforming)
        {
            timer += Time.deltaTime;//计时器每帧
            int index = (int)(timer / (1f / transformspeed));
            index %= cardNames.Length;
            nowGenerateCard.spriteName = cardNames[index];//此处决定了播放时的卡牌图片
            if (timer > transformTime)
            {
                //变换结束
                //随机生成一个卡牌名字
                string cardName = cardNames[Random.Range(0, cardNames.Length)];
                nowGenerateCard.spriteName = cardName;//随机生成一张卡片
                nowGenerateCard.GetComponent<card>().initProperty();//生成该卡牌时进行属性初始化，将该卡牌的属性修改
                timer = 0;
                isTransforming = false;
            }

        }
    }
}
