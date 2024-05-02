﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MuseumEntities : DbContext
    {
        private static MuseumEntities _context;
        public MuseumEntities()
            : base("name=MuseumEntities")
        {
        }

        public static MuseumEntities GetContext()
        {
            if (_context == null)
                _context = new MuseumEntities();
            return _context;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Exhibits> Exhibits { get; set; }
        public virtual DbSet<ExhibitsInHalls> ExhibitsInHalls { get; set; }
        public virtual DbSet<ExhibitsInStoreroom> ExhibitsInStoreroom { get; set; }
        public virtual DbSet<ExhibitsTypes> ExhibitsTypes { get; set; }
        public virtual DbSet<Halls> Halls { get; set; }
        public virtual DbSet<HallsInTourPrograms> HallsInTourPrograms { get; set; }
        public virtual DbSet<HistoricalEpochs> HistoricalEpochs { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<TourEntries> TourEntries { get; set; }
        public virtual DbSet<TourPrograms> TourPrograms { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<GuidedToursView> GuidedToursView { get; set; }
        public virtual DbSet<PopulariryPfProgram> PopulariryPfProgram { get; set; }
    }
}
