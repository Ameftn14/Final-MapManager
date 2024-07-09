using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSlot : MonoBehaviour, IDropHandler {
    [SerializeField] private MenuItem item;
    [SerializeField] private int index;
    public void OnDrop(PointerEventData eventData) {
        Debug.Log("Something was dropped on a slot");
        GameObject droppedGameObject = eventData.pointerDrag;
        // make sure the dropped object is a MenuItem
        if (droppedGameObject == null) return;
        if (droppedGameObject.GetComponent<MenuItem>() == null) return;

        Vector2 myPosition = GetComponent<RectTransform>().anchoredPosition;
        MenuItem droppedItem = droppedGameObject.GetComponent<MenuItem>();
        MenuSlot otherSlot = droppedItem.GetSlot();
        otherSlot.setItem(item);
        setItem(droppedItem);
    }
    public void setItem(MenuItem item) {
        this.item = item;
        item.setSlot(this);
    }
    public void SetID(int index) {
        this.index = index;
    }

    public void SetItem(MenuItem menuItem) {
        this.item = menuItem;
    }

    public int getIndex() {
        return index;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


}
