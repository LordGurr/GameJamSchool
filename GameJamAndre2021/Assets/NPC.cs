using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private PlayerMovement player;
    private bool moving = false;
    private float moveTime = 0.4f;
    private float distance = 2;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private enum Direction
    {
        left, right, up, down
    };

    // Update is called once per frame
    private void Update()
    {
        if (!player.moving && !moving)
        {
            if (Vector2.Distance(player.transform.position, transform.position) <= distance)
            {
                Vector3 temp = player.transform.position - transform.position;
                Direction directionToMove = DirectionToMove(temp);
                StartCoroutine(Move(directionToMove));
            }
            else
            {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
            }
        }
    }

    private Direction DirectionToMove(Vector3 temp)
    {
        float max = (temp.x);
        int index = 0;
        if (Mathf.Abs(temp.y) > Mathf.Abs(max))
        {
            max = (temp.y);
            index = 1;
        }
        if (index == 0)
        {
            if (max < 0)
            {
                return Direction.right;
            }
            return Direction.left;
        }
        else if (index == 1)
        {
            if (max < 0)
            {
                return Direction.up;
            }
        }
        return Direction.down;
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