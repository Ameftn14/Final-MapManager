using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuItemBehaviour : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {
    [SerializeField] private Vector3 startPosition;
    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
        startPosition = transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log("OnDrag");
        transform.localPosition += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        transform.localPosition = startPosition;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
