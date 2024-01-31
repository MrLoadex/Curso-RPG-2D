using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Quest")]
    [SerializeField] private Quest[] questDisponibles;
    

    [Header("Npc Quest")]
    [SerializeField] private NPCQuestDescripcion inspectorQuestPrefab;
    [SerializeField] private Transform inspectorQuestContenedor;
    
    [Header("Personaje Quest")]
    [SerializeField] private PersonajeQuestDescripcion personajeQuestPrefab;
    [SerializeField] private Transform personajeQuestContenedor;

    [Header("Panel Quest Completado")]
    [SerializeField] private GameObject panelQuestCompletado;
    [SerializeField] private TextMeshProUGUI questNombre;
    [SerializeField] private TextMeshProUGUI questRecompensaOro;
    [SerializeField] private TextMeshProUGUI questRecompensaExp;
    [SerializeField] private TextMeshProUGUI questRecompensaItemCantidad;
    [SerializeField] private Image questRecompensaItemIcono;

    public Quest QuestPorReclamar { get; private set; }

    private void Start() 
    {
        CargarQuestNPCs();
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            AñadirProgreso("Mata10", 1);
            AñadirProgreso("Mata25", 1);
            AñadirProgreso("Mata50", 1);
        }
    }

    private void CargarQuestNPCs()
    {
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            NPCQuestDescripcion nuevoQuest = Instantiate(inspectorQuestPrefab ,inspectorQuestContenedor);
            nuevoQuest.ConfigurarQuestUI(questDisponibles[i]);
        }
    }

    public void ReclamarRecompensa()
    {
        if(QuestPorReclamar == null)
        {
            return;
        }
        MonedasManager.Instance.AñadirMonedas(QuestPorReclamar.RecompensaOro);
        Personaje.Instance.PersonajeExperiencia.AñadirExperiencia(QuestPorReclamar.RecompensaExp);
        Inventario.Instance.AñadirItem(QuestPorReclamar.RecompensaItem.item, QuestPorReclamar.RecompensaItem.Cantidad);
        panelQuestCompletado.SetActive(false);
        QuestPorReclamar = null;
    }

    private void AñadirQuestPorCompletar(Quest questPorCompletar)
    {
        PersonajeQuestDescripcion nuevoQuest = Instantiate(personajeQuestPrefab, personajeQuestContenedor);
        nuevoQuest.ConfigurarQuestUI(questPorCompletar);
    }

    public void AñadirQuest(Quest questPorCompletar)
    {
        AñadirQuestPorCompletar(questPorCompletar);
    }

    public void AñadirProgreso(string questID, int cantidad)
    {
        Quest questPorActualizar = VerificarExistenciaQuest(questID);
        if (questPorActualizar == null)
        {
            return;
        }
        
        questPorActualizar.AñadirProgreso(cantidad);
    }

    private Quest VerificarExistenciaQuest(string questID)
    {
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            if(questDisponibles[i].ID == questID)
            {
                return questDisponibles[i];
            }
        }
        return null;
    }

    private void MostrarQuestCompletado(Quest questCompletado)
    {
        panelQuestCompletado.gameObject.SetActive(true);
        questNombre.text = questCompletado.Nombre;
        questRecompensaOro.text = questCompletado.RecompensaOro.ToString();
        questRecompensaExp.text = questCompletado.RecompensaExp.ToString();
        questRecompensaItemCantidad.text = questCompletado.RecompensaItem.Cantidad.ToString();
        questRecompensaItemIcono.sprite = questCompletado.RecompensaItem.item.Icono;
    }

    private void ResponderQuestCompletado(Quest questCompletado)
    {
        QuestPorReclamar = VerificarExistenciaQuest(questCompletado.ID);
        if(QuestPorReclamar == null)
        {
            return;
        }
        MostrarQuestCompletado(QuestPorReclamar);
    }

    private void OnEnable() 
    {
        Quest.EventoQuestCompletado += ResponderQuestCompletado;
    }

    private void OnDisable() 
    {
        Quest.EventoQuestCompletado += ResponderQuestCompletado;
    }

}
