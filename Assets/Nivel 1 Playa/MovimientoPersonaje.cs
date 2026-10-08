using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    public float velocidad = 5f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // GetAxisRaw devuelve 1, -1 o 0 exactos, ideal para 2D
        float movY = Input.GetAxisRaw("Vertical");
        float movX = Input.GetAxisRaw("Horizontal");

        // Movimiento
        transform.Translate(new Vector3(movX, movY, 0) * velocidad * Time.deltaTime);

        // Control de animación
        if (movX != 0 || movY != 0)
        {
            anim.SetBool("caminando", true);
        }
        else
        {
            anim.SetBool("caminando", false);
        }
    }
}