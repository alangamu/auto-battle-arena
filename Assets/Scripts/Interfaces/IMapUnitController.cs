namespace AutoFantasy.Scripts.Interfaces
{
    public interface IMapUnitController 
    {
        int Q { get; }
        int R { get; }
        void SetHexCoordinates(int q, int r);
    }
}