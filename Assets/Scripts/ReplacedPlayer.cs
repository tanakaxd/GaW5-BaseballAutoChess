using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ReplacedPlayer : MonoBehaviour, IDropHandler
{
    //public PlayerModelDataBase dataBase;
    public UnityEvent onOrderChanged;

    public FloatVariable money;

    public void OnDrop(PointerEventData eventData)
    {
        Transform draggedCard = eventData.pointerDrag.transform;


        if (draggedCard.CompareTag("Player"))//orderの入れ替え
        {
            PlayerController draggedPlayer = draggedCard.GetComponent<PlayerController>();
            PlayerController targetPlayer = transform.GetComponent<PlayerController>();

            if (draggedPlayer.ID == targetPlayer.ID)
            {
                if (targetPlayer.level >= 3)
                    return;
                //
                transform.GetComponent<PlayerController>().LevelUp();

                //ドラッグされた方は空にする＝デフォルトのロード

                draggedPlayer.LoadModel(draggedPlayer.defaultModel);
            }
            else
            {
                int draggedIndex = draggedCard.GetSiblingIndex();

                int replacedIndex = transform.GetSiblingIndex();

                Debug.Log(transform.name);
                Debug.Log(draggedCard.name);

                transform.SetSiblingIndex(draggedIndex);
                draggedCard.SetSiblingIndex(replacedIndex);

                onOrderChanged?.Invoke();
            }
        }
        else if (draggedCard.CompareTag("Drawn"))//新しいメンツの加入
        {
            DrawnPlayerController newPlayer = draggedCard.GetComponent<DrawnPlayerController>();
            PlayerController replacedPlayer = transform.GetComponent<PlayerController>();

            if (newPlayer.cost > money.Value)
            {
                return;
            }
            else
            {
                money.ApplyChange(-newPlayer.cost);
            }


            if (newPlayer.ID == replacedPlayer.ID)
            {
                if (replacedPlayer.level >= 3)
                    return;

                replacedPlayer.LevelUp();
                newPlayer.transform.gameObject.SetActive(false);
            }
            else
            {

                replacedPlayer.LoadModel(replacedPlayer.dataBase.GetModel(newPlayer.ID));
                newPlayer.transform.gameObject.SetActive(false);

                onOrderChanged?.Invoke();

            }

        }
    }
}