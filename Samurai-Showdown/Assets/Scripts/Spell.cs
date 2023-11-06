using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private Vector2 dimensionBox;
    [SerializeField] private Transform positionBox;
    [SerializeField] private float lifeTime;
    public AudioClip Invocar;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);   
    }

    // Update is called once per frame
    public void Tentacle()
    {
        Collider2D[] objetos = Physics2D.OverlapBoxAll(positionBox.position, dimensionBox, 0f);
        
        AudioManager.Instance.PlaySound(Invocar);

        foreach (Collider2D collision in objetos)
        {
            if(collision.CompareTag("Player"))
            {
                GameManagerScript.Instance.PerderVida();

                collision.GetComponent<CharacterController>().AplicarGolpe();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(positionBox.position, dimensionBox);
    }
}
