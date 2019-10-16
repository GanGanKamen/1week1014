using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpCtrl : MonoBehaviour
{
    [SerializeField] private Character player;
    [SerializeField] private Slider firstHpBar;
    [SerializeField] private float step;
    private Canvas canvas;

    private List<Slider> HpBars;
    [SerializeField]private int nowHpNum;
    [SerializeField]private int lostHpNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        HpBars = new List<Slider>();
        HpBars.Add(firstHpBar);
        nowHpNum = HpBars.Count-1-lostHpNum;
        firstHpBar.maxValue = player.hp;
    }

    // Update is called once per frame
    void Update()
    {
        HpNumCheck();
    }

    public void HpPlus()
    {
        player.hp += player.partHp; 
        GameObject hpBarObj = Instantiate(SystemCtrl.HpBar, canvas.transform);
        Slider hp = hpBarObj.GetComponent<Slider>();
        hp.maxValue = player.partHp;
        HpBars.Add(hp);
        HpBars.Reverse();
        
        for (int i = 0; i< HpBars.Count; i++)
        {
            HpBars[i].GetComponent<RectTransform>().localPosition =
                new Vector3(-370 + step * i, 200, 0);
        }
    }

    private void HpNumCheck()
    {
        nowHpNum = HpBars.Count - 1 - lostHpNum;
        for (int i = 0;i< HpBars.Count; i++)
        {
            if(i == nowHpNum)
            {
                HpBars[i].value = player.hp - player.partHp * nowHpNum;
            }
            else if(i < nowHpNum)
            {
                HpBars[i].value = player.partHp;
            }
            else
            {
                HpBars[i].value = 0;
            }
        }

        if(HpBars[nowHpNum].value <= 0)
        {
            if(nowHpNum == 0)
            {
                Debug.Log("GameOver");
            }
            else
            {
                lostHpNum += 1;
            }
        }
    }
}
