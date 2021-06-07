using Microsoft.EntityFrameworkCore;
namespace Work.Models
{
    public class PersonContext : DbContext
    {
        #region Соединение моделей с базами данных
        public PersonContext(DbContextOptions<PersonContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        public PersonContext()
        {

        }

        public DbSet<MainPersonModel> Persons { get; set; }
        public DbSet<Department> Deportaments { get; set; }
        public DbSet<Language> Languages { get; set; }
        #endregion
    }
}