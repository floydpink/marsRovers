namespace Nasa.Mars.Rovers.Model.Interfaces
{
    public interface IPlateau
    {
        int EastBoundary { get; }
        int NorthBoundary { get; }

        bool IsRoverWithinLimits(IRover rover);
    }
}
