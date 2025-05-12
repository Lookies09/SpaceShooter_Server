using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMovement : DirectionMovement
{    
    protected Vector2 screenBounds;

    protected SpriteRenderer spriteRenderer;

    protected float shipSizeX;
    protected float shipSizeY;


    // Start is called before the first frame update
    protected void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        shipSizeX = spriteRenderer.bounds.extents.x;
        shipSizeY = spriteRenderer.bounds.extents.y;
    }

    protected void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(h, v).normalized;

        transform.Translate(direction * speed * Time.deltaTime);

    }

    private void LateUpdate()
    {
        Vector2 shipPos = transform.position;

        shipPos.x = Mathf.Clamp(shipPos.x, screenBounds.x * -1 + shipSizeX, screenBounds.x - shipSizeX);
        shipPos.y = Mathf.Clamp(shipPos.y, screenBounds.y * -1 + shipSizeY, screenBounds.y - shipSizeY);

        transform.position = shipPos;
    }

}
