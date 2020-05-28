using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private Vector3 defaultPosition;
    private int siblingsIndexBefore;
    private int siblingsIndexAfter;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begindrag");
        canvasGroup.blocksRaycasts = false;
        //siblingsIndexBefore = transform.GetSiblingIndex();
        //Debug.Log(siblingsIndexBefore);
        defaultPosition = transform.position;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("drag");

        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("enddrag");
        transform.position = defaultPosition;

        canvasGroup.blocksRaycasts = true;


    }
}
