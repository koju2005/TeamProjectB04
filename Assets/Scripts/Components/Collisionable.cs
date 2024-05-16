using System;
using System.Collections;
using System.Collections.Generic;
using Data.Functions;
using DefaultNamespace.Data;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Collisionable : PooledObject
{
    [SerializeField] private CollisionInteraction[] action;
    private List<CollisionInteraction> preActions = new List<CollisionInteraction>(10);
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        foreach (var val in action)
        {
            val.EnterCollsion(gameObject, other.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        foreach (var val in action)
        {
            val.ExitCollsion(gameObject,other.gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var val in action)
        {
            val.EnterCollsion(gameObject,other.gameObject);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        foreach (var val in action)
        {
            val.ExitCollsion(gameObject,other.gameObject);
        }
    }

    private void OnValidate()
    {
        if (preActions.Count != action.Length)
        {
            bool[] isAlived = new bool[preActions.Count];

            List<CollisionInteraction> newNeedComponent = new List<CollisionInteraction>(10);
            //추가,삭제 Component Check
            for (int i = 0; i < action.Length; i++)
            {
                int idx = preActions.FindIndex(x => x == action[i]);
                if(idx == -1)
                    newNeedComponent.Add(action[i]);
                else
                {
                    isAlived[idx] = true;
                }
            }
            // Component 삭제
            int delCount = 0;
            for (int i = 0; i < isAlived.Length; i++)
            {
                if (!isAlived[i])
                {
                    Component component = preActions[i-delCount].GetRemoveComponent(gameObject);
                    UnityEditor.EditorApplication.delayCall += () => { DestroyImmediate(component,true); };
                    preActions.RemoveAt(i-delCount);
                    ++delCount;
                }
            }
            // Component 추가
            for (int i = 0; i < newNeedComponent.Count; i++)
            {
                newNeedComponent[i].AddRequireComponent(gameObject);
                preActions.Add(newNeedComponent[i]);
            }
        }
    }
}
