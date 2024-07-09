using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour {
    [SerializeField] private List<MenuSlot> slots;

    public static MenuManager instance;
    // Start is called before the first frame update
    void Start() {
        if (instance == null) {
            instance = this;
            slots = new List<MenuSlot>();
            GameObject slotA = GameObject.Find("SlotA");
            GameObject slotB = GameObject.Find("SlotB");

            if (slotA != null && slotB != null) {
                slots.Add(slotA.GetComponent<MenuSlot>());
                slots.Add(slotB.GetComponent<MenuSlot>());
            } else {
                Debug.LogError("SlotA or SlotB not found!");
            }
        } else {
            Destroy(gameObject);
        }
    }

    public void SwitchItemsPosition(int droppedIndex, int targetIndex) {
        Debug.Log("Switching items position");
    }
    // Update is called once per frame
    void Update() {

    }

}
