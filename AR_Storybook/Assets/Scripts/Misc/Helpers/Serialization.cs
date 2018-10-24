namespace ARStorybook.Helpers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Serializing of Variables 
    /// </summary>
    public class Serialization
    {
        /// <summary>
        /// Function to serialise ( convert ) a Vector3 to a string 
        /// </summary>
        /// <param name="_vector">Vector to be serialised</param>
        /// <returns>String with Vector3 data</returns>
        public static string SerialiseVector3(Vector3 _vector)
        {
            // create a mutable string for runtime modification
            StringBuilder stringBuilder = new StringBuilder();

            // convert and add to string 
            stringBuilder.Append(_vector.x).Append(" ");
            stringBuilder.Append(_vector.y).Append(" ");
            stringBuilder.Append(_vector.z).Append(" ");

            Debug.Log("Serialised Vector3 to string: " + stringBuilder.ToString());
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Function to deserialise ( convert ) a string to a Vector3
        /// </summary>
        /// <param name="_data">String to be deserialise</param>
        /// <returns>Vector3 with string data</returns>
        public static Vector3 DeserialiseVector3(string _data)
        {
            Vector3 tempVector;

            // convert and set vector
            string[] values = _data.Split(' ');
            tempVector = new Vector3(float.Parse(values[0]),
                                     float.Parse(values[1]),
                                     float.Parse(values[2]));

            Debug.Log("Deserialised String to Vector3: " + tempVector.x);
            return tempVector;
        }
    }
}
