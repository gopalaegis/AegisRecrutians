﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CONSTRUCTION.DataTable
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AEGIS_Entities : DbContext
    {
        public AEGIS_Entities()
            : base("name=AEGIS_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblCategoryMaster> tblCategoryMasters { get; set; }
        public virtual DbSet<tblCity> tblCities { get; set; }
        public virtual DbSet<tblCountry> tblCountries { get; set; }
        public virtual DbSet<tblJobDetail> tblJobDetails { get; set; }
        public virtual DbSet<tblSkillMaster> tblSkillMasters { get; set; }
        public virtual DbSet<tblState> tblStates { get; set; }
        public virtual DbSet<tblSubCategoryMaster> tblSubCategoryMasters { get; set; }
        public virtual DbSet<tblTechnologyMaster> tblTechnologyMasters { get; set; }
    }
}
