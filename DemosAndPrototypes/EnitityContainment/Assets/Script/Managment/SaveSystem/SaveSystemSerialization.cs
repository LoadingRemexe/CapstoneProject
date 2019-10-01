using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public static class SaveSystemSerialization
{
    #region Vector3
    public static float[] SerilizeVector3(Vector3 vector)
    {
        return new float[3] { vector.x, vector.y, vector.z };
    }

    public static Vector3 DeSerilizeVector3(float[] vector)
    {
        return new Vector3(vector[0], vector[1], vector[2]);
    }

    public static Vector3 DeSerilizeVector3(object vector)
    {
        try { return DeSerilizeVector3((float[])vector); }
        catch
        {
            Debug.LogError("Invalid type when Deserilizing Vector3");
            return Vector3.zero;
        }
    }
    #endregion

    #region Quaternion
    public static float[] SerilizeQuaternion(Quaternion quaternion)
    {
        return new float[4] { quaternion.x, quaternion.y, quaternion.z, quaternion.w };
    }

    public static Quaternion DeSerilizeQuaternion(float[] quaternion)
    {
        return new Quaternion(quaternion[0], quaternion[1], quaternion[2], quaternion[3]);
    }

    public static Quaternion DeSerilizeQuaternion(object quaternion)
    {
        try { return DeSerilizeQuaternion((float[])quaternion); }
        catch
        {
            Debug.LogError("Invalid type when Deserilizing Quaternion");
            return Quaternion.identity;
        }
    }
    #endregion

    #region Transform
    public static float[] SerilizeTransform(Transform transform)
    {
        return new float[10] { transform.position.x, transform.position.y, transform.position.z, transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w, transform.localScale.x, transform.localScale.y, transform.localScale.z };
    }

    public static void DeSerilizeTransform(float[] values, Transform transform)
    {
        transform.position = new Vector3(values[0], values[1], values[2]);
        transform.rotation = new Quaternion(values[3], values[4], values[5], values[6]);
        transform.localScale = new Vector3(values[7], values[8], values[9]);
    }

    public static void DeSerilizeTransform(object values, Transform transform)
    {
        try { DeSerilizeTransform((float[])values, transform); }
        catch { Debug.LogError("Invalid type when Deserilizing Transfrom"); }
    }

    #endregion
}

