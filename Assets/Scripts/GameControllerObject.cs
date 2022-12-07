using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerObject
{
    private static GameControllerObject gco;

    public static GameControllerObject GetGCO()
    {
        if(gco == null)
        {
            gco = new GameControllerObject();
        }
        return (gco);
    }

    private Map currentMap;
    private DisplayHandler displayer;
    private GameEntity[] entities;
    private CameraController cameraController;
    private Dictionary<string, GameEntity> assignedVariables;

    public GameControllerObject()
    {
        Data.LoadData();
        LoadMap(0);
    }

    public void LoadMap(int index)
    {
        assignedVariables = new Dictionary<string, GameEntity>();
        currentMap = Data.GetMap(index);
        entities = new GameEntity[currentMap.NumEntities];
        for(int i = 0; i < entities.Length; ++i)
        {
            entities[i] = currentMap.GetEntityStart(i).Entity.GetCopy();
            entities[i].Position = currentMap.GetEntityStart(i).StartPos;
        }
        SpawnModels();
        FocusCamera();
    }

    public void SetDisplayerReference(DisplayHandler d)
    {
        displayer = d;
        SpawnModels();
    }

    private void SpawnModels()
    {
        if(displayer != null)
        {
            displayer.ClearModels();
            displayer.SpawnModels(entities);
        }
    }

    public void SetCameraController(CameraController c)
    {
        cameraController = c;
        FocusCamera();
    }

    private void FocusCamera()
    {
        if(cameraController != null)
        {
            cameraController.FocusCamera(currentMap.Bounds);
        }
    }

    public void PlayerDirectionInput(Data.Direction direction)
    {
        bool didMove = false;
        for(int i = 0; i < entities.Length; ++i)
        {
            if(entities[i].Tag == "Player")
            {
                if(TryMove(entities[i], direction))
                {
                    didMove = true;
                }
            }
        }
        if(didMove)
        {
            for(int i = 0; i < entities.Length; ++i)
            {
                if(entities[i].Tag == "Computer")
                {
                    if(entities[i] is Computer c)
                    {
                        c.RunRules(Rule.RuleType.onMove);
                    }
                }
            }
        }
    }

    public void PlayerInteractInput()
    {
        for (int i = 0; i < entities.Length; ++i)
        {
            if (entities[i].Tag == "Computer")
            {
                if (entities[i] is Computer c)
                {
                    c.RunRules(Rule.RuleType.onInteract);
                }
            }
        }
        for(int i = 0; i < entities.Length; ++i)
        {
            if(entities[i].Tag == "Player")
            {
                for(int j = 0; j < entities.Length; ++j)
                {
                    if(entities[j].Tag == "Computer" && entities[i].Position == entities[j].Position)
                    {
                        DisplayCode(entities[j] as Computer);
                    }
                }
            }
        }
    }

    public void DisplayCode(Computer c)
    {
        GameObject[] codeObjects = c.GetCodeLines();
        displayer.DisplayCode(codeObjects);
    }

    public bool TryMove(GameEntity entity, Data.Direction direction)
    {
        Vector2Int pos = entity.Position;
        if(direction == Data.Direction.up)
        {
            pos.y++;
        }
        else if(direction == Data.Direction.right)
        {
            pos.x++;
        }
        else if(direction == Data.Direction.left)
        {
            pos.x--;
        }
        else if(direction == Data.Direction.down)
        {
            pos.y--;
        }
        if(pos.x < 0 || pos.y < 0 || pos.x >= currentMap.X || pos.y >= currentMap.Y)
        {
            return (false);
        }
        entity.Facing = direction;
        entity.Position = pos;
        return (true);
    }

    public bool TryShift(GameEntity entity, Data.Direction direction)
    {
        Vector2Int pos = entity.Position;
        if (direction == Data.Direction.up)
        {
            pos.y++;
        }
        else if (direction == Data.Direction.right)
        {
            pos.x++;
        }
        else if (direction == Data.Direction.left)
        {
            pos.x--;
        }
        else if (direction == Data.Direction.down)
        {
            pos.y--;
        }
        if (pos.x < 0 || pos.y < 0 || pos.x >= currentMap.X || pos.y >= currentMap.Y)
        {
            return(false);
        }
        entity.Position = pos;
        return (true);
    }

    public void RunForEachLoop(string tag, string variable, Statement loop)
    {
        foreach(GameEntity entity in entities)
        {
            if(entity.Tag == tag)
            {
                assignedVariables[variable] = entity;
                loop.Run();
            }
        }
    }

    public bool SameSpace(string left, string right)
    {
        bool anyLeft = false;
        string leftTag = left;
        if (left.Length > 3 && left.Substring(0, 3) == "any")
        {
            leftTag = left.Substring(3);
            anyLeft = true;
        }
        if(right.Length > 3 && right.Substring(0, 3) == "any")
        {
            string rightTag = right.Substring(3);
            foreach(GameEntity entity in entities)
            {
                if(entity.Tag == rightTag)
                {
                    if(anyLeft)
                    {
                        foreach(GameEntity leftEntity in entities)
                        {
                            if(leftEntity.Tag == leftTag)
                            {
                                if(leftEntity.Position == entity.Position)
                                {
                                    return (true);
                                }
                            }
                        }
                    }
                    else
                    {
                        GameEntity leftEntity = assignedVariables[left];
                        if(leftEntity != null && leftEntity.Position == entity.Position)
                        {
                            return (true);
                        }
                    }
                }
            }
        }
        else
        {
            GameEntity entity = assignedVariables[right];
            if(entity != null)
            {
                if (anyLeft)
                {
                    foreach (GameEntity leftEntity in entities)
                    {
                        if (leftEntity.Tag == leftTag)
                        {
                            if (leftEntity.Position == entity.Position)
                            {
                                return (true);
                            }
                        }
                    }
                }
                else
                {
                    GameEntity leftEntity = assignedVariables[left];
                    if (leftEntity != null && leftEntity.Position == entity.Position)
                    {
                        return (true);
                    }
                }
            }
        }
        return (false);
    }

    public void MoveEntity(string entityTag, string directionString)
    {
        if(entityTag.Length > 4 && entityTag.Substring(0, 4) == "each")
        {
            string tag = entityTag.Substring(4);
            foreach(GameEntity entity in entities)
            {
                if (entity.Tag == tag)
                {
                    MoveEntity(entity, directionString);
                }
            }
        }
        else
        {
            GameEntity entity = assignedVariables[entityTag];
            if (entity != null)
            {
                MoveEntity(entity, directionString);
            }
        }
    }

    public void MoveEntity(GameEntity entity, string directionString)
    {
        if (directionString == "Up")
        {
            TryMove(entity, Data.Direction.up);
        }
        else if (directionString == "Right")
        {
            TryMove(entity, Data.Direction.right);
        }
        else if (directionString == "Left")
        {
            TryMove(entity, Data.Direction.left);
        }
        else if (directionString == "Down")
        {
            TryMove(entity, Data.Direction.down);
        }
        else if (directionString == "Forward")
        {
            TryMove(entity, entity.Facing);
        }
        else if (directionString == "Backward")
        {
            TryMove(entity, (Data.Direction)((((int)entity.Facing) + 2) % 4));
        }
        else
        {
            string[] words = directionString.Split('.');
            if (words.Length == 2)
            {
                GameEntity directionSource = assignedVariables[words[0]];
                if (directionSource != null)
                {
                    if (words[1] == "Forward")
                    {
                        TryMove(entity, directionSource.Facing);
                    }
                    else if (words[1] == "Backward")
                    {
                        TryMove(entity, (Data.Direction)((((int)directionSource.Facing) + 2) % 4));
                    }
                }
            }
        }
    }

    public void ShiftEntity(string entityTag, string directionString)
    {
        if (entityTag.Length > 4 && entityTag.Substring(0, 4) == "each")
        {
            string tag = entityTag.Substring(4);
            foreach (GameEntity entity in entities)
            {
                if (entity.Tag == tag)
                {
                    ShiftEntity(entity, directionString);
                }
            }
        }
        else
        {
            GameEntity entity = assignedVariables[entityTag];
            if (entity != null)
            {
                ShiftEntity(entity, directionString);
            }
        }
    }

    public void ShiftEntity(GameEntity entity, string directionString)
    {
        if (directionString == "Up")
        {
            TryShift(entity, Data.Direction.up);
        }
        else if (directionString == "Right")
        {
            TryShift(entity, Data.Direction.right);
        }
        else if (directionString == "Left")
        {
            TryShift(entity, Data.Direction.left);
        }
        else if (directionString == "Down")
        {
            TryShift(entity, Data.Direction.down);
        }
        else if (directionString == "Forward")
        {
            TryShift(entity, entity.Facing);
        }
        else if (directionString == "Backward")
        {
            TryShift(entity, (Data.Direction)((((int)entity.Facing) + 2) % 4));
        }
        else
        {
            string[] words = directionString.Split('.');
            if (words.Length == 2)
            {
                GameEntity directionSource = assignedVariables[words[0]];
                if (directionSource != null)
                {
                    if (words[1] == "Forward")
                    {
                        TryShift(entity, directionSource.Facing);
                    }
                    else if (words[1] == "Backward")
                    {
                        TryShift(entity, (Data.Direction)((((int)directionSource.Facing) + 2) % 4));
                    }
                }
            }
        }
    }

    public void PlaceAt(string entityTag, int x, int y)
    {
        if (entityTag.Length > 4 && entityTag.Substring(0, 4) == "each")
        {
            string tag = entityTag.Substring(4);
            foreach (GameEntity entity in entities)
            {
                if (entity.Tag == tag)
                {
                    entity.XPos = x;
                    entity.YPos = y;
                }
            }
        }
        else
        {
            GameEntity entity = assignedVariables[entityTag];
            if (entity != null)
            {
                entity.XPos = x;
                entity.YPos = y;
            }
        }
    }
}
