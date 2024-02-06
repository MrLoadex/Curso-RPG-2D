using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//DropedItem
public class ItemPorAgregar : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InventarioItem inventarioItemReferencia;
    [SerializeField] private int cantidadAgregar;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            Inventario.Instance.AñadirItem(inventarioItemReferencia, cantidadAgregar);
            Destroy(gameObject);
        }    
    }
}
