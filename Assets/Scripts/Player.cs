using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public float Speed;
  public float JumpForce;

  public bool isJumping;
  public bool doubleJump;

  private Rigidbody2D rig;
  private Animator anim;

  bool isBlowing;

  // Start is called before the first frame update
  void Start()
  {
    rig = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    Move();
    Jump();
  }

  // Movimento do personagem
  void Move()
  {
    // Move o personagem em uma posição sem fisica
    /* Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
    transform.position += movement * Time.deltaTime * Speed; */

    float movement = Input.GetAxis("Horizontal");

    rig.velocity = new Vector2(movement * Speed, rig.velocity.y);

    if (movement > 0f)
    {
      anim.SetBool("walk", true);
      transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }
    else if (movement < 0f)
    {
      anim.SetBool("walk", true);
      transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }
    else
    {
      anim.SetBool("walk", false);
    }
  }

  // Pulo do personagem
  void Jump()
  {
    if (Input.GetButtonDown("Jump") && !isBlowing)
    {
      if (!isJumping)
      {
        rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        doubleJump = true;
        anim.SetBool("jump", true);
      }
      else
      {
        if (doubleJump)
        {
          rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
          doubleJump = false;
        }
      }
    }
  }

  // Detecta se o Player está colidindo com o ground
  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.layer == 8)
    {
      isJumping = false;
      anim.SetBool("jump", false);
    }

    if (collision.gameObject.tag == "Spike")
    {
      GameController.instance.ShowGameOver();
      Destroy(gameObject);
    }

    if (collision.gameObject.tag == "Saw")
    {
      GameController.instance.ShowGameOver();
      Destroy(gameObject);
    }
  }

  // Detecta se o Player parou de colidir com o ground
  void OnCollisionExit2D(Collision2D collision)
  {
    if (collision.gameObject.layer == 8)
    {
      isJumping = true;
    }
  }

  void OnTriggerStay2D(Collider2D collider)
  {
    if (collider.gameObject.layer == 11)
    {
      isBlowing = true;
    }
  }

  void OnTriggerExit2D(Collider2D collider)
  {
    if (collider.gameObject.layer == 11)
    {
      isBlowing = false;
    }
  }
}
