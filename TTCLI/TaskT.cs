using System.Text.Json;
using System.Linq;

class JsonFile
{
    public void WriteJsonFile(string name, object value)
    {
        var opciones = new JsonSerializerOptions
        {
            WriteIndented = true,
        };

        File.WriteAllText(name, JsonSerializer.Serialize(value, opciones));
    }

    public Dictionary<int, Tarea> ReadJsonFile(string name)
    {
        var JsonTextDSS = new Dictionary<int, Tarea>();
        if (!File.Exists(name))
        {
            return new Dictionary<int, Tarea>();
        }
        string JsonText = File.ReadAllText(name);
        if (string.IsNullOrEmpty(JsonText))
        {
            return JsonTextDSS;

        }else
        {
            JsonTextDSS = JsonSerializer.Deserialize<Dictionary<int, Tarea>>(JsonText);
            return JsonTextDSS;
        }
    }
}


class TareaJson
{
    public Dictionary<int, Tarea> dicTareaJson = new Dictionary<int, Tarea>();
}

class Tarea
{
    public string? Description { get; set; }
    public enum Estado { Todo, InProgress, Done, Error }
    public Estado EstadoActual { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }

}

class DatosTask
{
    int Id = 0;
    static string nombreJsonArchivo = "JsonTask.json";
    TareaJson diccionarioTask = new TareaJson();
    JsonFile jsonFile = new JsonFile();

    public void LlenarTask( string taskContent )
    {

        diccionarioTask.dicTareaJson = jsonFile.ReadJsonFile(nombreJsonArchivo);
        if (diccionarioTask.dicTareaJson.Count > 0)
        {
            Id = diccionarioTask.dicTareaJson.Keys.Last() + 1;        
        }else
        {
            Id = 1;
        }
        
        diccionarioTask.dicTareaJson.Add(Id, new Tarea()
        {
            Description = taskContent,
            CreatedAt = DateTime.Now,
        });
        


        jsonFile.WriteJsonFile(nombreJsonArchivo, diccionarioTask.dicTareaJson);
        Console.WriteLine($"\n\t Tak added succesfully ( ID:{Id} ) \n\t");
    }

    public void UpdateTask(int id, string taskContent)
    {
        diccionarioTask.dicTareaJson = jsonFile.ReadJsonFile(nombreJsonArchivo);

        diccionarioTask.dicTareaJson[id].Description = taskContent;
        diccionarioTask.dicTareaJson[id].UpdateAt = DateTime.Now;

        jsonFile.WriteJsonFile(nombreJsonArchivo, diccionarioTask.dicTareaJson);
    }

    public void DeletTask(int id)
    {
        diccionarioTask.dicTareaJson = jsonFile.ReadJsonFile(nombreJsonArchivo);
        // Id = +diccionarioTask.dicTareaJson.Count;
        diccionarioTask.dicTareaJson.Remove(id);
        jsonFile.WriteJsonFile(nombreJsonArchivo, diccionarioTask.dicTareaJson);

    }


    public void EnumSelect(string taskContent, int idEnum)
    {
        diccionarioTask.dicTareaJson = jsonFile.ReadJsonFile(nombreJsonArchivo);
        
        switch (taskContent.ToLower())
        {
            case "todo":
                diccionarioTask.dicTareaJson[idEnum].EstadoActual = Tarea.Estado.Todo;
                break;

            case "in-progress":
                diccionarioTask.dicTareaJson[idEnum].EstadoActual = Tarea.Estado.InProgress; 
                break;

            case "done":
                diccionarioTask.dicTareaJson[idEnum].EstadoActual = Tarea.Estado.Done;
                break;
            default:
                diccionarioTask.dicTareaJson[idEnum].EstadoActual = Tarea.Estado.Error;
                break;
            
            
        }

        jsonFile.WriteJsonFile(nombreJsonArchivo, diccionarioTask.dicTareaJson);

    }

        public List<Tarea> ListByTask (Tarea.Estado estado)
        {
            List<Tarea> ListaState = new List<Tarea>();
            diccionarioTask.dicTareaJson = jsonFile.ReadJsonFile(nombreJsonArchivo);
            
            switch (estado)
            {
             
                case Tarea.Estado.Todo:
                    foreach (var item in diccionarioTask.dicTareaJson)
                    {
                        if (item.Value.EstadoActual == estado)
                        {
                            ListaState.Add(item.Value);
                        }
                    }
                    break;

                case Tarea.Estado.Done:
                    foreach (var item in diccionarioTask.dicTareaJson)
                    {
                        if (item.Value.EstadoActual == estado)
                        {
                            ListaState.Add(item.Value);
                        }
                    }
                    break;

                case Tarea.Estado.InProgress:
                    foreach (var item in diccionarioTask.dicTareaJson)
                    {
                        if (item.Value.EstadoActual == estado)
                        {
                            ListaState.Add(item.Value);
                        }
                    }
                    break;
                default:
                    foreach (var item in diccionarioTask.dicTareaJson)
                    {
                            ListaState.Add(item.Value);
                    }
                    break;
            }        
            return ListaState;
        }
}


namespace MainProgram
{
    class Program
    {
        static void Main( string [] args )
        {
            List<Tarea> TareasByStatus = new List<Tarea>();   
            DatosTask Task1 = new DatosTask();
            string command = args[0].ToLower();
            string taskContent = "";
            string updateTask = "";
            if (command == "update")
            {
                updateTask = args[2].ToLower();

            }if (command != "list" )
            {
                taskContent = args[1].ToLower();
            }if (command == "list" && args.Length > 1)
            {
                command = args[1].ToLower();
            }
            


            switch (command)
            {
                case "add":
                    Task1.LlenarTask(taskContent);
                    break;
                case "update":
                    Task1.UpdateTask(Convert.ToInt32(taskContent),updateTask);
                    break;
                case "delete":
                    Task1.DeletTask(Convert.ToInt32(taskContent));
                    break;
                case "mark-in-progress":
                    Task1.EnumSelect("in-progress",Convert.ToInt32(taskContent));
                    break;
                case "mark-done":
                    Task1.EnumSelect("done",Convert.ToInt32(taskContent));
                    break;
                case "mark-todo":
                    Task1.EnumSelect("todo",Convert.ToInt32(taskContent));
                    break ;
                case "list":
                    TareasByStatus = Task1.ListByTask(Tarea.Estado.Error);
                    Console.WriteLine("List ALL");
                    
                    for (int i = 0; i < TareasByStatus.Count  ; i++)
                    {
                        Console.WriteLine($"\n Descripccion task: {TareasByStatus[i].Description} \n");
                    }

                    break; 
                case "done":
                    TareasByStatus = Task1.ListByTask(Tarea.Estado.Done);
                    Console.WriteLine("List DONE");

                    for (int i = 0; i < TareasByStatus.Count  ; i++)
                    {
                        Console.WriteLine($"\n Descripccion task: {TareasByStatus[i].Description} \n");
                    }

                    break;                    
                case "todo":
                    TareasByStatus = Task1.ListByTask(Tarea.Estado.Todo);
                    Console.WriteLine("List TODO");
                    for (int i = 0; i < TareasByStatus.Count  ; i++)
                    {
                        Console.WriteLine($"\n Descripccion task: {TareasByStatus[i].Description} \n");
                    }

                    break;
                case "in-progress":
                    TareasByStatus = Task1.ListByTask(Tarea.Estado.InProgress);
                    Console.WriteLine("List In-progress");               
                    for (int i = 0; i < TareasByStatus.Count  ; i++)
                    {
                        Console.WriteLine($"\n Descripccion task: {TareasByStatus[i].Description} \n");
                    }

                    break;

            }
        }
    }

}
