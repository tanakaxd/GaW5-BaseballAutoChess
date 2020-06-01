using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReplacedPlayer : MonoBehaviour, IDropHandler
{
    //public PlayerModelDataBase dataBase;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.transform.CompareTag("Player"))
        {
            int draggedIndex = eventData.pointerDrag.transform.GetSiblingIndex();

            int replacedIndex = transform.GetSiblingIndex();

            Debug.Log(transform.name);
            Debug.Log(eventData.pointerDrag.transform.name);

            transform.SetSiblingIndex(draggedIndex);
            eventData.pointerDrag.transform.SetSiblingIndex(replacedIndex);
        }
        else if (eventData.pointerDrag.transform.CompareTag("Drawn"))
        {
            DrawnPlayerController newPlayer = eventData.pointerDrag.transform.GetComponent<DrawnPlayerController>();
            PlayerController replacedPlayer = transform.GetComponent<PlayerController>();

            replacedPlayer.LoadModel(replacedPlayer.dataBase.GetModel(newPlayer.ID));
            newPlayer.transform.gameObject.SetActive(false);

            
        }
        
    }

 
}
