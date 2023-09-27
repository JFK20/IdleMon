using System;
using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    private void OnEnable()
    {
        InputEventSystem.OnInpBegin += OnInpBegin;
        InputEventSystem.OnInpDrag += OnInpDrag;
        InputEventSystem.OnInpEnd += OnInpEnd;
    }

    private void OnDisable() {
        InputEventSystem.OnInpBegin -= OnInpBegin;
        InputEventSystem.OnInpDrag -= OnInpDrag;
        InputEventSystem.OnInpEnd -= OnInpEnd;
    }
    
    private void OnInpBegin(Vector3 pos) {
        Debug.Log(pos);
    }
    
    private void OnInpDrag(Vector3 pos) {
        //throw new NotImplementedException();
    }
    
    private void OnInpEnd(Vector3 pos) {
        //throw new NotImplementedException();
    }
}
