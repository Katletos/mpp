// See https://aka.ms/new-console-template for more information

using tree;

Console.WriteLine("");

var a = new MyList<int>(2);

a.Add(2);
a.Add(2);
Console.WriteLine(a.Count);
a.Add(2);

Console.WriteLine(a.Count);