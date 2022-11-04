// See https://aka.ms/new-console-template for more information

using ObjectsLayoutDemo;
using ObjectLayoutInspector;

PackingFieldDemo.Run();
return;

var o = new object();

TypeLayout.PrintLayout(o.GetType());

