using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAnchorPoints : MonoBehaviour
{
    public GameObject folder;
    public Vector3[] pointsPosition;
    public Material color;

    void Start()
    {
        int index = 0;
        foreach (Vector3 pos in pointsPosition) 
        {
            CreateInvisiblePoints(pos, folder, index);
            index++;
        }
    }

    private void CreateInvisiblePoints(Vector3 position, GameObject parent, int num)
    {
        GameObject point = GameObject.CreatePrimitive(PrimitiveType.Cube);
        point.transform.localScale = Vector3.one;
        point.transform.localPosition = position;
        point.transform.parent = parent.transform;
        point.name = "Invisible Point " + num;
        //point.GetComponent<Renderer>().material = color;
        Destroy(point.GetComponent<MeshRenderer>());
    }
}
