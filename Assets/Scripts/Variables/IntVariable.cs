using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class IntVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public int Value;

    public void SetValue(int value)
    {
        Value = value;
        OnValueChange?.Invoke();
    }

    public void SetValue(IntVariable value)
    {
        Value = value.Value;
        OnValueChange?.Invoke();
    }

    public void ApplyChange(int amount)
    {
        Value += amount;
        OnValueChange?.Invoke();
    }

    public void ApplyChange(IntVariable amount)
    {
        Value += amount.Value;
        OnValueChange?.Invoke();
    }

    #region event
    //public delegate void OnValueChange();

    #endregion

    public UnityEvent OnValueChange;
    //public GameEvent gameEvent;


}