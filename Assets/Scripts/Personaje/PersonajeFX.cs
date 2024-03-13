using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoPersonaje
{
    Player,
    IA
}

public class PersonajeFX : MonoBehaviour
{

    [Header("Pooler")]
    [SerializeField] private ObjectPooler pooler;
    
    [Header("Configuracion")]
    [SerializeField] private GameObject canvasTextoAnimacionPrefab;
    [SerializeField] private Transform canvasTextoPosicion; 

    [Header("Tipo")]
    [SerializeField] private  TipoPersonaje tipoPersonaje;

    void Start()
    {
        pooler.CrearPooler(canvasTextoAnimacionPrefab);
    }

    private IEnumerator IEMostrarTextoDaño(float cantidadDaño, Color color)
    {
        GameObject nuevoTextoGO = pooler.ObtenerInstancia();
        TextoAnimacion texto = nuevoTextoGO.GetComponent<TextoAnimacion>();
        texto.EstablecerTexto(cantidadDaño, color);
        nuevoTextoGO.transform.SetParent(canvasTextoPosicion);
        nuevoTextoGO.transform.position =  canvasTextoPosicion.position;
        nuevoTextoGO.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        nuevoTextoGO.SetActive(false);
        nuevoTextoGO.transform.SetParent(pooler.ListaContenedor.transform);

    }

    private void RespuestaEventoDañoRecibidoHaciaPlayer(float cantidadDaño)
    {
        if (tipoPersonaje == TipoPersonaje.Player)
        {            
            StartCoroutine(IEMostrarTextoDaño(cantidadDaño, Color.black));
        }
    }

    private void RespuestaDañoHaciaEnemigo(float daño)
    {
        if (tipoPersonaje == TipoPersonaje.IA)
        {            
            StartCoroutine(IEMostrarTextoDaño(daño, Color.red));
        }
    }

    private void OnEnable() 
    {
        IAController.EventoHacerDaño += RespuestaEventoDañoRecibidoHaciaPlayer;
        PersonajeAtaque.EventoEnemigoDañado += RespuestaDañoHaciaEnemigo;
    }

    private void OnDisable() 
    {
        IAController.EventoHacerDaño -= RespuestaEventoDañoRecibidoHaciaPlayer;
        PersonajeAtaque.EventoEnemigoDañado -= RespuestaDañoHaciaEnemigo;
        
    }
}
