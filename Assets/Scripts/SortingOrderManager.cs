using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrderManager : MonoBehaviour
{
    public string layerName;
    public int order;
    MeshRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.sortingLayerName = layerName;
        renderer.sortingOrder = order;
    }

    void Update()
    {
        if (renderer.sortingLayerName != layerName)
            renderer.sortingLayerName = layerName;
        if (renderer.sortingOrder != order)
            renderer.sortingOrder = order;
    }

    public void OnValidate()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.sortingLayerName = layerName;
        renderer.sortingOrder = order;
    }
}
