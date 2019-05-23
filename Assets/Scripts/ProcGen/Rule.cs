using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule
{
    string ruleName;
    Rule[] expansion;
    bool terminal;
    public Room room;

    public Rule(string ruleName, Rule[] expansion, bool terminal, GameObject room)
    {
        this.ruleName = ruleName;
        this.terminal = terminal;
        this.expansion = expansion;
    }

    public Rule findRuleWithName(string findName, List<Rule> rules)
    {
        foreach (Rule rule in rules)
        {
            if(rule.ruleName == findName)
            {
                return rule;
            }
        }
        return null;
    }

    public Rule chooseExpansion()
    {
        int r = Random.Range(0, expansion.Length);
        Debug.Log("Chose expansion " + expansion[r].ruleName);
        return expansion[r];
    }
}
