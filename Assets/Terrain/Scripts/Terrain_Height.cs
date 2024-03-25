using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.y = Terrain.activeTerrain.SampleHeight(transform.position);
        transform.position = pos;
    }
}
