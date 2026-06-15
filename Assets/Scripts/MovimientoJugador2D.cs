using UnityEngine;
// Hola desconocido ;) soy Sthifer o stiffler como me llamo en multiples juegos
// Este es el codigo y tiene multiples explicaciones lo mas simples posibles para que todo adulto y niño entienda lo que hace 
// chaoooooooo
public class MovimientoJugador2D : MonoBehaviour
{
    [Header("Componentes")]
    // Este es el Rigidbody2D del jugador.
    // Sirve para que el personaje use físicas, gravedad y movimiento.
    [SerializeField] private Rigidbody2D rb2d;

    [Header("Movimiento")]
    // Esta es la velocidad con la que el jugador camina.
    [SerializeField] private float velocidadMovimiento = 6f;

    // Aquí se guarda si el jugador va a la izquierda, derecha o está quieto.
    private float entradaHorizontal;

    [Header("Salto")]
    // Esta es la fuerza con la que el jugador salta.
    [SerializeField] private float fuerzaSalto = 12f;

    // Este punto va en los pies del jugador.
    // Se usa para revisar si está tocando el suelo.
    [SerializeField] private Transform controladorSuelo;

    // Este es el tamaño de la cajita invisible que detecta el suelo.
    [SerializeField] private Vector2 dimensionesCaja = new Vector2(0.7f, 0.1f);

    // Aquí se coloca la capa del suelo y las plataformas.
    // Así el jugador sabe qué cosas cuentan como suelo.
    [SerializeField] private LayerMask capasSuelo;

    // Esta variable dice si el jugador está en el suelo o no.
    private bool enSuelo;

    // Esta variable guarda cuando el jugador presiona la tecla de salto.
    private bool entradaSalto;

    private void Awake()
    {
        // Aquí buscamos el Rigidbody2D del jugador automáticamente.
        // Así no hay que ponerlo a mano si está en el mismo objeto.
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Aquí se lee si el jugador presiona A/D o las flechas.
        // -1 es izquierda, 1 es derecha y 0 es quieto.
        entradaHorizontal = Input.GetAxisRaw("Horizontal");

        // Aquí se revisa si el jugador presionó la barra espaciadora.
        if (Input.GetButtonDown("Jump"))
        {
            entradaSalto = true;
        }
        // Aquí se revisa si los pies del jugador están tocando el suelo.
        // Esto evita que el jugador salte en el aire muchas veces.
        enSuelo = Physics2D.OverlapBox(
            controladorSuelo.position,
            dimensionesCaja,
            0f,
            capasSuelo
        );
    }

    private void FixedUpdate()
    {
        // Aquí movemos al jugador hacia los lados.
        // Se cambia la velocidad en X, pero se deja igual la velocidad en Y.
        rb2d.linearVelocity = new Vector2(
            entradaHorizontal * velocidadMovimiento,
            rb2d.linearVelocity.y
        );

        // Aquí hacemos que el jugador salte.
        // Solo puede saltar si presionó salto y está tocando el suelo.
        if (entradaSalto && enSuelo)
        {
            rb2d.linearVelocity = new Vector2(
                rb2d.linearVelocity.x,
                fuerzaSalto
            );
        }

        // Aquí apagamos la entrada de salto.
        // Esto evita que el salto se repita solo.
        entradaSalto = false;
    }
    private void OnDrawGizmosSelected()
    {
        // Esto dibuja una cajita amarilla en los pies del jugador.
        // Sirve para ver en la escena dónde se está detectando el suelo.
        if (controladorSuelo != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
        }
    }
}