using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomPathTrolling : MonoBehaviour
{
    public NavMeshAgent agent; //宣告代理人屬性
    public float range;        //指定中心的半徑範圍

    [SerializeField] private bool stopOrWalk; //決定要不要停下來，True代表可走，False代表停下來
    public Transform headPlayer;              //抓取玩家鏡頭

    public Transform centrePoint;      
    //代理人想要移動的區域的中心
    //如果不需要一個特定的區域的話，就把Trasform的參考設為代理人自身的Trasform
   

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stopOrWalk = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopOrWalk) //如果還能走
        {        
            if (agent.remainingDistance <= agent.stoppingDistance) //路徑走完時
            {
                Vector3 point;
                if (RandomPoint(centrePoint.position, range, out point)) //在中心點和區域的半徑內通過
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //在gizmos中觀察
                    agent.SetDestination(point);  //重新設置代理人的目標地
                }
            }
        }
        else
        {
            //不能走的話
            //就重置代理人的路徑，並且面對玩家
            agent.ResetPath();
            transform.LookAt(new Vector3(headPlayer.position.x, transform.position.y, headPlayer.position.z)); //讓NPC即時面相玩家鏡頭
        }

    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //在sphere裡面隨機挑一點 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //文件: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //1.0是隨機點到navmesh上另一個點的最大距離，如果範圍很大的話，可以像文件中一樣，加個loop
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public void SetWalkFalse() 
    {
        //當玩家撞到NPC的時候，讓stopOrWalk設定為false，使NPC呼叫ResetPath()。 
        //會由DialogTrigger呼叫
        stopOrWalk = false;
    }
    public void SetWalkTrue()  
    {
        //當玩家對話完後，讓stopOrWalk設定為walk，讓NPC繼續移動
        //會由DialogManager呼叫
        stopOrWalk = true;
    }

}
