using Unity.Mathematics;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float velocidad;

    Rigidbody2D _rigidbody2D;
    Vector2 direccion;
    EnemigoInteraccion enemigoObjetivo;

    private void Awake() 
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if(enemigoObjetivo == null) { return; }

        MoverProyectil();
    }

    private void MoverProyectil()
    {
        //Obtener la direcicon del enemigo
        direccion = enemigoObjetivo.transform.position - transform.position;

        //Obtener el angulo
        float angulo = Mathf.Atan2(direccion.y,direccion.x) * Mathf.Rad2Deg;

        //Rotamos el objeto
        // Crea un cuaternión que representa una rotación alrededor del eje Z en el espacio local del objeto.
        transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
        _rigidbody2D.MovePosition(_rigidbody2D.position + direccion.normalized * velocidad * Time.fixedDeltaTime);
    }

    public void InicializarProyectil(EnemigoInteraccion enemigo)
    {
        enemigoObjetivo = enemigo;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemigo"))
        {
            gameObject.SetActive(false);
        }
    }

}
