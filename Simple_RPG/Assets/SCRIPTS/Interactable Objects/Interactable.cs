using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //interactionDistance
    public float radius = 3f;

    bool isFocus = false;
    Transform player;

    bool hasInterActed = false;
    /// <summary>
    /// ///////////////////////////////////////////////////////////////// BRACKEYS video INTERACTION 11:34
    /// </summary>

    private void Update()
    {
        if (isFocus)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= radius)
            {
                //Interact
                Debug.Log("INTERACT!");
            }
        }
    }


    public void OnFocused (Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
