using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Цветочный_магазин.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> orders { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            orders = new HashSet<Order>();
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public virtual DbSet<Flower> Flowers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Flower>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<Flower>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Flower>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<Flower>()
                .HasMany(e => e.orders)
                .WithOptional(e => e.flower)
                .HasForeignKey(e => e.id_flower)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ApplicationUser>()
               .HasMany(e => e.orders)
               .WithOptional(e => e.User)
               .HasForeignKey(e => e.id_user)
               .WillCascadeOnDelete();


            modelBuilder.Entity<Order>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.surname)
                .IsUnicode(false);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}