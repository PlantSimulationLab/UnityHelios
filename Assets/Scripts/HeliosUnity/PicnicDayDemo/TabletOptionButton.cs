using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeliosUnity.PicnicDayDemo
{

    public enum TabletOption
    {
        NONE = -1,
        RGB = 0, // rgb channel
        WUE = 1, // water use efficieny
        PSYNTH = 2, // photosynthesis
        LEST = 3 // light estimation
    }

    public abstract class TabletOptionButton : MonoBehaviour
    {
        protected TabletOption tabletOption = TabletOption.NONE;

        public abstract void OnOptionButtonClick();

    }
}
