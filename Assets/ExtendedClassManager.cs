using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtendedClassManager
{
    /// <summary>
    /// Funcion que chequea la distancia entre dos objetos
    /// </summary>
    /// <param name="a">Transform desde el cual se llama el método (origen).</param>
    /// <param name="b">Transform con el que se quiere comparar la distancia (destino).</param>
    /// <param name="c">Distancia máxima permitida entre ambos objetos</param>
    /// <returns>Devuelve <c>true</c> si la distancia entre ambos objetos es menor que <paramref name="c"/>; de lo contrario, <c>false</c>.</returns>
    public static bool IsWithinDistanceOf(this Transform a, Transform b, float c)
    {
        var _sqrDistanceTranforms = (a.position - b.position).sqrMagnitude;
        var _threshold = c * c;
        return (_sqrDistanceTranforms < _threshold);
    }
    public static float IsMostNearDistance(this Transform a, Transform b)
    {
        var _sqrDistanceTranforms = (a.position - b.position).sqrMagnitude;
        return _sqrDistanceTranforms;
    }
}
