using Microsoft.EntityFrameworkCore;
using OpenTodo.Models;


namespace OpenTodo.Data
{

    public class OpenTodoContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var userTable = modelBuilder.Entity<UserSchema>();
            var taskTable = modelBuilder.Entity<TaskSchema>();
            var boardTable = modelBuilder.Entity<BoardSchema>();
            var categoryTable = modelBuilder.Entity<CategorySchema>();
            var boardParticipantTable = modelBuilder.Entity<BoardParticipantSchema>();
            userTable.ToTable("user");
            taskTable.ToTable("task");
            boardTable.ToTable("board");
            categoryTable.ToTable("category");
            boardParticipantTable.ToTable("board_participant");
            userTable.Property(u => u.FirstName).HasMaxLength(30).IsRequired();
            userTable.Property(u => u.LastName).HasMaxLength(30).IsRequired();
            userTable.Property(u => u.Username).HasMaxLength(30).IsRequired();
            userTable.Property(u => u.PasswordHash).HasMaxLength(200).IsRequired();
            taskTable.Property(r => r.Title)
            .IsRequired()
            .HasColumnType("varchar").HasMaxLength(255);
            taskTable.Property(t => t.Description).IsRequired();
            taskTable.Property(t => t.Category).IsRequired().HasColumnType("smallint");
            var now = DateTime.Now.ToUniversalTime();
            taskTable.Property(t => t.CreatedAt).HasDefaultValue(now);
            boardParticipantTable.Property(t => t.JoinedAt).HasDefaultValue(now);
    
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }
        public DbSet<UserSchema> Users { get; set; }
        public DbSet<TaskSchema> Tasks { get; set; }
        public DbSet<BoardSchema> Boards { get; set; }
        public DbSet<CategorySchema> Categories { get; set; }
        public DbSet<BoardParticipantSchema> BoardParticipants {get; set;}
    }



    
}
