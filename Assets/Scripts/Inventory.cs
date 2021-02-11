using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{

    public int count;
    public Type type;


    // Use this for initialization
    void Start()
    {
        gameObject.tag = type.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public enum Type
    {
        GUN,
        BOMB,
        KEY

    }
}