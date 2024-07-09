using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {
    [SerializeField] private string itemName;
    private Guid id;
    [SerializeField] private MenuSlot orignalSlot;

    public void OnBeginDrag(PointerEventData eventData) {
        // turn off raycasting
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        orignalSlot.GetComponent<RectTransform>().SetAsLastSibling();
    }
    public void OnDrag(PointerEventData eventData) {
        Debug.Log("OnDrag");
        transform.localPosition += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        // TODO if not accepted, return to start position
        transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    public Guid GetID() {
        return id;
    }

    // Start is called before the first frame update
    void Start() {
        id = Guid.NewGuid();
        itemName = id.ToString();
    }

    // Update is called once per frame
    void Update() {

    }

    public int getSlotIndex() {
        return orignalSlot.getIndex();
    }

    public void setSlot(MenuSlot menuSlot) {
        orignalSlot = menuSlot;
        gameObject.transform.SetParent(menuSlot.transform);
        gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    internal MenuSlot GetSlot() {
        return orignalSlot;
    }

}
