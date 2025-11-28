using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CollisionManager : MonoBehaviour
{
    public CollisionType CollisionType;
    public CollisionName collisionName;

    [Header("Trigger")]
    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private UnityEvent onTriggerExit;
    [SerializeField] private UnityEvent onTriggerStay;

    [Header("Collision")]
    [SerializeField] private UnityEvent onCollisionEnter;
    [SerializeField] private UnityEvent onCollisionExit;
    [SerializeField] private UnityEvent onCollisionStay;


    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == collisionName.ToString()) { onTriggerEnter?.Invoke();}
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.name == collisionName.ToString()) { onTriggerEnter?.Invoke(); }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == collisionName.ToString()) { onTriggerExit?.Invoke(); }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == collisionName.ToString()) { onTriggerExit?.Invoke(); }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == collisionName.ToString()) { onTriggerStay?.Invoke(); }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == collisionName.ToString()) { onTriggerStay?.Invoke(); }
    }

    #endregion

    #region Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == collisionName.ToString()) { onCollisionEnter?.Invoke(); }
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.name == collisionName.ToString()) { onCollisionEnter?.Invoke(); }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
         if(collision.gameObject.name == collisionName.ToString()) { onCollisionStay?.Invoke(); }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.name == collisionName.ToString()) { onCollisionStay?.Invoke(); }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.name == collisionName.ToString()) { onCollisionExit?.Invoke(); }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.name == collisionName.ToString()) { onCollisionExit?.Invoke(); }
    }

    #endregion
}

public enum CollisionType
{
    Collision,
    Trigger
}

public enum CollisionName
{
    Player,
    Trap,
    Enemy
}