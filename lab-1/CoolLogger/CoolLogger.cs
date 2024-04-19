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
      _timer = new Timer(WriteLogsOnTimer, null, timeSpan, timeSpan);
      
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
      Console.WriteLine("log added");
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
      if (_logs.Count is 0)
      {
         return;
      }
      
      try
      {
         _logs.ForEach(x => _writer.WriteLine(x));  
         _writer.Flush();
         _logs.Clear();
         Console.WriteLine("write to disk");
      }
      catch (Exception e)
      {
         Console.WriteLine(e);
         throw;
      }
   }
}