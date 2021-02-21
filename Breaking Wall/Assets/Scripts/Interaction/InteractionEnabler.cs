using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEnabler : MonoBehaviour
{
    static InteractionEnabler instance;

    [Header("Debugging")]
    public int startingLevel = 0;

    [Header("Interaction in Scene")]
    [Space(10)]
    public LevelInteraction[] levels;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Destroying Interaction Enabler " + name + " because " + instance.name + " is singleton!");
            Destroy(this);
        }
    }


    public void setLevel(int levelIndex)
    {


        if (levelIndex >= 0 && levelIndex < levels.Length)
        {
            Debug.Log("Enabling " + levelIndex + "th group");
            for (int i = 0; i < levels.Length; i++)
            {

                if (i == levelIndex)
                {
                    levels[i].enableInteraction();
                }
                else
                {
                    levels[i].disableInteraction();
                }

            }
        }
        else
        {
            Debug.LogWarning("Index out of bounds in InteractionEnabler!");

            foreach (LevelInteraction l in levels)
            {
                l.disableInteraction();
            }

        }

    }

}
[Serializable]
public class LevelInteraction
{
    public EventInteraction[] eventInteractions;
    public CollisionInteraction[] collisionInteractions;
    public DistanceInteraction[] distanceInteractions;


    public void enableInteraction()
    {

        foreach (EventInteraction e in eventInteractions)
        {
            e.readyForInteraction = true;
        }

        foreach (CollisionInteraction c in collisionInteractions)
        {
            c.readyForInteraction = true;
        }

        foreach (DistanceInteraction d in distanceInteractions)
        {
            d.readyForInteraction = true;
        }
    }

    public void disableInteraction()
    {
        foreach (EventInteraction e in eventInteractions)
        {
            e.readyForInteraction = false;
        }

        foreach (CollisionInteraction c in collisionInteractions)
        {
            c.readyForInteraction = false;
        }

        foreach (DistanceInteraction d in distanceInteractions)
        {
            d.readyForInteraction = false;
        }
    }

}
