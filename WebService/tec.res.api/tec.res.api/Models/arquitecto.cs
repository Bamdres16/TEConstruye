//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class arquitecto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public arquitecto()
        {
            this.trabaja_en = new HashSet<trabaja_en>();
        }
    
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string cedula { get; set; }
        public string numero_telefono { get; set; }
        public string codigo_arquitecto { get; set; }
        public Nullable<int> id_especialidad { get; set; }
        public int id { get; set; }
    
        public virtual especialidad especialidad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<trabaja_en> trabaja_en { get; set; }
    }
}