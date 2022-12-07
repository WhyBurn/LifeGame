using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityObject : MonoBehaviour
{
    private GameEntity entity;
    private Vector3 targetPosition;

    public void Update()
    {
        gameObject.transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
    }

    public void SetEntity(GameEntity e)
    {
        entity = e;
        e.SetDisplayObject(this);
        targetPosition = new Vector3(e.XPos * Data.displayScale, e.YPos * Data.displayScale);
    }

    public void MoveTo(Vector2Int position, Data.Direction facing)
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 0, ((int)facing) * -90);
        targetPosition = new Vector3(position.x * Data.displayScale, position.y * Data.displayScale);
    }
}
