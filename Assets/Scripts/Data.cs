using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Data
{
    public enum Direction { up = 0, right = 1, down = 2, left = 3 };

    public static float displayScale = .33f;
    private static Map[] maps;
    private static Statement[] statements;
    private static BoolStatement[] boolStatements;
    private static Rule[] rules;

    public static int NumMaps
    {
        get { return (maps.Length); }
    }

    public static Map GetMap(int index)
    {
        if (index < 0 || index >= NumMaps)
        {
            return (null);
        }
        return (maps[index]);
    }

    public static void LoadData()
    {
        LoadStatements();
        LoadRules();
        LoadMaps();
    }

    public static void LoadStatements()
    {
        StreamReader reader = new StreamReader("BoolStatements.csv");
        string line = reader.ReadLine();
        List<string[]> rows = new List<string[]>();
        while ((line = reader.ReadLine()) != null)
        {
            string[] row = line.Split(",");
            rows.Add(row);
        }
        reader.Close();
        boolStatements = new BoolStatement[rows.Count];
        for(int i =  rows.Count - 1; i >= 0; --i)
        {
            BoolStatement current = null;
            if(rows[i][0] == "SameSpace")
            {
                current = new SameSpaceStatement(rows[i][1].Split(';'), rows[i][2].Split(';'));
            }
            else if(rows[i][0] == "AndOr")
            {
                string[] andOrStrings = rows[i][1].Split(';');
                AndOrStatement.AndOrType[] andOrs = new AndOrStatement.AndOrType[andOrStrings.Length];
                for(int a = 0; a < andOrStrings.Length; ++a)
                {
                    if(andOrStrings[a] == "and")
                    {
                        andOrs[a] = AndOrStatement.AndOrType.and;
                    }
                    else
                    {
                        andOrs[a] = AndOrStatement.AndOrType.or;
                    }
                }
                current = new AndOrStatement(andOrs, boolStatements[int.Parse(rows[i][2])], boolStatements[int.Parse(rows[i][3])]);
            }
            boolStatements[i] = current;
        }

        reader = new StreamReader("Statements.csv");
        line = reader.ReadLine();
        rows = new List<string[]>();
        while ((line = reader.ReadLine()) != null)
        {
            string[] row = line.Split(",");
            rows.Add(row);
        }
        reader.Close();
        statements = new Statement[rows.Count];
        for (int i = rows.Count - 1; i >= 0; --i)
        {
            Statement current = null;
            if (rows[i][0] == "ChangeMap")
            {
                string[] indexStrings = rows[i][1].Split(';');
                int[] indexs = new int[indexStrings.Length];
                for (int a = 0; a < indexStrings.Length; ++a)
                {
                    indexs[a] = int.Parse(indexStrings[a]);
                }
                current = new ChangeMapStatement(indexs);
            }
            else if(rows[i][0] == "ForEach")
            {
                current = new ForEachStatement(rows[i][1].Split(';'), rows[i][2], statements[int.Parse(rows[i][3])]);
            }
            else if(rows[i][0] == "IfElse")
            {
                Statement elseStatement = null;
                if(int.Parse(rows[i][3]) >= 0)
                {
                    elseStatement = statements[int.Parse(rows[i][3])];
                }
                current = new IfElseStatement(boolStatements[int.Parse(rows[i][1])], statements[int.Parse(rows[i][2])], elseStatement);
            }
            else if(rows[i][0] == "Move")
            {
                current = new MoveStatement(rows[i][1].Split(';'), rows[i][2].Split(';'));
            }
            else if(rows[i][0] == "PlaceAt")
            {
                string[] xStrings = rows[i][2].Split(';');
                int[] xs = new int[xStrings.Length];
                for (int a = 0; a < xStrings.Length; ++a)
                {
                    xs[a] = int.Parse(xStrings[a]);
                }
                string[] yStrings = rows[i][3].Split(';');
                int[] ys = new int[yStrings.Length];
                for (int a = 0; a < yStrings.Length; ++a)
                {
                    ys[a] = int.Parse(yStrings[a]);
                }
                current = new PlaceAtStatement(rows[i][1].Split(';'), xs, ys);
            }
            else if(rows[i][0] == "Shift")
            {
                current = new ShiftStatement(rows[i][1].Split(';'), rows[i][2].Split(';'));
            }
            statements[i] = current;
        }
    }

    public static void LoadRules()
    {
        StreamReader reader = new StreamReader("Rules.csv");
        string line = reader.ReadLine();
        List<Rule> r = new List<Rule>();
        while((line = reader.ReadLine()) != null)
        {
            string[] row = line.Split(',');
            Rule.RuleType type = Rule.RuleType.onMove;
            if(row[0] == "Interact")
            {
                type = Rule.RuleType.onInteract;
            }
            string[] indexStrings = row[1].Split(';');
            Statement[] s = new Statement[indexStrings.Length];
            for(int i = 0; i < s.Length; ++i)
            {
                s[i] = statements[int.Parse(indexStrings[i])].GetCopy();
            }
            r.Add(new Rule(type, s));
        }
        reader.Close();
        rules = new Rule[r.Count];
        for(int i = 0; i < rules.Length; ++i)
        {
            rules[i] = r[i];
        }
    }

    private static void LoadMaps()
    {
        StreamReader reader = new StreamReader("Maps.csv");
        string line = reader.ReadLine();
        List<Map> m = new List<Map>();
        while((line = reader.ReadLine()) != null)
        {
            string[] row = line.Split(",");
            StreamReader entitiesReader = new StreamReader(row[0] + ".csv");
            string entitiesLine = entitiesReader.ReadLine();
            List<EntityStartPos> e = new List<EntityStartPos>();
            while((entitiesLine = entitiesReader.ReadLine()) != null)
            {
                string[] entitiesRow = entitiesLine.Split(',');
                GameEntity entity = null;
                if(entitiesRow[0] == "Computer")
                {
                    string[] ruleIndexStrings = entitiesRow[4].Split(';');
                    Rule[] r = new Rule[ruleIndexStrings.Length];
                    for(int i = 0; i < r.Length; ++i)
                    {
                        r[i] = rules[int.Parse(ruleIndexStrings[i])];
                    }
                    entity = new Computer("Computer", Resources.Load<Sprite>(entitiesRow[3]), r);
                }
                else
                {
                    entity = new GameEntity(entitiesRow[3], Resources.Load<Sprite>(entitiesRow[4]));
                }
                e.Add(new EntityStartPos(entity, new Vector2Int(int.Parse(entitiesRow[1]), int.Parse(entitiesRow[2]))));
            }
            entitiesReader.Close();
            EntityStartPos[] entityStarts = new EntityStartPos[e.Count];
            for (int i = 0; i < entityStarts.Length; ++i)
            {
                entityStarts[i] = e[i];
            }
            m.Add(new Map(new Vector2Int(int.Parse(row[1]), int.Parse(row[2])), entityStarts));
        }
        reader.Close();
        maps = new Map[m.Count];
        for(int i = 0; i < maps.Length; ++i)
        {
            maps[i] = m[i];
        }

    }
}
