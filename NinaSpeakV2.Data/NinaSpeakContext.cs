using Microsoft.EntityFrameworkCore;
using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Data.ModelsMapping;

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
            new UserModelMapping().Configure(modelBuilder.Entity<User>());
            new ChatBotModelMapping().Configure(modelBuilder.Entity<ChatBot>());
            new InstitutionModelMapping().Configure(modelBuilder.Entity<Institution>());
            new ChatBotGenreModelMapping().Configure(modelBuilder.Entity<ChatBotGenre>());
            new UserInstitutionModelMapping().Configure(modelBuilder.Entity<UserInstitution>());
            new ChatBotConversationModelMapping().Configure(modelBuilder.Entity<ChatBotConversation>());
        }
    }
}
