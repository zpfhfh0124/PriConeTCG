using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public enum LayerType
{
    background = 0,
    middle,
    point,
    name
}

public class Order : MonoBehaviour
{
    const int MAX_ORDER = 100;

    [SerializeField] LayerType layerType;
    [SerializeField] int originOrder;
    [SerializeField] Renderer backRenderer;
    [SerializeField] Renderer characterRenderer;
    [SerializeField] Renderer nameRenderer;
    [SerializeField] Renderer atkRenderer;
    [SerializeField] Renderer hpRenderer;

    public void SetOriginOrder(int originOrder)
    {
        this.originOrder = originOrder;
        SetOrder(originOrder);
    }

    // 맨 앞의 오더
    public void SetHeadOrder(bool isHeadOrder)
    {
        SetOrder( isHeadOrder ? MAX_ORDER : originOrder);
    }

    public void SetOrder(int order)
    {

    }
}
