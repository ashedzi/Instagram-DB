using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Instagram_DB.Models;

public partial class InstagramDbContext : DbContext
{
    public InstagramDbContext()
    {
    }

    public InstagramDbContext(DbContextOptions<InstagramDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<DirectMessage> DirectMessages { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Story> Stories { get; set; }

    public virtual DbSet<StoryLike> StoryLikes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=DESKTOP-UEPM0DL\\SQLEXPRESS;Database=Instagram DB;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFAA64DE0290");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.CommenterUserId).HasColumnName("CommenterUserID");
            entity.Property(e => e.PostId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PostID");
            entity.Property(e => e.PosterUserId).HasColumnName("PosterUserID");
            entity.Property(e => e.Timestamp).HasColumnType("datetime");

            entity.HasOne(d => d.CommenterUser).WithMany(p => p.CommentCommenterUsers)
                .HasForeignKey(d => d.CommenterUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Commenter");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Post");

            entity.HasOne(d => d.PosterUser).WithMany(p => p.CommentPosterUsers)
                .HasForeignKey(d => d.PosterUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Poster");
        });

        modelBuilder.Entity<DirectMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__DirectMe__C87C037C017184A4");

            entity.Property(e => e.MessageId).HasColumnName("MessageID");
            entity.Property(e => e.ReceiverUserId).HasColumnName("ReceiverUserID");
            entity.Property(e => e.SenderUserId).HasColumnName("SenderUserID");
            entity.Property(e => e.TextContent).HasMaxLength(400);
            entity.Property(e => e.Timestamp).HasColumnType("datetime");

            entity.HasOne(d => d.ReceiverUser).WithMany(p => p.DirectMessageReceiverUsers)
                .HasForeignKey(d => d.ReceiverUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DirectMessages_Receiver");

            entity.HasOne(d => d.SenderUser).WithMany(p => p.DirectMessageSenderUsers)
                .HasForeignKey(d => d.SenderUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DirectMessages_Sender");
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => new { e.PostId, e.LikerUserId });

            entity.Property(e => e.PostId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PostID");
            entity.Property(e => e.LikerUserId).HasColumnName("LikerUserID");
            entity.Property(e => e.PosterUserId).HasColumnName("PosterUserID");

            entity.HasOne(d => d.LikerUser).WithMany(p => p.LikeLikerUsers)
                .HasForeignKey(d => d.LikerUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Likes_Liker");

            entity.HasOne(d => d.Post).WithMany(p => p.LikesNavigation)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Likes_Post");

            entity.HasOne(d => d.PosterUser).WithMany(p => p.LikePosterUsers)
                .HasForeignKey(d => d.PosterUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Likes_Poster");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Posts__AA126038DF3D8F60");

            entity.Property(e => e.PostId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PostID");
            entity.Property(e => e.Caption).HasMaxLength(350);
            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Timestamp).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Posts_User");
        });

        modelBuilder.Entity<Story>(entity =>
        {
            entity.HasKey(e => e.StoryId).HasName("PK__Stories__3E82C0283A327E60");

            entity.Property(e => e.StoryId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("StoryID");
            entity.Property(e => e.Caption).HasColumnType("text");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Timestamp).HasColumnType("datetime");

            entity.HasOne(d => d.PosterNavigation).WithMany(p => p.StoryPosterNavigations)
                .HasForeignKey(d => d.Poster)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Stories__Poster__4BAC3F29");

            entity.HasOne(d => d.ViewerNavigation).WithMany(p => p.StoryViewerNavigations)
                .HasForeignKey(d => d.Viewer)
                .HasConstraintName("FK__Stories__Viewer__4CA06362");
        });

        modelBuilder.Entity<StoryLike>(entity =>
        {
            entity.HasKey(e => new { e.StoryId, e.Liker }).HasName("PK__StoryLik__43C1B54C74C86C01");

            entity.Property(e => e.StoryId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("StoryID");
            entity.Property(e => e.Timestamp).HasColumnType("datetime");

            entity.HasOne(d => d.LikerNavigation).WithMany(p => p.StoryLikes)
                .HasForeignKey(d => d.Liker)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StoryLike__Liker__5070F446");

            entity.HasOne(d => d.Story).WithMany(p => p.StoryLikes)
                .HasForeignKey(d => d.StoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StoryLike__Story__4F7CD00D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACB7CE7B66");

            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.HasIndex(e => e.Username, "UQ_Users_Username").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Bio).HasMaxLength(1000);
            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(25);
            entity.Property(e => e.FullName)
                .HasMaxLength(51)
                .HasComputedColumnSql("(([FirstName]+' ')+[LastName])", true);
            entity.Property(e => e.LastName).HasMaxLength(25);
            entity.Property(e => e.PostId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PostID");
            entity.Property(e => e.ProfilePic).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(30);

            entity.HasMany(d => d.FollowerUsers).WithMany(p => p.FollowingUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "Follower",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("FollowerUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Followers_Follower"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("FollowingUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Followers_Following"),
                    j =>
                    {
                        j.HasKey("FollowerUserId", "FollowingUserId");
                        j.ToTable("Followers");
                        j.IndexerProperty<int>("FollowerUserId").HasColumnName("FollowerUserID");
                        j.IndexerProperty<int>("FollowingUserId").HasColumnName("FollowingUserID");
                    });

            entity.HasMany(d => d.FollowingUsers).WithMany(p => p.FollowerUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "Follower",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("FollowingUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Followers_Following"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("FollowerUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Followers_Follower"),
                    j =>
                    {
                        j.HasKey("FollowerUserId", "FollowingUserId");
                        j.ToTable("Followers");
                        j.IndexerProperty<int>("FollowerUserId").HasColumnName("FollowerUserID");
                        j.IndexerProperty<int>("FollowingUserId").HasColumnName("FollowingUserID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
