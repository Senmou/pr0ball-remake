using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class InputHelper : MonoBehaviour {

    #region Singleton
    public static InputHelper instance;

    private float longPressCounter;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    #endregion

    public bool IsPointerOverUIObject() {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    public void PrintClickedElementsName() {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        string text = string.Empty;
        foreach (var item in results) {
            text += "[" + item.gameObject.name + "]" + " > ";
        }

        if (text.Length > 2)
            text = text.Remove(text.Length - 3);

        Debug.Log(text);
    }

    public bool ClickedOnTag(string tag) {
        return false;
        //PointerEventData eventData = new PointerEventData(EventSystem.current);
        //eventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //List<RaycastResult> results = new List<RaycastResult>();
        //EventSystem.current.RaycastAll(eventData, results);

        //int resultCount = results.Count;
        //for (int i = 0; i < resultCount; i++) {
        //    if (results[i].gameObject.CompareTag(tag))
        //        return true;
        //}
        //return false;
    }

    public bool LongPress(float seconds) {

        if (Input.GetMouseButton(0))
            longPressCounter += Time.unscaledDeltaTime;

        if (longPressCounter >= seconds) {
            longPressCounter = 0f;
            return true;
        }

        return false;
    }
}
