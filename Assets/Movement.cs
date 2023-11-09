using Unity.VisualScripting;
using UnityEngine;

public class MovimientoProyectil : MonoBehaviour
{
    public float fuerzaInicial = 10f; // Fuerza inicial del proyectil.
    public float anguloLanzamiento = 45f; // Ángulo de lanzamiento en grados.
    private Rigidbody rb;
    private float anguloRad;
    private Vector3 posicionInicial;
    private float tiempo = 0f;
    private Renderer objetoRenderer;
    public GameObject original;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anguloRad = anguloLanzamiento * Mathf.Deg2Rad;
        objetoRenderer = GetComponent<Renderer>();

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LanzarProyectil();
            Copia();
        }
    }

    private void LanzarProyectil()
    {
        tiempo = 0f;
        rb.velocity = fuerzaInicial * new Vector3(Mathf.Cos(anguloRad), Mathf.Sin(anguloRad), 0);

        
    }

    private void FixedUpdate()
    {
        if (tiempo > 0)
        {
            MoverProyectil();
        }
    }

    private void MoverProyectil()
    {
        float gravedad = Physics.gravity.y;
        Vector3 nuevaPosicion = posicionInicial + tiempo * rb.velocity;
        nuevaPosicion.y = posicionInicial.y + rb.velocity.y * tiempo + (0.5f * gravedad * tiempo * tiempo);
        rb.MovePosition(nuevaPosicion);
        tiempo += Time.fixedDeltaTime;

        // Comprobar si el proyectil ha llegado a su destino.
        if (tiempo > 0 && tiempo >= (2 * rb.velocity.y / Mathf.Abs(gravedad)))
        {
            rb.isKinematic = false; // Hacer que el Rigidbody deje de ser cinemático para mostrar el objeto.
            rb.detectCollisions = true; // Reactivar la detección de colisiones.
        }
    }

    void Copia()
    {
        GameObject copia = Instantiate(original, transform.position, Quaternion.identity);
        //Collider copiaCollider = copia.GetComponent<Collider>();
        Renderer copiaRenderer = copia.GetComponent<Renderer>();
        copiaRenderer.enabled = false;
        
    }

}
