using UnityEngine;

public class SimpleSpriteParallax : MonoBehaviour
{
    [Header("Settings")]
    public float scrollSpeed = 2f;
    public bool isMoving = true;

    private SpriteRenderer spriteRenderer;
    private float spriteWidth;
    private Vector3 startPosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.bounds.size.x;
        startPosition = transform.position;
    }

    void Update()
    {
        if (!isMoving) return;

        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x <= startPosition.x - spriteWidth)
        {
            transform.position = startPosition;
        }
    }
}