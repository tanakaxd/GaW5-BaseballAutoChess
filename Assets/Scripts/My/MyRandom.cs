using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyRandom
{
    public static double RandomGaussian(float average, float sigma)
    {
        var rnd = new System.Random();

        double X, Y;
        double Z1;

        X = rnd.NextDouble();
        Y = rnd.NextDouble();

        Z1 = sigma * Math.Sqrt(-2.0 * Math.Log(X)) * Math.Cos(2.0 * Math.PI * Y) + average;
        //Z2 = Math.Sqrt(-2.0 * Math.Log(X)) * Math.Sin(2.0 * Math.PI * Y);

        return Z1;
    }

    public static double RandomGaussianUnity(float average, float sigma)
    {
        double X, Y;
        double Z1;

        X = UnityEngine.Random.value;
        Y = UnityEngine.Random.value;
        //Debug.Log(X);
        //Debug.Log(Y);

        Z1 = sigma * Math.Sqrt(-2.0 * Math.Log(X)) * Math.Cos(2.0 * Math.PI * Y) + average;
        //Z2 = Math.Sqrt(-2.0 * Math.Log(X)) * Math.Sin(2.0 * Math.PI * Y);

        return Z1;
    }
}
