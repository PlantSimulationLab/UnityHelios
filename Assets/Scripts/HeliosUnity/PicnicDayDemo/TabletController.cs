using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeliosUnity.PicnicDayDemo
{

    public class TabletController : MonoBehaviour
    {
        [SerializeField] private TabletOption currentOption;

        #region Monobehaviour Callbacks

        private void OnEnable()
        {
            EventManager.TabletHold += OnTabletHold;
            EventManager.TabletRelease += OnTabletRelease;
            EventManager.TabletSliderChange += OnTabletSliderChange;
            EventManager.TabletOptionChange += OnTabletOptionSelect;
        }

        private void OnDisable()
        {
            EventManager.TabletHold -= OnTabletHold;
            EventManager.TabletRelease -= OnTabletRelease;
            EventManager.TabletSliderChange -= OnTabletSliderChange;
            EventManager.TabletOptionChange -= OnTabletOptionSelect;
        }
        #endregion

        #region Event Management
        public void OnTabletHold()
        {
            
        }

        public void OnTabletRelease()
        {

        }

        public void OnTabletSliderChange()
        {

        }

        public void OnTabletOptionSelect()
        {

        }
        #endregion
    }
}
