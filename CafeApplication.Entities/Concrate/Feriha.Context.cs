﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CafeApplication.Entities.Concrate
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NusrETEntities : DbContext
    {
        public NusrETEntities()
            : base("name=NusrETEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Garsonlar> Garsonlar { get; set; }
        public virtual DbSet<Kategoriler> Kategoriler { get; set; }
        public virtual DbSet<Masalar> Masalar { get; set; }
        public virtual DbSet<Sepet> Sepet { get; set; }
        public virtual DbSet<Siparisler> Siparisler { get; set; }
        public virtual DbSet<Urunler> Urunler { get; set; }
    }
}
