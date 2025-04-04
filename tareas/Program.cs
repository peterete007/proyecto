using System;
using System.Collections.Generic;
using System.Linq;

class Tarea
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public bool Completada { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string Prioridad { get; set; }

    public Tarea(int id, string descripcion, DateTime fechaCreacion)
    {
        Id = id;
        Descripcion = descripcion;
        Completada = false;
        FechaCreacion = fechaCreacion;
        Prioridad = CalcularPrioridad();
    }

    public string CalcularPrioridad()
    {
        var diasPendientes = (DateTime.Now - FechaCreacion).TotalDays;

        if (Completada)
            return "Baja";

        if (diasPendientes > 3)
            return "Alta";
        if (diasPendientes >= 1)
            return "Media";

        return "Baja";
    }

    public void MarcarComoCompletada()
    {
        Completada = true;
        Prioridad = CalcularPrioridad();
    }
}

class Program
{
    static List<Tarea> tareas = new List<Tarea>();
    static int idContador = 1;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            MostrarMenu();
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    AgregarTarea();
                    break;
                case "2":
                    ListarTareasPorPrioridad();
                    break;
                case "3":
                    MarcarTareaComoCompletada();
                    break;
                case "4":
                    EliminarTarea();
                    break;
                case "5":
                    Console.WriteLine("Saliendo...");
                    return;
                default:
                    Console.WriteLine("Opción no válida. Intenta nuevamente.");
                    break;
            }
        }
    }

    static void MostrarMenu()
    {
        Console.WriteLine("=== Gestor de Tareas ===");
        Console.WriteLine("1. Agregar tarea");
        Console.WriteLine("2. Listar tareas por prioridad");
        Console.WriteLine("3. Marcar tarea como completada");
        Console.WriteLine("4. Eliminar tarea");
        Console.WriteLine("5. Salir");
        Console.Write("Elige una opción: ");
    }

    static void AgregarTarea()
    {
        string descripcion;
        while (true)
        {
            Console.Write("Ingrese la descripción de la tarea: ");
            descripcion = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(descripcion)) break;
            Console.WriteLine("Descripción no válida. Inténtalo de nuevo.");
        }

        DateTime fechaCreacion;
        while (true)
        {
            Console.Write("Ingrese la fecha y hora de la tarea (dd/MM/yyyy HH:mm): ");
            string fechaInput = Console.ReadLine();
            if (DateTime.TryParseExact(fechaInput, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out fechaCreacion))
                break;
            Console.WriteLine("Fecha no válida. Inténtalo de nuevo.");
        }

        Tarea nuevaTarea = new Tarea(idContador++, descripcion, fechaCreacion);
        tareas.Add(nuevaTarea);
        Console.WriteLine("Tarea agregada exitosamente.");
        Console.ReadLine();
    }

    static void ListarTareasPorPrioridad()
    {
        var tareasAgrupadas = tareas.GroupBy(t => t.Prioridad).OrderBy(g => g.Key);
        foreach (var grupo in tareasAgrupadas)
        {
            Console.WriteLine($"\nPrioridad: {grupo.Key}");
            foreach (var tarea in grupo)
            {
                Console.WriteLine($"ID: {tarea.Id}, Descripción: {tarea.Descripcion}, Estado: {(tarea.Completada ? "Completada" : "Pendiente")}, Fecha de Creación: {tarea.FechaCreacion}");
            }
        }
        Console.ReadLine();
    }

    static void MarcarTareaComoCompletada()
    {
        int id;
        while (true)
        {
            Console.Write("Ingrese el ID de la tarea a marcar como completada: ");
            if (int.TryParse(Console.ReadLine(), out id)) break;
            Console.WriteLine("ID no válido. Inténtalo de nuevo.");
        }

        var tarea = tareas.FirstOrDefault(t => t.Id == id);
        if (tarea != null)
        {
            tarea.MarcarComoCompletada();
            Console.WriteLine("Tarea marcada como completada.");
        }
        else
        {
            Console.WriteLine("Tarea no encontrada.");
        }
        Console.ReadLine();
    }

    static void EliminarTarea()
    {
        int id;
        while (true)
        {
            Console.Write("Ingrese el ID de la tarea a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out id)) break;
            Console.WriteLine("ID no válido. Inténtalo de nuevo.");
        }

        var tarea = tareas.FirstOrDefault(t => t.Id == id);
        if (tarea != null)
        {
            tareas.Remove(tarea);
            Console.WriteLine("Tarea eliminada.");
        }
        else
        {
            Console.WriteLine("Tarea no encontrada.");
        }
        Console.ReadLine();
    }
}


