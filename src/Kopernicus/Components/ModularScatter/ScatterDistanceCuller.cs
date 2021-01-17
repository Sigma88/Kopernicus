using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Kopernicus.Components.ModularScatter
{
    [RequireComponent(typeof(MeshRenderer))]
    class ScatterDistanceCuller : MonoBehaviour
    {
        private MeshRenderer surfaceObject;
        private float maxdistance = 10000;
        private Boolean configValueLoaded = false;
        private void Start()
        {
            surfaceObject = GetComponent<MeshRenderer>();
        }
        private void Update()
        {
            if (configValueLoaded == false)
            {
                maxdistance = Kopernicus.RuntimeUtility.RuntimeUtility.KopernicusConfig.ScatterCullDistance;
                configValueLoaded = true;
            }
            float distance = 0;
            if (FlightGlobals.ActiveVessel != null)
            {
                try
                {
                    distance = Vector3.Distance(FlightGlobals.ActiveVessel.transform.position, surfaceObject.transform.position);
                }
                catch
                {
                    distance = Vector3.Distance(Camera.current.transform.position, surfaceObject.transform.position);
                    //If craft breaks up this prevents errors.
                }
            }
            else
            {
                distance = Vector3.Distance(Camera.current.transform.position, surfaceObject.transform.position);
            }

            if (distance > maxdistance)
            {
                surfaceObject.enabled = false;
            }
            else
            {
                surfaceObject.enabled = true;
            }
        }
    }
}
