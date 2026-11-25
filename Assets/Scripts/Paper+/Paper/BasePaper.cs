using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BasePaper : MonoBehaviour, IDamageable
{
    //*
    //Any GameObject that derives from BasePaper has to set its status to one of these two:
    //Empty or Printed
    //*
    protected enum Status
    {
        Empty,
        Printed
    }

    //*
    //The IsDragging variable is used to facilitate the dragging of a GameObject derived from BasePaper
    //whenever a mouse clicks and holds on it.
    //* 

    public bool IsDragging;

    protected Status CurrentStatus;


    protected virtual void Start()
    {
        IsDragging = false;
    }

    protected virtual void Update()
    {
        if (IsDragging)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    protected virtual void SetStatus (Status passedStatus)
    {
        CurrentStatus = passedStatus;
    }

    protected virtual void OnMouseDown()
    {
        IsDragging = true;
        // AudioSystem.Instance.PlaySound(SoundType.UI, 1);
    }

    protected virtual void OnMouseUp()
    {
        IsDragging = false;
        // AudioSystem.Instance.PlaySound(SoundType.UI, 2);
    }

    //*
    //TakeDamage is used to destroy the paper, it is used when you put EmptyPaper
    //in the printer...or put PrintedPaper in the Tray.
    //*

    public void TakeDamage(int amount)
    {
        int damageTaken = amount;

        Destroy(this.gameObject);
    }
}
