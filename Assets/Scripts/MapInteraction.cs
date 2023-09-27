using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using Unity;

public class MapInteraction : MonoBehaviour
{
    //[SerializeField] private int maxWaitTime = 1;

    private void OnEnable()
    {
        InputEventSystem.OnInpBegin += OnInpBegin;
    }

    private void OnDisable() {
        InputEventSystem.OnInpBegin -= OnInpBegin;
    }
    
    private void OnInpBegin(Vector3 pos) {
        // Check if the mouse is over a UI element
        if (EventSystem.current.IsPointerOverGameObject()) {
            Vector2 position2D = new Vector2(pos.x, pos.y);
            
            RaycastHit2D hit = Physics2D.Raycast(position2D, Vector2.zero);
            
            if (hit.collider != null && hit.collider.CompareTag("Clickable"))
            {
                Debug.Log(hit.collider.gameObject.name);
            }
        }
    }
    
    /*private bool IsDoubleTap() {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) {
            Touch touch = Input.GetTouch(0);
            float deltaTime = touch.deltaTime;
            if (deltaTime > 0 && deltaTime < maxWaitTime && touch.tapCount == 2) {
                return true;
            }
        }
        return false;
    }*/
}
