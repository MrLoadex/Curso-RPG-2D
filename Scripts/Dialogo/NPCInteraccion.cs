using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraccion : MonoBehaviour
{
    [SerializeField] private GameObject npcBotonInteractuar;
    [SerializeField] private NPCDialogo npcDialogo;

    public NPCDialogo Dialogo => npcDialogo;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            DialogoManager.Instance.NPCDisponible = this;
            npcBotonInteractuar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        DialogoManager.Instance.NPCDisponible = null;
        npcBotonInteractuar.SetActive(false);
    }
}
