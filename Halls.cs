//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HistoricalMuseum
{
    using System;
    using System.Collections.Generic;
    
    public partial class Halls
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Halls()
        {
            this.ExhibitsInHalls = new HashSet<ExhibitsInHalls>();
            this.HallsInTourPrograms = new HashSet<HallsInTourPrograms>();
        }
    
        public int id { get; set; }
        public string Theme { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExhibitsInHalls> ExhibitsInHalls { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HallsInTourPrograms> HallsInTourPrograms { get; set; }
    }
}
