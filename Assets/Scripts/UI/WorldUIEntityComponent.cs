using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldUIEntityComponent : MonoBehaviour
{
    //Texture instance references on component
    public TextMeshProUGUI Text;
    public RawImage Image;

    private GameObject m_Source = null;
    private WorldUIController.WorldUIType m_WorldUIType = WorldUIController.WorldUIType.DamageEvent;

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

    public WorldUIController.WorldUIType GetUIType()
    {
        return m_WorldUIType;
    }

    public GameObject GetSourceOfWorldUI()
    {
        return m_Source;
    }

    public bool CanReuse(GameObject sourceOfDmg, WorldUIController.WorldUIType damageEvent)
    {
        return sourceOfDmg == GetSourceOfWorldUI() && damageEvent == GetUIType();
    }

    internal void ShowWorldUI(float dmg, Vector3 position, GameObject sourceOfDmg, WorldUIController.WorldUIType damageEvent)
    {
        m_Source = sourceOfDmg;
        m_WorldUIType = damageEvent;
        gameObject.transform.position = position;
        UpdateText("Ouchy!");
    }

    void UpdateText(string text)
    {
        Text.SetText(text);
    }
}
