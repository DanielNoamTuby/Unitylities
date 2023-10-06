using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Unitylities
{
    public class FakeTimeClock : MonoBehaviour
    {
        private Slider fill;
        private TextMeshProUGUI text;
        [SerializeField] private FakeTimeSystem tm;

        private void Awake()
        {
            fill = transform.GetComponent<Slider>();
            text = transform.Find("left").Find("clock").GetComponent<TextMeshProUGUI>();
        }
        private void Start()
        {
            if (tm != null)
            {
                tm.OnMinuteHourChanged += SubOnMinuteHourChanged;
            }
            else
            {
                Debug.Log("No Time Manager Found !");
            }
        }

        private void SubOnMinuteHourChanged(object sender, EventArgs e)
        {
            fill.value = tm.CurrentTime / tm.DayLength;
            text.text = tm.FormatCurrentTime();
        }
        public void SetFakeTimeSystem(FakeTimeSystem timeManager)
        {
            tm = timeManager;
        }
    }
}