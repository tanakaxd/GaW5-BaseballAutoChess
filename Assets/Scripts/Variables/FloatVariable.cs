// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
//
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using DG.Tweening;
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

        //DOTween.To(
        //    () => score,          // 何を対象にするのか
        //    num => score = num,   // 値の更新
        //    money.Value,                  // 最終的な値
        //    1.0f                  // アニメーション時間
        //);

    }

    public void ApplyChange(FloatVariable amount)
    {
        Value += amount.Value;
        OnValueChange?.Invoke();
    }

    public UnityEvent OnValueChange;
}