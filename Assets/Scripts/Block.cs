using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public GameObject vfxFail;

    public GameObject vfxSuccess;

    private Vector3 startPos;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Level"))
        {
            if (spriteRenderer.sprite == GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].sprite)
            {
                GameObject vfx = Instantiate(vfxSuccess, transform.position, Quaternion.identity) as GameObject;
                Destroy(vfx, 1f);
                GetComponent<DragAndDrop>()._dragging = false;
                transform.position = GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].targets[0].position;

                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

                GameManager.Instance.CheckLevelUp();
            }
            else
            {
                ResetPos();
            }
        }
    }

    public void ResetPos()
    {
        GameObject vfx = Instantiate(vfxFail, transform.position, Quaternion.identity) as GameObject;
        Destroy(vfx, 1f);
        GetComponent<DragAndDrop>()._dragging = false;
        transform.position = startPos;
    }

}
