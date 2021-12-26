using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //interactionDistance
    public float radius = 3f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInterActed = false;


    public virtual void Interact()
    {
        // This method is meant to be overwritten (in the specific script of the interactable item or enemy)
        Debug.Log("Interacting with " + transform.name);
    }



    private void Update()
    {
        if (isFocus && !hasInterActed)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                //Interact
                //Debug.Log("INTERACT!");
                Interact();

                hasInterActed = true;
            }
        }
    }


    public void OnFocused (Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInterActed = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInterActed = false;
    }


    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

}
