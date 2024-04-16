using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    private Vector2 _touchPosition;
    bool _isInput = false;

    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetMouseButton(0))
        {
            _touchPosition = Input.mousePosition;
            _isInput = true;
        }
#else
        if(Input.touchCount > 0)
        {
            _touchPosition = Input.GetTouch(0).position;
            _isInput = true;
        }
#endif
    }
}
