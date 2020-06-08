// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
//
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class FloatVariable : ScriptableObject
{
#if UNITY_EDITOR

    [Multiline]
    public string DeveloperDescription = "";

#endif
    public float Value;

    public void SetValue(float value)
    {
        Value = value;
        OnValueChange?.Invoke();
    }

    public void SetValue(FloatVariable value)
    {
        Value = value.Value;
        OnValueChange?.Invoke();
    }

    public void ApplyChange(float amount)
    {
        Value += amount;
        OnValueChange?.Invoke();
    }

    public void ApplyChange(FloatVariable amount)
    {
        Value += amount.Value;
        OnValueChange?.Invoke();
    }

    public UnityEvent OnValueChange;
}