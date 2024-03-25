using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Itembox : MonoBehaviour
{
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public static int x = 0;
    ColorBlock cb = new ColorBlock();
    ColorBlock cb0= new ColorBlock();
    // Start is called before the first frame update
    void Start()
    {
        cb.normalColor = Color.red;
        cb0.normalColor = Color.white;
        cb.colorMultiplier = 1;
        cb0.colorMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Chosebtn();
    }
    void Chosebtn()
    {
        //----------------------------------------------物品藍往下往上滾 <0便3 >0便0
        if((Input.GetAxis("Mouse ScrollWheel"))==0.1f)
        {
            x++;
            if(x>3)
            {
                x=0;
            }
        }
        else if((Input.GetAxis("Mouse ScrollWheel"))==-0.1f)
        {
            x--;
            if(x<0)
            {
                x=3;
            }
        }
        //----------------------------------
        if(x==0)
        {
            btn1.colors=cb0;
            btn2.colors=cb0;
            btn3.colors=cb0;
        }
        else if(x==1)
        {
            btn1.colors=cb;
            btn2.colors=cb0;
            btn3.colors=cb0;
        }
        else if(x==2)
        {
            btn1.colors=cb0;
            btn2.colors=cb;
            btn3.colors=cb0;
        }
        else if(x==3)
        {
            btn1.colors=cb0;
            btn2.colors=cb0;
            btn3.colors=cb;
        }
    }
}
