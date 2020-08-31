using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
  private SpriteRenderer spriteRenderer;
  private CircleCollider2D circleCollider;

  public GameObject collected;
  public int score;

  // Start is called before the first frame update
  void Start()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    circleCollider = GetComponent<CircleCollider2D>();
  }

  void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.gameObject.tag == "Player")
    {
      // Quando o Player tocar na Apple desativa o sr e circle e ativa a fumaça collected
      spriteRenderer.enabled = false;
      circleCollider.enabled = false;
      collected.SetActive(true);
      // Quando o Player tocar na Apple soma ao score
      GameController.instance.totalScore += score;
      GameController.instance.UpdateScoreText();
      Destroy(gameObject, 0.25f);
    }
  }
}
