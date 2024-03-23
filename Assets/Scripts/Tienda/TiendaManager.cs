using System;
using UnityEngine;

public class TiendaManager : MonoBehaviour 
{
    [Header("Config")]
    [SerializeField] private ItemTienda itemTiendaPrefab;     
    [SerializeField] private Transform panelContenedor;
    
    [Header("Items")]
    [SerializeField]
    private ItemVenta[] itemDisponibles;

    private void Start() 
    {
        CargarItemsTienda();
    }

    private void CargarItemsTienda()
    {
        for (int i = 0; i < itemDisponibles.Length; i++)
        {
            ItemTienda itemTienda = Instantiate(itemTiendaPrefab, panelContenedor);
            itemTienda.ConfigurarItemVenta(itemDisponibles[i]);
        }
    }
 }