// Aplicación de Gestión de Tareas con Prioridades Dinámicas
using System;
using System.Collections.Generic;

// Clase para representar una Tarea
public class Tarea
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public bool Completada { get; set; }
    public DateTime FechaCreacion { get; private set; }

    public Tarea(int id, string descripcion)
    {
        Id = id;
        Descripcion = descripcion;
        Completada = false;
        FechaCreacion = DateTime.Now;
    }

    public string CalcularPrioridad()
    {
        if (Completada) return "Baja";
        TimeSpan diferencia = DateTime.Now - FechaCreacion;
        if (diferencia.TotalDays > 3) return "Alta";
        if (diferencia.TotalDays >= 1) return "Media";
        return "Baja";
    }

    public override string ToString()
    {
        return $"ID: {Id}, Descripción: {Descripcion}, Estado: {(Completada ? "Completada" : "Pendiente")}, Prioridad: {CalcularPrioridad()}, Creada: {FechaCreacion}";
    }
}

// Clase para gestionar las tareas
public class GestorTareas
{
    private List<Tarea> tareas = new List<Tarea>();
    private int contadorId = 1;

    public void AgregarTarea(string descripcion)
    {
        var tarea = new Tarea(contadorId++, descripcion);
        tareas.Add(tarea);
        Console.WriteLine("Tarea agregada exitosamente.");
    }

    public void ListarTareas()
    {
        if (tareas.Count == 0)
        {
            Console.WriteLine("No hay tareas registradas.");
            return;
        }

        Console.WriteLine("Tareas por Prioridad:");
        foreach (var prioridad in new[] { "Alta", "Media", "Baja" })
        {
            foreach (var tarea in tareas)
            {
                if (tarea.CalcularPrioridad() == prioridad)
                {
                    Console.WriteLine(tarea);
                }
            }
        }
    }

    public void MarcarComoCompletada(int id)
    {
        var tarea = tareas.Find(t => t.Id == id);
        if (tarea != null)
        {
            tarea.Completada = true;
            Console.WriteLine("Tarea marcada como completada.");
        }
        else
        {
            Console.WriteLine("Tarea no encontrada.");
        }
    }

    public void EliminarTarea(int id)
    {
        var tarea = tareas.Find(t => t.Id == id);
        if (tarea != null)
        {
            tareas.Remove(tarea);
            Console.WriteLine("Tarea eliminada.");
        }
        else
        {
            Console.WriteLine("Tarea no encontrada.");
        }
    }
}

// Programa Principal
class Program
{
    static void Main()
    {
        var gestor = new GestorTareas();
        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine("\nMenú de Gestión de Tareas:");
            Console.WriteLine("1. Agregar Tarea");
            Console.WriteLine("2. Listar Tareas por Prioridad");
            Console.WriteLine("3. Marcar Tarea como Completada");
            Console.WriteLine("4. Eliminar Tarea");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");

            if (int.TryParse(Console.ReadLine(), out int opcion))
            {
                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese la descripción de la tarea: ");
                        string descripcion = Console.ReadLine();
                        gestor.AgregarTarea(descripcion);
                        break;
                    case 2:
                        gestor.ListarTareas();
                        break;
                    case 3:
                        Console.Write("Ingrese el ID de la tarea a completar: ");
                        if (int.TryParse(Console.ReadLine(), out int idCompletar))
                        {
                            gestor.MarcarComoCompletada(idCompletar);
                        }
                        break;
                    case 4:
                        Console.Write("Ingrese el ID de la tarea a eliminar: ");
                        if (int.TryParse(Console.ReadLine(), out int idEliminar))
                        {
                            gestor.EliminarTarea(idEliminar);
                        }
                        break;
                    case 5:
                        continuar = false;
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Entrada no válida. Intente de nuevo.");
            }
        }
    }
} // Fin del Programa

