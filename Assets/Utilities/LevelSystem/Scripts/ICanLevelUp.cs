using System;
using Unitylities;

namespace Unitylities
{
    public interface ICanLevelUp
    {
        public LevelSystem LevelSystem { get; }
        public void AddExperience(int amount);
    }

}