using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHandler : MonoBehaviour
{
    private List<GameObject> models;
    [SerializeField]
    private GameObject defaultSpawnObject;

    // Start is called before the first frame update
    void Start()
    {
        GameControllerObject.GetGCO().SetDisplayerReference(this);
    }

    public void ClearModels()
    {
        while(models.Count > 0)
        {
            Destroy(models[0]);
            models.RemoveAt(0);
        }
    }

    public void SpawnModels(GameEntity[] entities)
    {
        for(int i = 0; i < entities.Length; ++i)
        {
            GameObject model = Instantiate(defaultSpawnObject, new Vector3(entities[i].XPos * Data.displayScale, entities[i].YPos * Data.displayScale)
                , Quaternion.Euler(0, 0, 90 * ((int)entities[i].Facing)));
            model.GetComponent<SpriteRenderer>().sprite = entities[i].Sprite;
            model.GetComponent<EntityObject>().SetEntity(entities[i]);
            models.Add(model);
        }
    }
}
