using Microsoft.EntityFrameworkCore;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Data.EntitiesMapping;

namespace NinaSpeakV2.Data
{
    public class NinaSpeakContext : DbContext
    {
        public NinaSpeakContext(DbContextOptions<NinaSpeakContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<User> User { get; set; }

        public DbSet<Institution> Institution { get; set; }

        public DbSet<UserInstitution> UserInstitution { get; set; }

        public DbSet<ChatBot> ChatBot { get; set; }

        public DbSet<ChatBotGenre> ChatBotGenre { get; set; }

        public DbSet<ChatBotConversation> ChatBotConversation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserEntityMapping().Configure(modelBuilder.Entity<User>());
            new ChatBotEntityMapping().Configure(modelBuilder.Entity<ChatBot>());
            new InstitutionEntityMapping().Configure(modelBuilder.Entity<Institution>());
            new ChatBotGenreEntityMapping().Configure(modelBuilder.Entity<ChatBotGenre>());
            new UserInstitutionEntityMapping().Configure(modelBuilder.Entity<UserInstitution>());
            new ChatBotConversationEntityMapping().Configure(modelBuilder.Entity<ChatBotConversation>());
        }
    }
}
