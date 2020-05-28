using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReplacedPlayer : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        int draggedIndex = eventData.pointerDrag.transform.GetSiblingIndex();

        int replacedIndex = transform.GetSiblingIndex();

        transform.SetSiblingIndex(draggedIndex);
        eventData.pointerDrag.transform.SetSiblingIndex(replacedIndex);
    }

 
}
