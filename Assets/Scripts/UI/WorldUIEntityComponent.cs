using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WorldUIEntityComponent : MonoBehaviour
{
    //Texture instance references on component
    public TextMeshProUGUI Text;
    public RawImage Image;

    //Circumstantial info
    private GameObject m_Source = null;
    private WorldUIController.WorldUIType m_WorldUIType = WorldUIController.WorldUIType.DamageEvent;

    [SerializeField]
    AnimationCurve FadeCurve;

    float m_AnimationTime = 0;
    CanvasGroup m_CanvasGrp = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_CanvasGrp = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        var alpha = FadeCurve.Evaluate(m_AnimationTime);
        m_CanvasGrp.alpha = alpha;

        m_AnimationTime += Time.deltaTime;
    }

    public bool IsActive()
    {
        return m_CanvasGrp && m_CanvasGrp.alpha > 0 //is visible
            || FadeCurve.length > 0 && FadeCurve.keys[FadeCurve.length - 1].time < m_AnimationTime; //Time elapsed fade curve
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

    internal void ShowWorldUI(string msg, Vector3 position, GameObject sourceOfDmg, WorldUIController.WorldUIType damageEvent)
    {
        m_Source = sourceOfDmg;
        m_WorldUIType = damageEvent;
        gameObject.transform.position = position;
        UpdateText(msg);
    }

    void UpdateText(string text)
    {
        Text.SetText(text);
        m_AnimationTime = 0;
    }
}
