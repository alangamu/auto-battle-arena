namespace AutoFantasy.Scripts.Interfaces
{
    public interface ISelectable 
    {
        void Select(bool option);
        bool IsSelected { get; }
    }
}