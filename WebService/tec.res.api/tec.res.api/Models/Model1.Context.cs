﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace tec.res.api.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TEConstruyeEntities : DbContext
    {
        public TEConstruyeEntities()
            : base("name=TEConstruyeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<admin> admin { get; set; }
        public virtual DbSet<arquitecto> arquitecto { get; set; }
        public virtual DbSet<cliente> cliente { get; set; }
        public virtual DbSet<diseña> diseña { get; set; }
        public virtual DbSet<empleado> empleado { get; set; }
        public virtual DbSet<especialidad> especialidad { get; set; }
        public virtual DbSet<etapa> etapa { get; set; }
        public virtual DbSet<gasto> gasto { get; set; }
        public virtual DbSet<ingeniero> ingeniero { get; set; }
        public virtual DbSet<labora_en> labora_en { get; set; }
        public virtual DbSet<material> material { get; set; }
        public virtual DbSet<obra> obra { get; set; }
        public virtual DbSet<requiere> requiere { get; set; }
        public virtual DbSet<tiene> tiene { get; set; }
        public virtual DbSet<trabaja_en> trabaja_en { get; set; }
        public virtual DbSet<ubicacion> ubicacion { get; set; }
    }
}
