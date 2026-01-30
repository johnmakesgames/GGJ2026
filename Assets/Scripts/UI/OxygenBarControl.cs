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
        //Debug lets move the slider to show it doing stuff
        if(OxygenSlider)
        {
            float newValue = OxygenSlider.value + 0.1f;
            OxygenSlider.value = newValue % (float)OxygenSlider.maxValue;
        }
        if (HealthSlider)
        {
            float newValue = HealthSlider.value + 0.1f;
            HealthSlider.value = newValue % (float)HealthSlider.maxValue;
        }
    }
}
