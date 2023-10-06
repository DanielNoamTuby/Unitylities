using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Unitylities
{
    public class GameManager : MonoBehaviour
    {
        private FakeTimeSystem fakeTimeSystem;
        public FakeTimeClock fakeTimeClock;

        private void Awake()
        {
            fakeTimeSystem = new FakeTimeSystem(85300f, FakeTimeEnums.Day.Saturday, 4, FakeTimeEnums.Month.December, 0);
        }

        private void Start()
        {
            fakeTimeClock.SetFakeTimeSystem(fakeTimeSystem);
        }

        private void Update()
        {
            fakeTimeSystem.UpdateTime(Time.deltaTime);
        }

        public void OnButtonClick()
        {
            float day = 86400f;
            fakeTimeSystem.AdvanceTime(day);
        }
    }
}