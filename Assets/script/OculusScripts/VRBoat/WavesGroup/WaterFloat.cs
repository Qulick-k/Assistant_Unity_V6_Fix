using Ditzelgames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFloat : MonoBehaviour
{

    //public properties 公有屬性
    public float AirDrag = 1;
    public float WaterDrag = 10;
    public Transform[] FloatPoints;
    public bool AttachToSurface;

    //used components 使用的掛件
    protected Rigidbody Rigidbody;
    protected Waves Waves;

    //water line  水線
    protected float WaterLine;
    protected Vector3[] WaterLinePoints;

    //help Vectors 中心的公差
    protected Vector3 centerOffset;
    protected Vector3 smoothVectorRotation;
    protected Vector3 TargetUp;

    //抓取中心位置
    public Vector3 Center { get { return transform.position + centerOffset; } }


    // Start is called before the first frame update
    void Awake()
    {
        Waves = FindObjectOfType<Waves>();
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.useGravity = false;


        //compute center 計算中心
        WaterLinePoints = new Vector3[FloatPoints.Length];
        for (int i = 0; i < FloatPoints.Length; i++)
        {
            WaterLinePoints[i] = FloatPoints[i].position;
        }
        centerOffset = PhysicsHelper.GetCenter(WaterLinePoints) - transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //default water surface  預設的水面值
        var newWaterLine = 0f;
        var pointUnderWater = false;

        //set WaterLinePoints and WaterLine
        for (int i = 0; i <FloatPoints.Length; i++)
        {
            //height  利用GetHeight()，重新設定高度
            WaterLinePoints[i] = FloatPoints[i].position;
            WaterLinePoints[i].y = Waves.GetHeight(FloatPoints[i].position);
            newWaterLine += WaterLinePoints[i].y / FloatPoints.Length;
            if (WaterLinePoints[i].y > FloatPoints[i].position.y)
            {
                pointUnderWater = true;
            }
        }

        var waterLineDelta = newWaterLine - WaterLine;
        WaterLine = newWaterLine;

        //gravity 重力
        var gravity = Physics.gravity;
        Rigidbody.drag = AirDrag;
        if (WaterLine > Center.y)
        {
            Rigidbody.drag = WaterDrag;
            //under water
            if (AttachToSurface)
            {
                //attach to water surface
                Rigidbody.position = new Vector3(Rigidbody.position.x, WaterLine - centerOffset.y, Rigidbody.position.z);
            }
            else
            {
                //go up
                gravity = -Physics.gravity;
                transform.Translate(Vector3.up * WaterLine * 0.9f);
            }           
        }
        Rigidbody.AddForce(gravity * Mathf.Clamp(Mathf.Abs(WaterLine - Center.y), 0, 1));

        //compute up vector
        TargetUp = PhysicsHelper.GetNormal(WaterLinePoints);

        //rotation
        if (pointUnderWater)
        {
            //attach to water surface
            TargetUp = Vector3.SmoothDamp(transform.up, TargetUp, ref smoothVectorRotation, 0.2f);
            Rigidbody.rotation = Quaternion.FromToRotation(transform.up, TargetUp) * Rigidbody.rotation;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (FloatPoints == null)
        {
            return;
        }

        for (int i = 0; i < FloatPoints.Length; i++)
        {
            if (FloatPoints[i] == null)
            {
                continue;
            }

            if (Waves != null)
            {

                //draw cube
                Gizmos.color = Color.red;
                Gizmos.DrawCube(WaterLinePoints[i], Vector3.one * 0.3f);
            }

            //draw sphere
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(FloatPoints[i].position, 0.1f);
        }

        //draw center
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(Center.x, WaterLine, Center.z), Vector3.one * 1f);
        }
    }
}
