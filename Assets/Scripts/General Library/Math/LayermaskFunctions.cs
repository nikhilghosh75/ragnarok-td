/*
 * A set of static functions used for interacting with layermasks without requiring the knowledge of layers.
 * @ Nikhil Ghosh '23
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSoft.Math
{
    public class LayermaskFunctions
    {
        /// <summary>
        /// Checks if a layer is in a given layermask
        /// </summary>
        /// <param name="mask">The mask to check</param>
        /// <param name="layer">The layer to check</param>
        /// <returns>Is the layer in the layermask</returns>
        public static bool IsInLayerMask(LayerMask mask, int layer)
        {
            if(layer < 0 || layer > 32)
            {
                return false;
            }
            return (((mask.value) >> layer) % 2) == 1;
        }

        public static LayerMask AddToLayerMask(LayerMask mask, int layer)
        {
            if (layer < 0 || layer > 32)
            {
                return mask;
            }

            mask.value |= 1 << layer;
            return mask;
        }
    }
}