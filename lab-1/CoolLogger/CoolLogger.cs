namespace CoolLogger;

public class CoolLogger
{
   private readonly List<string> _logs;
   private readonly int _logsBufSize;
   private readonly StreamWriter _writer;
   private readonly Timer _timer;
   
   public CoolLogger(string path, int logsBufSize, TimeSpan timeSpan)
   {
      _logs = new List<string>();
      _logs.EnsureCapacity(logsBufSize);
      _logsBufSize = logsBufSize;
      _timer = new Timer(WriteLogsOnTimer, null, TimeSpan.Zero, timeSpan);
      
      ArgumentNullException.ThrowIfNull(path);
      try
      {
         var sw = new StreamWriter(path);
         _writer = sw;
      }
      catch (Exception e)
      {
         Console.WriteLine(e);
         throw;
      }
   }

   public void AddLog(string logMessage)
   {
      lock (_logs)
      {
         if (_logs.Count == _logsBufSize)
         {
            WriteToDisk();
         }
         
         _logs.Add(logMessage);
      }
   }

   private void WriteLogsOnTimer(object? state)
   {
      lock (_logs)
      {
         WriteToDisk();
      }
   }
   
   private void WriteToDisk()
   {
      try
      {
         _logs.ForEach(x => _writer.Write(x));   
         _logs.Clear();
      }
      catch (Exception e)
      {
         Console.WriteLine(e);
         throw;
      }
   }
}