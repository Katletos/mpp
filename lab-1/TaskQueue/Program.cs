using TaskQueue;

// Часть 1
// Создать класс на языке C# - TaskQueue - Пул Потоков
// Создаёт указанное количество потоков пула в конструкторе
// Содержит очередь задач из делегатов без параметров
// Обеспечивает постановку в очередь и последующее выполнение делегатов с помощью метода MethodQueueDelegate
// Работу можно любую (считать и так далее)
// Часть 2
// Разработать класс для имитации часов с обратным отсчётом, реализующей возможность по истечению назначенного времени
// (пользователем передаётся в класс время, через конструктор или интерфейс), передавать сообщение и
// дополнительную информацию о событии, любому подписавшемуся на событие типу
// Предусмотреть возможность подписки на событие нескольким классам 

// 1 часть
// lockBuffer который представляет собой журнал строковых сообщений
// предоставляет метод Add(string str)
// буфиризирует добавляемое одиночное сообщение и пишет на диск
// периодически скидывает инфу на диск
// периодически пишет на диск по таймеру вне зависимости от количества
// 2 часть
// ститический метод ParrallelWhaitAll 
// принимает в параметрах массив делегатов и исполняет из параллельно при помощи пула потоков
// Дожидается исполнения всех делегатов
// Реализовать простейший пример


var clock = new Clock(TimeSpan.FromSeconds(2));
clock.Start();

while (true)
{
   
}

// Create a scheduler that uses two threads.
LimitedConcurrencyLevelTaskScheduler lcts = new LimitedConcurrencyLevelTaskScheduler(2);
List<Task> tasks = new List<Task>();

// Create a TaskFactory and pass it our custom scheduler.
TaskFactory factory = new TaskFactory(lcts);
CancellationTokenSource cts = new CancellationTokenSource();

// Use our factory to run a set of tasks.
Object lockObj = new Object();
int outputItem = 0;

for (int tCtr = 0; tCtr <= 4; tCtr++) {
   int iteration = tCtr;
   Task t = factory.StartNew(() => {
      for (int i = 0; i < 1000; i++) {
         lock (lockObj) {
            Console.Write("{0} in task t-{1} on thread {2}   ",
               i, iteration, Thread.CurrentThread.ManagedThreadId);
            outputItem++;
            if (outputItem % 3 == 0)
               Console.WriteLine();
         }
      }
   }, cts.Token);
   tasks.Add(t);
}

// Use it to run a second set of tasks.
for (int tCtr = 0; tCtr <= 4; tCtr++) { 
   int iteration = tCtr;
   Task t1 = factory.StartNew(() => {
      for (int outer = 0; outer <= 10; outer++) {
         for (int i = 0x21; i <= 0x7E; i++) {
            lock (lockObj) {
               Console.Write("'{0}' in task t1-{1} on thread {2}   ",
                  Convert.ToChar(i), iteration, Thread.CurrentThread.ManagedThreadId);
               outputItem++;
               if (outputItem % 3 == 0)
                  Console.WriteLine();
            }
         }
      }
   }, cts.Token);
   tasks.Add(t1);
}

// Wait for the tasks to complete before displaying a completion message.
Task.WaitAll(tasks.ToArray());
cts.Dispose();
Console.WriteLine("\n\nSuccessful completion.");
Thread.Sleep(5);

class StringPrinter
{
   void PrintString(string str)
   {
      Console.WriteLine(str);
   }
}