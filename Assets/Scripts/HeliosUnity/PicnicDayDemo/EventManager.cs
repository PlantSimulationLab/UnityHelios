using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HeliosUnity.PicnicDayDemo
{
    public class EventManager
    {
        public static Action ProgramStart;
        public static Action ProgramEnd;

        public static Action ExperienceStart;
        public static Action ExperienceEnd;
        public static Action ExperienceReset;

        public static Action TabletHold;
        public static Action TabletRelease;
        public static Action TabletOptionChange;
        public static Action TabletSliderChange;

        public static Action CanopySelect;
        public static Action CanopyDeselect;

        public static Action TVInformationChange;

    }
}

