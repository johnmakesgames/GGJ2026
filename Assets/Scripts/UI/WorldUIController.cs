using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.Windows;

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
        WorldUIElementPool = UnityEngine.Object.FindObjectsByType<WorldUIEntityComponent>(FindObjectsInactive.Include, FindObjectsSortMode.None);
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
        var noLongerActiveElements = ActiveElements.FindAll(x => !x.IsVisualActive() && !x.gameObject.activeSelf); //remove non-active ones
        foreach (var item in noLongerActiveElements)
        {
            ReturnToFreePool(item);
        }
    }

    private void ReturnToFreePool(WorldUIEntityComponent element)
    {
        ActiveElements.Remove(element);
        FreeElements.Push(element);
    }

    WorldUIEntityComponent GetUIWorldElement(GameObject source, WorldUIType type, bool reuseElement)
    {
        WorldUIEntityComponent foundElementForWorldUI = null;
        //look through active elements for a matching one
        if (reuseElement)
        {
            foundElementForWorldUI = ActiveElements.FindLast(x => x.CanReuse(source, type));
        }

        if (foundElementForWorldUI == null)
        {
            if (FreeElements.TryPop(out foundElementForWorldUI))
            {
                ActiveElements.Add(foundElementForWorldUI);
                foundElementForWorldUI.ConfigureType(source, type);
                foundElementForWorldUI.gameObject.SetActive(true);
            }
        }
        return foundElementForWorldUI;
    }

    internal WorldUIEntityComponent ShowDamage(float dmg, Vector3 position, GameObject sourceOfDmg, bool reuseDamageForSameSource)
    {
        WorldUIEntityComponent foundElementForWorldUI = GetUIWorldElement(sourceOfDmg, WorldUIType.DamageEvent, reuseDamageForSameSource);
        //Can you really have no free UI elements....
        if (foundElementForWorldUI)
        {
            foundElementForWorldUI.UpdateText($"-{dmg}", position);
        }

        return foundElementForWorldUI; //Maybe someone can properly reuse this element
    }


    internal void ShowItemPickedup(ItemTag item, GameObject source)
    {
        bool resueElement = false;
        WorldUIEntityComponent foundElementForWorldUI = GetUIWorldElement(source, WorldUIType.Scavenge, resueElement);
        if (foundElementForWorldUI)
        {
            string itemGainedStr = "Picked up ";
            switch (item)
            {
                case ItemTag.ExtraOxygenTank:
                    {
                        itemGainedStr += "Oxygen Tank";
                        break;
                    }
                case ItemTag.Can:
                    {
                        itemGainedStr += "Can";
                        break;
                    }
                case ItemTag.Plant:
                    {
                        itemGainedStr += "Plant!";
                        break;
                    }
                case ItemTag.Gunpowder:
                    {
                        itemGainedStr += "GUN POWDER";
                        break;
                    }
                case ItemTag.Food:
                    {
                        itemGainedStr += "Food";
                        break;
                    }
                case ItemTag.ScrapMetal:
                    {
                        itemGainedStr += "Scrape Metal";
                        break;
                    }
                case ItemTag.Medkit:
                    {
                        itemGainedStr += "Med Kit";
                        break;
                    }
                case ItemTag.Ammo:
                    {
                        itemGainedStr += "Ammo";
                        break;
                    }
                case ItemTag.Slug:
                    {
                        itemGainedStr += "Slug";
                        break;
                    }
                case ItemTag.Cure:
                    {
                        itemGainedStr += "Cure";
                        break;
                    }
                default:
                    {
                        throw new NotImplementedException("Missing item type enum");
                    }
            } 

            foundElementForWorldUI.UpdateText(itemGainedStr, source.gameObject.transform.position);
        }
    }


    internal WorldUIEntityComponent ShowStatGained(string source, GameObject sourceObj, WorldUIType type)
    {
        WorldUIEntityComponent foundElementForWorldUI = GetUIWorldElement(sourceObj, type, false);
        //Can you really have no free UI elements....
        if (foundElementForWorldUI)
        {
            foundElementForWorldUI.UpdateText(source, sourceObj.transform.position);
        }

        return foundElementForWorldUI; //Maybe someone can properly reuse this element
    }

}
