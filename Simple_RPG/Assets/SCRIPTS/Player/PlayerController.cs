using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMover))]
public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;
    Camera cam;
    PlayerMover mover;
    public Interactable focus;


    void Start()
    {
        cam = Camera.main;
        mover = GetComponent<PlayerMover>();
    }



    void Update()
    {
        CheckForClicks();
    }


    private void CheckForClicks()
    {
        // Check for LEFT User Click
        if (Input.GetMouseButtonDown(0))        // LEFT
        {
            // Fire a ray at mouse position
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Check if ray hit anything
            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                //Debug.Log("We hit " + hit.collider.name + " " + hit.point);

                // Move the Player
                mover.MoveToPoint(hit.point);

                // Stop focusing on any objects
                RemoveFocus();
            }
        }

        // Check for RIGHT User Click
        if (Input.GetMouseButtonDown(1))        // RIGHT
        {
            // Fire a ray at mouse position
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Check if ray hit anything
            if (Physics.Raycast(ray, out hit, 100))
            {
                // Check if hit an interactable object
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    // If did, set it as focus
                    SetFocus(interactable);
                }
            }
        }
    }

    private void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null) { focus.OnDefocused(); }

            focus = newFocus;
            mover.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }

    private void RemoveFocus()
    {
        if (focus != null) { focus.OnDefocused(); }
        focus = null;
        mover.StopFollowingTarget();
    }

}
