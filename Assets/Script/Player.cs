using System;
using UnityEngine;


public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;
    private Vector3 direction;
    public float gravity = -12f;
    public float strength = 5f;
    public bool canJump = true;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() 
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f , 0.15f);
    }
    
    private void OnEnable()
    {
        direction = Vector3.zero;
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            audioManager.PlaySFX(audioManager.flap);
            direction = Vector3.up * strength;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
          
            if (touch.phase == TouchPhase.Began){
                direction = Vector3.up * strength;
            }
        }

        direction.y += gravity * Time.deltaTime;
        if (canJump) // If bird can jump, then jump
        {
            transform.position += direction * Time.deltaTime;
        }
       
    }
    private void AnimateSprite()
    {
        spriteIndex++;
        if(spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;

        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    [Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    { 
     if( other.gameObject.tag == "Obstacle")
     {
       
       FindObjectOfType<GameManager>().GameOver();

     }else if(other.gameObject.tag == "Scoring")
     {
        FindObjectOfType<GameManager>().IncreaseScore();
     }
    }

    public void teleportMiddle()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
    }
}


