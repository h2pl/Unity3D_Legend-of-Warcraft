using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//用于显示使用过的卡牌
public class historyCard : MonoBehaviour {

    public Transform incard;//transform用于记录位置
    public Transform outcard;//用于记录发牌和显示的位置，没有实际意义
    public Transform card1;
    public Transform card2;//用于显示历史卡牌的垂直排列，用于记录位置。
    public GameObject cardPrefab;//载入卡牌游戏物体

    private List<GameObject> cardlist = new List<GameObject>();//做一个卡牌容器，用于存储历史卡牌
    private float yOffset;//垂直偏移量
    



    void Start()//不能放在awake中，否则会出现bug
    {
        yOffset = card2.position.y - card1.position.y;

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(AddCard());
        }
    }
    public IEnumerator AddCard()
    {
        //生成卡牌，出现在显示位置，
        GameObject go = GameObject.Instantiate(cardPrefab, incard.position, Quaternion.identity) as GameObject;
        yield return 0;
        go.transform.position = incard.position;
        iTween.MoveTo(go, card1.position, 1f);//异步执行

        cardlist.Add(go);
        //remove index at 0
        if(cardlist.Count>7)//超过七张则放不下
        {
            iTween.MoveTo(cardlist[0], outcard.position, 1f);//将最后一张移出
            Destroy(cardlist[0], 2);//在数组中销毁卡牌，即删除游戏物体
            cardlist.RemoveAt(0);//在容器中删除该卡牌，7张历史牌继续排列
        }
        for(int i=0;i<cardlist.Count-1;i++)
            {
            //每当加入一张新牌，就将所有卡牌往下移动一个单位。每次都运行，保证顺序
            iTween.MoveTo(cardlist[i], cardlist[i].transform.position + new Vector3(0, yOffset, 0), 0.5f);
            }
    }
	// Use this for initialization
	
	
	// Update is called once per frame
	
}
