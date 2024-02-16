using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventarioUI : Singleton<InventarioUI>
{
    [Header("Panel Inventario Descripcion")]
    [SerializeField] private GameObject panelInventarioDescripcion;
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemDescripcion;

    //[Header("")]
    [SerializeField] private InventarioSlot slotPrefab;
    [SerializeField] private Transform contenedor;

    public int IndexSlotInicialPorMover{get; private set;}
    public InventarioSlot slotSeleccionado { get; private set; }
    private List<InventarioSlot> slotsDisponibles = new List<InventarioSlot>();

    // Start is called before the first frame update
    void Start()
    {
        InicializarInventario();
        IndexSlotInicialPorMover = -1;
    }

    private void Update() 
    {
        ActualizarSlotSeleccionado();
        if (Input.GetKeyDown(KeyCode.M))
        {
            IndexSlotInicialPorMover = slotSeleccionado.Index;
        }
    }

    private void InicializarInventario()
    {
        for (int i = 0; i < Inventario.Instance.NumeroDeSlots; i++)
        {
            InventarioSlot nuevoSlot = Instantiate(slotPrefab,contenedor);
            nuevoSlot.Index = i;
            slotsDisponibles.Add(nuevoSlot);
        }

    }
    
    private void ActualizarSlotSeleccionado()
    {
        GameObject goSeleccionado = EventSystem.current.currentSelectedGameObject;
        if (goSeleccionado == null)
        {
            return;
        }

        InventarioSlot slot =  goSeleccionado.GetComponent<InventarioSlot>();
        if (slot != null)
        {
            slotSeleccionado = slot;
        }
    }

    public void DibujarItemEnInventario(InventarioItem itemPorAñadir, int cantidad, int itemIndex)
    {
        InventarioSlot slot = slotsDisponibles[itemIndex];

        if (itemPorAñadir != null)
        {
            slot.ActivarSlotUI(true);
            slot.ActualizarSlot(itemPorAñadir, cantidad);
        }
        else
        {
            slot.ActivarSlotUI(false);
        }
    }

    private void ActualizarInventarioDescripcion(int index)
    {
        if (Inventario.Instance.ItemsInventario[index] != null)
        {
            itemIcono.sprite = Inventario.Instance.ItemsInventario[index].Icono;
            itemNombre.text = Inventario.Instance.ItemsInventario[index].Nombre;
            itemDescripcion.text = Inventario.Instance.ItemsInventario[index].Descripcion;
            panelInventarioDescripcion.SetActive(true);
        }
        else
        {
            panelInventarioDescripcion.SetActive(false);
        }
    }
    
    public void UsarItem()
    {
        if(slotSeleccionado != null)
        {
            slotSeleccionado.SlotUsarItem();
            slotSeleccionado.SeleccionarSlot();
        }
    }

    public void EquiparItem()
    {
        if(slotSeleccionado != null)
        {
            slotSeleccionado.SlotEquiparItem();
            slotSeleccionado.SeleccionarSlot();
        }
    }

    public void RemoverItem()
    {
        if(slotSeleccionado != null)
        {
            slotSeleccionado.SlotRemoverItem();
            slotSeleccionado.SeleccionarSlot();
        }
    }


    #region Evento
    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int index)
    {
        if (tipo == TipoDeInteraccion.Click)
        {
            ActualizarInventarioDescripcion(index);
        }
        else if (tipo == TipoDeInteraccion.Equipar)
        {

        }
    }

    private void OnEnable() 
    {
        InventarioSlot.EventoSlotInteraccion += SlotInteraccionRespuesta;
    }

    private void OnDisable() 
    {
        InventarioSlot.EventoSlotInteraccion -= SlotInteraccionRespuesta;
    }
    #endregion

}
