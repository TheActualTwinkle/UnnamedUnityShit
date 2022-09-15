using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Map : MonoBehaviour
{
    private static bool isOpen;
    public static bool IsOpen { get => isOpen; }

    [SerializeField] private MapFieldOfView fieldOfView;

    private List<DrawnMapElement> mapElements = new List<DrawnMapElement>();

    private Animator animator;

    private void OnEnable()
    {
        fieldOfView.MapEelementEnterEvent += OnNewElementEnter;
        fieldOfView.MapEelementExitEvent += OnNewElementExit;
    }

    private void OnDisable()
    {
        fieldOfView.MapEelementEnterEvent -= OnNewElementEnter;
        fieldOfView.MapEelementExitEvent -= OnNewElementExit;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (InteractAccessor.CanInteract)
        {
            if (Input.GetKeyDown(Keybinds.KeyBinds[Actions.Map]))
            {
                Open();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            Close();
        }
    }

    private void FixedUpdate()
    {
        foreach (var mapElement in mapElements)
        {

        }
    }

    private void Open()
    {
        isOpen = true;

        animator.SetTrigger("OpenMap");

    }

    private void Close()
    {
        animator.SetTrigger("CloseMap");
    }

    private void OnNewElementEnter(DrawnMapElement mapElement)
    {
        mapElements.Add(mapElement);
    }    
    
    private void OnNewElementExit(DrawnMapElement mapElement)
    {
        mapElements.Remove(mapElement);
    }

    // Animator.
    private void EnableInput()
    {
        isOpen = false;
    }
}