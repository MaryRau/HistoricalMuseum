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
    
    public partial class HallsInTourPrograms
    {
        public int id { get; set; }
        public int TourProgram { get; set; }
        public int Hall { get; set; }
    
        public virtual Halls Halls { get; set; }
        public virtual TourPrograms TourPrograms { get; set; }
    }
}
