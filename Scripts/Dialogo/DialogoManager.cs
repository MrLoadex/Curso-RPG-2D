using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogoManager : Singleton<DialogoManager>
{
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private Image npcIcono;
    [SerializeField] private TextMeshProUGUI npcNombreTMP;
    [SerializeField] private TextMeshProUGUI npcConversacionTMP;
    
    public NPCInteraccion NPCDisponible { get; set; }

    private Queue<string> dialogosSecuencia;
    private bool dialogoAnimado;
    private bool despedidaMostrada;
    private bool dialogoComenzado;

    private void Start() 
    {
        dialogosSecuencia = new Queue<string>();
    }

    private void Update() 
    {
        if (NPCDisponible == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && !dialogoComenzado)
        {
            dialogoComenzado = true;
            ConfigurarPanel(NPCDisponible.Dialogo);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(despedidaMostrada)
            {
                AbrirCerrarPanelDialogo(estado: false);
                despedidaMostrada = false;
                return;
            }

            if(NPCDisponible.Dialogo.ContieneInteraccionExtra)
            {
                UIManager.Instance.AbrirPanelInteraccion(NPCDisponible.Dialogo.InteraccionExtra);
                AbrirCerrarPanelDialogo(false);
            }

            if (dialogoAnimado)
            {
                ContinuarDialogo();
            }
        }
   
    }

    private void ContinuarDialogo()
    {
        if (NPCDisponible == null)
        {
            return;
        }

        if (despedidaMostrada)
        {
            return;
        }

        if(dialogosSecuencia.Count == 0)
        {
            string despedida = NPCDisponible.Dialogo.Despedida;
            MostrarTextoConAnimacion(despedida);
            despedidaMostrada = true;
            dialogoComenzado = false;
            return;
        }

        string siguienteDialogo = dialogosSecuencia.Dequeue();
        MostrarTextoConAnimacion(siguienteDialogo);
    }

    public void AbrirCerrarPanelDialogo(bool estado)
    {
        panelDialogo.SetActive(estado);
    }

    private void ConfigurarPanel(NPCDialogo npcDialogo)
    {
        AbrirCerrarPanelDialogo(true);
        npcIcono.sprite = npcDialogo.Icono;
        npcNombreTMP.text = $"{npcDialogo.Nombre}:";
        CargarDialogosSecuencia(npcDialogo);
        MostrarTextoConAnimacion(npcDialogo.Saludo);
    }

    private void CargarDialogosSecuencia(NPCDialogo npcDialogo)
    {
        if (npcDialogo.Conversacion == null || npcDialogo.Conversacion.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < npcDialogo.Conversacion.Length; i++)
        {
            dialogosSecuencia.Enqueue(npcDialogo.Conversacion[i].Oracion);
        }
    }

    private IEnumerator AnimarTexto(string oracion)
    {
        dialogoAnimado = false;
        npcConversacionTMP.text = "";
        char[] letras = oracion.ToCharArray();
        for (int i = 0; i < letras.Length; i++)
        {
            npcConversacionTMP.text += letras[i];
            yield return new WaitForSeconds(0.03f);  
        }

        dialogoAnimado = true;
    }

    private void MostrarTextoConAnimacion(string oracion)
    {
        StartCoroutine(AnimarTexto(oracion));
    }
}
