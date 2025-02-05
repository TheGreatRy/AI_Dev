using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class NavNode : MonoBehaviour
{
    public List<NavNode> neighbors = new List<NavNode>();
    public NavNode GetRandomNeighbor()
    {
        return (neighbors.Count > 0) ? neighbors[Random.Range(0, neighbors.Count)] : null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<NavNode>(out NavNode navNode))
        {
            navNode = neighbors[Random.Range(0, neighbors.Count)];
        }

    }
    void Start()
    {
        
    }

     void Update()
    {
        
    }
    //public NavNode GetNearestNaveNode()
    //{

    //}

    #region HELPER
    public static NavNode[] GetNavNodes()
    {
        return FindObjectsByType<NavNode>(FindObjectsSortMode.None);
    }
    public static NavNode[] GetNavNodesByTag(string tag)
    {
        List<NavNode> result = new List<NavNode>();
        var gameObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (var gameObject in gameObjects)
        {
            if (gameObject.TryGetComponent(out NavNode navNode))
            {
                result.Add(navNode);
            }
        }
        return result.ToArray();

    }
    public static NavNode GetRandomNavNode()
    {
        var navNodes = GetNavNodes();
        return (navNodes == null) ? null : navNodes[Random.Range(0, navNodes.Length)]; 
    }
    #endregion
}
