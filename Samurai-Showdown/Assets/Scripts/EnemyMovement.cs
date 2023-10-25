using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float moveSpeed = 0.5f;  // Velocidad de movimiento de los enemigos
    private Transform player;       // Referencia al transform del jugador

    void Start()
    {
        // Encuentra el objeto con la etiqueta "Player" (asegúrate de etiquetar al jugador como "Player" en Unity)
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // Calcula la dirección hacia la posición del jugador
            Vector3 moveDirection = player.position - transform.position;
            moveDirection.Normalize();

            // Invierte la dirección en el eje X si el enemigo está a la izquierda del jugador
            if (transform.position.x > player.position.x)
            {
                moveDirection.x = -moveDirection.x;
            }

            // Mueve al enemigo en la dirección del jugador
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}