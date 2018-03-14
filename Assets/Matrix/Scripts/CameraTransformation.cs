using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransformation : Transformation {

    public enum ProjectionType : byte
    {
        Orthographic,
        Perspective
    }

    public ProjectionType projection = CameraTransformation.ProjectionType.Perspective;
    
    public override Matrix4x4 Matrix
    {
        get
        {
            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetRow(0, new Vector4(1f, 0f, 0f, 0f));
            matrix.SetRow(1, new Vector4(0f, 1f, 0f, 0f));
            matrix.SetRow(2, new Vector4(0f, 0f, 0f, 0f));
            matrix.SetRow(3, new Vector4(0f, 0f, 1f, 0f));


            if (projection == ProjectionType.Orthographic)
            {
                matrix.SetRow(3, new Vector4(0f, 0f, 0f, 1f));
            }
            else if (projection == ProjectionType.Perspective)
            {
                matrix.SetRow(3, new Vector4(0f, 0f, 1f, 0f));
            }

            return matrix;
        }
    }

}
