﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace cmsTelevision.Models
{
    public partial class cmsTelevisionContext : DbContext
    {
        public cmsTelevisionContext()
        {
        }

        public cmsTelevisionContext(DbContextOptions<cmsTelevisionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accident> Accident { get; set; }
        public virtual DbSet<Demarrage> Demarrage { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Pointeuse> Pointeuse { get; set; }
        public virtual DbSet<Regle> Regle { get; set; }
        public virtual DbSet<RegleCms> RegleCms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=cmsTelevision;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accident>(entity =>
            {
                entity.ToTable("accident");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateAccident)
                    .HasColumnName("dateAccident")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Libelle)
                    .HasColumnName("libelle")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NombreBlessure).HasColumnName("nombreBlessure");

                entity.Property(e => e.NombreMort).HasColumnName("nombreMort");
            });

            modelBuilder.Entity<Demarrage>(entity =>
            {
                entity.ToTable("demarrage");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasColumnName("nombre");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("message");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateMessage)
                    .HasColumnName("dateMessage")
                    .HasColumnType("datetime");

                entity.Property(e => e.Libelle)
                    .HasColumnName("libelle")
                    .IsUnicode(false);

                entity.Property(e => e.Show).HasColumnName("show");

                entity.Property(e => e.Titre)
                    .HasColumnName("titre")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pointeuse>(entity =>
            {
                entity.ToTable("pointeuse");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Adresse)
                    .HasColumnName("adresse")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Etat).HasColumnName("etat");

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Regle>(entity =>
            {
                entity.ToTable("regle");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumOrdre).HasColumnName("numOrdre");

                entity.Property(e => e.Show).HasColumnName("show");
            });

            modelBuilder.Entity<RegleCms>(entity =>
            {
                entity.ToTable("regleCms");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumOrdre).HasColumnName("numOrdre");

                entity.Property(e => e.Show).HasColumnName("show");
            });
        }
    }
}
