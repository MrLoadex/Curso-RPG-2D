using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TiposDeAtaque
{
    Melee,
    Embestida
}

public class IAController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] Personaje personaje;
    private PersonajeStats stats;

    [Header("Estados")]
    [SerializeField] private IAEstado estadoInicial;
    [SerializeField] private IAEstado estadoDefault;

    [Header("Config")]
    [SerializeField] private float rangoDeteccion;
    [SerializeField] private float rangoDeAtaque;
    [SerializeField] private float rangoDeEmbestida;
    [SerializeField] private float velocidadEmbestida;
    [SerializeField] private LayerMask personajeLayerMask;
    [SerializeField] private float velocidadMovimiento;
    
    [Header("Ataque")]
    [SerializeField] float daño;
    [SerializeField] float tiempoEntereAtaques;
    [SerializeField] TiposDeAtaque tipoAtaque;
    
    [Header("Debug")]
    [SerializeField]private bool mostrarDeteccion;
    [SerializeField]private bool mostrarRangoAtaque;

    private float _tiempoParaSiguienteAtaque;
    private BoxCollider2D _boxCollider2D;

    public Transform PersonajeReferencia { get; set; }
    public IAEstado EstadoActual { get; set; }
    public EnemigoMovimiento EnemigoMovimiento { get; set; }
    public float RangoDeteccion => rangoDeteccion;
    public float VelocidadMovimiento => velocidadMovimiento;
    public LayerMask PersonajeLayerMask => personajeLayerMask;
    public float Daño => daño;
    public TiposDeAtaque TipoAtaque => tipoAtaque;
    public float RangoDeAtaqueDeterminado => tipoAtaque == TiposDeAtaque.Embestida ? rangoDeEmbestida : rangoDeAtaque;

    private void Start() 
    {   
        _boxCollider2D = GetComponent<BoxCollider2D>();
        EstadoActual = estadoInicial;
        EnemigoMovimiento = GetComponent<EnemigoMovimiento>();
        stats = personaje.PersonajeStats;
    }

    private void Update() 
    {
        EstadoActual.EjecutarEstado(this);
    }

    public void CambiarEstado(IAEstado nuevoEstado)
    {
        if (nuevoEstado != estadoDefault)
        {
            EstadoActual = nuevoEstado;
        }

    }

    public void AtaqueMele(float cantidad)
    {
        if (PersonajeReferencia == null)
        {
            return;
        }

        AplicarDañoAlPersonaje(cantidad);
    }

    private IEnumerator IEEmbestida(float cantidadDaño)
    {
        Vector3 personajePosicion = PersonajeReferencia.position;
        Vector3 posicionInicial = transform.position;
        Vector3 direccionHaciaPersonaje = (personajePosicion - posicionInicial).normalized;
        Vector3 posicionDeAtaque = personajePosicion - direccionHaciaPersonaje * 0.5f;
        _boxCollider2D.enabled = false;

        float transicionDeAtaque = 0;
        while(transicionDeAtaque <= 1f)
        {
            transicionDeAtaque += Time.deltaTime * velocidadMovimiento;
            float interpolacion = (-Mathf.Pow(transicionDeAtaque,2) + transicionDeAtaque) * 4f;
            transform.position = Vector3.Lerp(posicionInicial, posicionDeAtaque, interpolacion);
            yield return null;
        }

        if (PersonajeReferencia != null)
        {
            AplicarDañoAlPersonaje(cantidadDaño);
        }

        _boxCollider2D.enabled = true;
    }

    public void AtaqueEmbestida(float cantidad)
    {
        StartCoroutine(IEEmbestida(cantidad));
    }

    public void AplicarDañoAlPersonaje(float cantidad)
    {
        float dañoPorRealizar = 0;
        //Verificar si pasa el bloqueo..
        if(Random.value < stats.PorcentajeBloqueo / 100)
        {
            return;
        }
        //Se le resta la defensa al daño a realizar (con un minimo a de 1 de daño por realizar)
        dañoPorRealizar = Mathf.Max(cantidad - stats.Defensa, 1f);

        //Se le hace daño al personaje
        PersonajeReferencia.GetComponent<PersonajeVida>().RecibirDaño(dañoPorRealizar);
    }

    public bool PersonajeEnRangoDeAtaque(float rango)
    {
        float distanciaHaciaPersonaje = (PersonajeReferencia.position - transform.position).sqrMagnitude;
        if(distanciaHaciaPersonaje < Mathf.Pow(rango, 2))
        {
            return true;
        }
        return false;
    }

    public bool EsTiempoDeAtacar()
    {
        if(Time.time > _tiempoParaSiguienteAtaque)
        {
            return true;
        }
        return false;
    }

    public void ActualizarTiempoEntreAtaques()
    {
        _tiempoParaSiguienteAtaque = Time.time + tiempoEntereAtaques;
    }

    private void OnDrawGizmos() {
        if(mostrarDeteccion)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
        }

        if(mostrarRangoAtaque)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, RangoDeAtaqueDeterminado);
        }
    }

}
