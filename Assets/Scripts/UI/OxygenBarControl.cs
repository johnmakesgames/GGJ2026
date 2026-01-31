using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBarControl : MonoBehaviour
{
    public Slider OxygenSlider;
    public Slider HealthSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetOxygenBarValue(float value, float maxValue)
    {
        OxygenSlider.value = (value / maxValue) * 100.0f;
    }

    public void SetHealthBarValue(float value, float maxValue)
    {
        HealthSlider.value = (value / maxValue) * 100.0f;
    }
}
