using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusaninPathFinding
{
    public class Rotator
    {
        #region Properties

        public int Pitch { get; set; }

        public int Yaw { get; set; }

        public int Roll { get; set; }

        #endregion

        #region Constructors

        public Rotator()
        {
        }

        public Rotator(int path, int yaw, int roll)
        {
            Pitch = path;
            Yaw = yaw;
            Roll = roll;
        }

        #endregion

    }
}
