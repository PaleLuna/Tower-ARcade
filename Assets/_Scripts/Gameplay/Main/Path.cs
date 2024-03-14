using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Path
{
    [SerializeReference]
    private List<PathPoint> pathPoints = new();
    public void Refresh()
    {
        if (pathPoints.Count == 0) return;

        for (int i = 0; i < pathPoints.Count - 1; i++)
            pathPoints[i].nextPoint = pathPoints[i + 1];

        pathPoints[pathPoints.Count - 1].nextPoint = null;
    }

    public PathPoint GetFirst() => pathPoints[0];
}
