using System.Collections.Generic;
using Unitylities;

public class NPC
{
    #region Fields
    private string name;
    private string description;
    private ScheduleSystem schedule;
    #endregion

    #region Properties
    public string Name => name;
    public string Description => description;
    public ScheduleSystem Schedule => schedule;

    #endregion

    #region Constructor
    public NPC(string name, string description, ScheduleSystem schedule)
    {
        this.name = name;
        this.description = description;
        this.schedule = schedule;
    }
    #endregion
}
