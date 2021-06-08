using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public bool moving { private set; get; }
    private float moveTime = 0.5f;

    private enum Direction
    {
        left, right, up, down
    };

    private void Start()
    {
        moving = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!moving)
        {
            if (Input.GetAxis("Horizontal") > 0.1f)
            {
                StartCoroutine(Move(Direction.right));
            }
            else if (Input.GetAxis("Horizontal") < -0.1f)
            {
                StartCoroutine(Move(Direction.left));
            }
            else if (Input.GetAxis("Vertical") < -0.1f)
            {
                StartCoroutine(Move(Direction.down));
            }
            else if (Input.GetAxis("Vertical") > 0.1f)
            {
                StartCoroutine(Move(Direction.up));
            }
            else
            {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
            }
        }
    }

    private IEnumerator Move(Direction direction)
    {
        moving = true;
        float time = 0;

        Vector3 originalPos = transform.position;
        while (time < moveTime)
        {
            time += Time.deltaTime;

            float offset = Mathf.Clamp(time / moveTime, 0, 1);
            if (direction == Direction.right)
            {
                transform.position = new Vector3(offset + originalPos.x, Equation(offset) + originalPos.y, transform.position.z);
            }
            else if (direction == Direction.left)
            {
                transform.position = new Vector3(-offset + originalPos.x, Equation(offset) + originalPos.y, transform.position.z);
            }
            else if (direction == Direction.up)
            {
                transform.position = new Vector3(transform.position.x, offset + originalPos.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, -offset + originalPos.y, transform.position.z);
            }
            yield return null;
        }
        moving = false;
    }

    private float Equation(float x)
    {
        return -1.6f * Mathf.Pow(x - 0.5f, 2) + 0.4f;
    }
}