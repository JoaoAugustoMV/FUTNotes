using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Annotations.Domain.AnnotationSession;
using FootNotes.Annotations.Domain.TagModels;
using FootNotes.Core.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FootNotes.Annotations.Data.Context
{
    public class AnnotationsContext(DbContextOptions<AnnotationsContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureAnnotationSession(modelBuilder);
            ConfigureAnnotation(modelBuilder);
            ConfigureTag(modelBuilder);
            ConfigureAnnotationTag(modelBuilder);

            modelBuilder.Ignore<Event>();
        }

        #region Configuration Methods
        private static void ConfigureAnnotationSession(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnnotationSession>(entity =>
            {                
                entity.ToTable("annotation_sessions");
                
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .IsRequired();

                entity.Property(e => e.MatchId)
                    .HasColumnName("match_id")
                    .IsRequired();
                    
                entity.Property(e => e.Started)
                    .HasColumnName("started")
                    .IsRequired();
                    
                entity.Property(e => e.Ended)
                    .HasColumnName("ended");

                entity.Property(e => e.Status)
                    .HasColumnName("status");

                entity.Property(e => e.Type)
                    .HasColumnName("type");

                entity.HasMany(s => s.Annotations)
                    .WithOne(a => a.AnnotationSession)
                    .HasForeignKey(a => a.AnnotationSessionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private static void ConfigureAnnotation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Annotation>(entity =>
            {                
                entity.ToTable("annotations");

                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(e => e.AnnotationSessionId)
                    .HasColumnName("annotation_session_id")
                    .IsRequired();

                entity.Property(e => e.TimeStamp)
                    .HasColumnName("time_stamp")
                    .IsRequired();
                    
                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .IsRequired();
                    
                entity.Property(e => e.Description)
                    .HasColumnName("description");

                entity.Property(e => e.Minute)
                    .HasColumnName("minute");

            });
        }
        
        private static void ConfigureTag(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>(entity =>
            {                
                entity.ToTable("tags");

                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsRequired();
                    
                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .IsRequired();

                entity.Property(e => e.IsDefault)
                    .HasColumnName("is_default");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .IsRequired();
            });
        }

        private static void ConfigureAnnotationTag(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnnotationTag>(entity =>
            {                

                entity.ToTable("annotation_tags");
                entity.HasKey(e => new { e.AnnotationId, e.TagId });
                
                entity.Property(e => e.AnnotationId)
                    .HasColumnName("annotation_id")
                    .IsRequired();
                    
                entity.Property(e => e.TagId)
                    .HasColumnName("tag_id")
                    .IsRequired();
            });
        }

        #endregion
    }
}
