using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Utility.QuaternionExtensions
{
    public static class QuaternionExtensions
    {
        public static Quaternion Lerp(Quaternion a, Quaternion b, float t, bool forceShorterPath)
        {
            if (forceShorterPath)
            {
                float dot = Quaternion.Dot(a, b);
                if (dot < 0f)
                {
                    return Lerp(ScalarMultiply(a, -1.0f), b, t, true);
                }
            }

            Quaternion r = Quaternion.identity;
            r.x = a.x * (1f - t) + b.x * (t);
            r.y = a.y * (1f - t) + b.y * (t);
            r.z = a.z * (1f - t) + b.z * (t);
            r.w = a.w * (1f - t) + b.w * (t);
            return r;
        }

        public static Quaternion ScalarMultiply(Quaternion input, float scalar)
        {
            return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
        }

        public static Quaternion Add(Quaternion p, Quaternion q)
        {
            return new Quaternion(p.x + q.x, p.y + q.y, p.z + q.z, p.w + q.w);
        }
    
    }
}
