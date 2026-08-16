using System;
using System.Collections.Generic;
using System.Linq;
using lab_1_POE_U20200218.Models;

namespace lab_1_POE_U20200218.Data
{
    public class EmpleadoService
    {
        public List<Empleado> GetAll()
        {
            using var db = new AppDbContext();
            return db.Empleados.OrderBy(e => e.Id).ToList();
        }

        public Empleado GetById(int id)
        {
            using var db = new AppDbContext();
            return db.Empleados.Find(id);
        }

        public void Add(Empleado empleado)
        {
            if (empleado == null) throw new ArgumentNullException(nameof(empleado));
            using var db = new AppDbContext();
            db.Empleados.Add(empleado);
            db.SaveChanges();
        }

        public void Update(Empleado empleado)
        {
            if (empleado == null) throw new ArgumentNullException(nameof(empleado));
            using var db = new AppDbContext();
            var existing = db.Empleados.Find(empleado.Id);
            if (existing == null) throw new InvalidOperationException("Empleado no encontrado");
            existing.Nombre = empleado.Nombre;
            existing.Apellido = empleado.Apellido;
            existing.DUI = empleado.DUI;
            existing.Edad = empleado.Edad;
            existing.Genero = empleado.Genero;
            existing.FechaIngreso = empleado.FechaIngreso;
            existing.Cargo = empleado.Cargo;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            using var db = new AppDbContext();
            var existing = db.Empleados.Find(id);
            if (existing == null) return;
            db.Empleados.Remove(existing);
            db.SaveChanges();
        }
    }
}
