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
    }

    public Rule findRuleWithName(string findName)
    {
        return null;
    }
}
