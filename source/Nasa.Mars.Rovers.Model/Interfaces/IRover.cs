using System.Collections.Generic;
namespace Nasa.Mars.Rovers.Model.Interfaces
{
    public interface IRover
    {
        int Easting { get; }
        int Northing { get;  }
        Direction Heading { get;  }
        
        void Navigate(Command command);
    }
}
