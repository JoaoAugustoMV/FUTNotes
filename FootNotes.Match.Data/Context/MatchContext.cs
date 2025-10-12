using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.MatchManagement.Domain.CompetitionModels;
using FootNotes.MatchManagement.Domain.MatchModels;
using FootNotes.MatchManagement.Domain.TeamModels;
using Microsoft.EntityFrameworkCore;

namespace FootNotes.MatchManagement.Data.Context
{
    public class MatchContext(DbContextOptions<MatchContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureMatch(modelBuilder);
            ConfigureTeam(modelBuilder);
            ConfigureCompetition(modelBuilder);            
        }

        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Competition> Competitions { get; set; }

        #region Configuration Methods
        private static void ConfigureMatch(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>(entity =>
            {                
                entity.ToTable("matches");
                
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(e => e.AwayPenaltyScore)
                    .HasColumnName("away_penalty_score");

                entity.Property(e => e.AwayScore)
                    .HasColumnName("away_score");
                    
                entity.Property(e => e.AwayTeamId)
                    .HasColumnName("away_team_id")
                    .IsRequired();
                entity.Property(e => e.CompetitionId)
                    .HasColumnName("competition_id");
                    
                entity.Property(e => e.DecisionType)
                    .HasColumnName("decision_type");

                entity.Property(e => e.HomePenaltyScore)
                    .HasColumnName("home_penalty_score");

                entity.Property(e => e.HomeTeamId)
                    .HasColumnName("home_team_id")
                    .IsRequired();

                entity.Property(e => e.HomeScore)
                    .HasColumnName("home_score");

                entity.Property(e => e.MatchDate)
                    .HasColumnName("match_date");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .IsRequired();  

                entity.Ignore(e => e.Events);
            });
        }

        private static void ConfigureTeam(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("teams");
                
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ShortName)
                    .HasColumnName("short_name")
                    .HasMaxLength(50);

                entity.Property(e => e.CoachId)
                    .HasColumnName("coach_id");

                entity.Ignore(e => e.Events);
            });
        }

        private static void ConfigureCompetition(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Competition>(entity =>
            {
                entity.ToTable("competitions");
                
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .IsRequired()
                    .HasMaxLength(255);
                
                entity.Property(e => e.Season)
                    .HasColumnName("season")
                    .HasMaxLength(50);

                entity.Property(e => e.Scope)
                    .HasColumnName("scope")
                    .IsRequired();

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .IsRequired();

                entity.Ignore(e => e.Events);
            });
        }


        #endregion
    }
}
