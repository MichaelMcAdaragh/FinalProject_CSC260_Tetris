using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject_CSC260_Tetris
{
    public enum RotationalState
    {
        Deg000,             // 0
        Deg090,             // 1
        Deg180,             // 2
        Deg270              // 3    // should work mathematically fine. (modulo 4) (+4 to prevent neg values)
    }
}
