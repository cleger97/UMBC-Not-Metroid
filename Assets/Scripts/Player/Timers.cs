using System.Collections;
using UnityEngine;

public class FloatTimer : MonoBehaviour {

    public float Value { get; protected set; }

    private float baseValue;

    public FloatTimer Constructor(float value) {
        this.Value = value;
        this.baseValue = value;
        this.hideFlags = HideFlags.HideInInspector;
        return this;
    }

    public void UpdateValue(float time, float newValue) {
        StopAllCoroutines();
        StartCoroutine(UpdateValueCoroutine(time, newValue));
    }

    IEnumerator UpdateValueCoroutine(float time, float newValue) {
        Value = newValue;
        yield return new WaitForSeconds(time);
        Value = baseValue;
    }
}

public class BoolTimer : MonoBehaviour {

    public bool Value { get; protected set; }

    private bool baseValue;

    public BoolTimer Constructor(bool value) {
        this.Value = value;
        this.baseValue = value;
        this.hideFlags = HideFlags.HideInInspector;
        return this;
    }

    public void UpdateValue(float time, bool newValue) {
        StopAllCoroutines();
        StartCoroutine(UpdateValueCoroutine(time, newValue));
    }

    IEnumerator UpdateValueCoroutine(float time, bool newValue) {
        Value = newValue;
        yield return new WaitForSeconds(time);
        Value = baseValue;
    }
}