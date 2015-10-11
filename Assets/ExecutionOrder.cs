using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle;
using Battle.Abilities;
using UnityEditor;

public class ExecutionOrder : MonoBehaviour
{

    [SerializeField] private List<MonoScript> _scripts;
    private readonly List<KeyValuePair<MonoScript, MonoScript>> _scriptPairs = new List<KeyValuePair<MonoScript, MonoScript>>();

    private int startCounter = 1;

    void Awake()
    {

        foreach (var script in MonoImporter.GetAllRuntimeMonoScripts())
        {
            try
            {

                var baseScript = _scripts.FirstOrDefault(s => s.GetClass() == script.GetClass().BaseType);
                if (baseScript == null) continue;
                
                _scriptPairs.Add(new KeyValuePair<MonoScript, MonoScript>(baseScript,script));
            }
            catch (Exception) 
            {
                
                //ignored
            }

        }


        foreach (var script in _scripts)
        {
            //Add base class first, then derived classes

            MonoImporter.SetExecutionOrder(script, startCounter);
            startCounter++;

            var thisScript = script;

            var scriptPairs = _scriptPairs.Where(s => s.Key == thisScript);

            

            foreach (var scriptPair in scriptPairs)
            {
                MonoImporter.SetExecutionOrder(scriptPair.Value, startCounter);
                startCounter++;
            }

        }



        
    }
    
}
