using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeFX : MonoBehaviour
{

    [SerializeField] private GameObject canvasTextoAnimacionPrefab;
    [SerializeField] private Transform canvasTextoPosicion; 

    private ObjectPooler pooler;

    private void Awake() {
        pooler = GetComponent<ObjectPooler>();
    }
    
    void Start()
    {
        pooler.CrearPooler(canvasTextoAnimacionPrefab);
    }

    private IEnumerator IEMostrarTextoDaño(float cantidadDaño)
    {
        GameObject nuevoTextoGO = pooler.ObtenerInstancia();
        TextoAnimacion texto = nuevoTextoGO.GetComponent<TextoAnimacion>();
        texto.EstablecerTexto(cantidadDaño);
        nuevoTextoGO.transform.SetParent(canvasTextoPosicion);
        nuevoTextoGO.transform.position =  canvasTextoPosicion.position;
        nuevoTextoGO.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        nuevoTextoGO.SetActive(false);
        nuevoTextoGO.transform.SetParent(pooler.ListaContenedor.transform);

    }

    private void RespuestaEventoDañoRecibido(float cantidadDaño)
    {
        StartCoroutine(IEMostrarTextoDaño(cantidadDaño));
    }

    private void OnEnable() 
    {
        IAController.EventoHacerDaño += RespuestaEventoDañoRecibido;
    }

    private void OnDisable() 
    {
        IAController.EventoHacerDaño -= RespuestaEventoDañoRecibido;
        
    }
}
