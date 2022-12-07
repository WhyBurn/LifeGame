using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHandler : MonoBehaviour
{
    private List<GameObject> models;
    [SerializeField]
    private GameObject defaultSpawnObject;
    [SerializeField]
    private GameObject codePanel;
    [SerializeField]
    private Transform codeContent;

    // Start is called before the first frame update
    void Start()
    {
        models = new List<GameObject>();
        GameControllerObject.GetGCO().SetDisplayerReference(this);
        codePanel.SetActive(false);
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

    public void ClearCodeContent()
    {
        for (int i = 0; i < codeContent.childCount; ++i)
        {
            Destroy(codeContent.GetChild(i).gameObject);                
        }
    }

    public void DisplayCode(GameObject[] lines)
    {
        ClearCodeContent();
        for(int i = 0; i < lines.Length; ++i)
        {
            lines[i].transform.SetParent(codeContent);
        }
        codePanel.SetActive(true);
    }
}
