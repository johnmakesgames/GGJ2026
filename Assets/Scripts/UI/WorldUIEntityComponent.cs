using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldUIEntityComponent : MonoBehaviour
{
    //Texture instance references on component
    public TextMeshProUGUI Text;
    public RawImage Image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsActive()
    {
        return this.gameObject.activeSelf;
    }

    public void Enable()
    {
        
    }
}
