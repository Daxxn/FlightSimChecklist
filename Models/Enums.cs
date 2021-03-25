using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistApp.Models.Enums
{
   public enum AircraftType
   {
      Other,
      Prop,
      TurboProp,
      Jet,
      Heli
   };
   public enum State
   {
      ON,
      OFF,
      SET,
      CHECKED,
      READY,
      UP,
      DOWN,
      TAKEOFF,
      LANDING
   };
}
