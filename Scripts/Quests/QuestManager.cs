using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
