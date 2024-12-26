namespace ProiectPOoSAM;

public interface ILogger
{
    public void AddToLogger(List<string> oldLog,string message);
    public void WriteLogger();
}