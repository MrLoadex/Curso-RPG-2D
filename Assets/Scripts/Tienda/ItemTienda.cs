using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTienda : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombreTMP;
    [SerializeField] private TextMeshProUGUI itemCostoTMP;
    [SerializeField] private TextMeshProUGUI cantidadPorComprarTMP;

    private int cantidad = 1;
    private int costoInicial;
    private int costoActual;

    public ItemVenta ItemCargado { get; private set; }

    public void ConfigurarItemVenta(ItemVenta itemVenta)
    {
        ItemCargado = itemVenta;
        itemIcono.sprite = itemVenta.Item.Icono;
        itemNombreTMP.text= itemVenta.Item.Nombre;
        itemCostoTMP.text= itemVenta.CostoBase.ToString();
        //Configurar costos
        costoInicial = itemVenta.CostoBase;
        costoActual = itemVenta.CostoBase;
    }

    private void Update() 
    {
        cantidadPorComprarTMP.text = cantidad.ToString();
        itemCostoTMP.text = costoActual.ToString();
    }

    public void ComprarItem()
    {
        if (MonedasManager.Instance.MonedasTotales <= costoActual)
        {
            return;
        }

        Inventario.Instance.AñadirItem(ItemCargado.Item, cantidad);
        MonedasManager.Instance.RemoverMonedas(costoActual);
        cantidad = 1;
        costoActual = costoInicial;
    }

    public void SumarItemPorComprar()
    {
        int costoDeCompra = costoInicial * (cantidad + 1);
        if (MonedasManager.Instance.MonedasTotales >=  costoDeCompra)
        {
            cantidad ++;
            costoActual = costoInicial * cantidad;
        }
    }

    public void RestarItemPorComprar()
    {
        if (cantidad == 1)
        {
            return;
        }
        cantidad --;
        costoActual = costoInicial * cantidad;  
    }
}
