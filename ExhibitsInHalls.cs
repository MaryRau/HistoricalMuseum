//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HistoricalMuseum
{
    using System;
    using System.Collections.Generic;
    
    public partial class ExhibitsInHalls
    {
        public int id { get; set; }
        public int Hall { get; set; }
        public int Exhibit { get; set; }
    
        public virtual Exhibits Exhibits { get; set; }
        public virtual Halls Halls { get; set; }
    }
}
