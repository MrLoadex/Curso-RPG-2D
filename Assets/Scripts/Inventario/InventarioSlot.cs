
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TipoDeInteraccion
{
    Click,
    Usar,
    Equipar,
    Remover
}

public class InventarioSlot : MonoBehaviour
{
    public static Action<TipoDeInteraccion, int> EventoSlotInteraccion; 

    [SerializeField] private Image itemIcono;
    [SerializeField] private GameObject fondoCantidad;
    [SerializeField] private TextMeshProUGUI cantidadTMP;

    public int Index { get; set; }

    private Button _boton;

    private void Awake() 
    {
        _boton = GetComponent<Button>();    
    }

    public void ActualizarSlot(InventarioItem item, int cantidad)
    {
        itemIcono.sprite = item.Icono;
        cantidadTMP.text = cantidad.ToString();
    }

    public void ActivarSlotUI(bool estado)
    {
        itemIcono.gameObject.SetActive(estado);
        fondoCantidad.SetActive(estado);
    }

    public void SeleccionarSlot()
    {
        _boton.Select();
    }

    public void ClickSlot()
    {
        EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Click,Index);

        //Mover Item
        if (InventarioUI.Instance.IndexSlotInicialPorMover != -1)
        {
            if(InventarioUI.Instance.IndexSlotInicialPorMover != Index)
            {
                Inventario.Instance.MoverItem(InventarioUI.Instance.IndexSlotInicialPorMover, Index);
            }
        }
    }

    public void SlotUsarItem()
    {
        if (Inventario.Instance.ItemsInventario[Index] != null)
        {
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Usar, Index);
        }
    }

    public void SlotEquiparItem()
    {
        if(Inventario.Instance.ItemsInventario[Index] != null)
        {
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Equipar, Index);
        }
    }

    public void SlotRemoverItem()
    {
        if(Inventario.Instance.ItemsInventario[Index] != null)
        {
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Remover, Index);
        }
    }

}
