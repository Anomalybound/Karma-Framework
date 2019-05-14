namespace Karma
{
    public interface ITime
    {
        float LogicTime { get; }
        
        float RealTime { get; }
    }
}