using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WorldUIController : MonoBehaviour
{
    public enum WorldUIType
    {
        DamageEvent,
        HealthGame,
        OxygenGain,
        Scavenge
    }

    private WorldUIEntityComponent[] WorldUIElementPool;
    private Stack<WorldUIEntityComponent> FreeElements = new Stack<WorldUIEntityComponent>();
    private List<WorldUIEntityComponent> ActiveElements = new List<WorldUIEntityComponent>(); //Not a queue as some events have different timings to display for


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WorldUIElementPool = Object.FindObjectsByType<WorldUIEntityComponent>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        var worldCamera = Camera.main;

        foreach (var item in WorldUIElementPool)
        {
            FreeElements.Push(item);
            if (worldCamera)
            {
                var canvas = item.gameObject.GetComponent<Canvas>();
                if (canvas)
                {
                    canvas.worldCamera = worldCamera;
                    canvas.renderMode = RenderMode.ScreenSpaceCamera;
                    canvas.renderMode = RenderMode.WorldSpace;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check active lists (which could be empty)
        var noLongerActiveElements = ActiveElements.FindAll(x => !x.IsActive()); //remove non-active ones
        foreach (var item in noLongerActiveElements)
        {
            ReturnToFreePool(item);
        }
    }

    internal WorldUIEntityComponent ShowDamage(float dmg, Vector3 position, GameObject sourceOfDmg, bool reuseDamageForSameSource)
    {
        WorldUIEntityComponent foundElementForWorldUI = null;
        //look through active elements for a matching one
        if (reuseDamageForSameSource)
        {
           foundElementForWorldUI = ActiveElements.FindLast(x => x.CanReuse(sourceOfDmg, WorldUIType.DamageEvent));
        }

        if(foundElementForWorldUI == null)
        {
            if(FreeElements.TryPop(out foundElementForWorldUI))
            {
                ActiveElements.Add(foundElementForWorldUI);
                foundElementForWorldUI.gameObject.SetActive(true);
            }
        }

        //Can you really have no free UI elements....
        if(foundElementForWorldUI)
        {
            foundElementForWorldUI.ShowWorldUI(dmg, position, sourceOfDmg, WorldUIType.DamageEvent);
        }

        return foundElementForWorldUI; //Maybe someone can properly reuse this element
    }

    private void ReturnToFreePool(WorldUIEntityComponent element)
    {
        element.gameObject.SetActive(false);

        ActiveElements.Remove(element);
        FreeElements.Push(element);
    }
}
