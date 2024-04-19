using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SGRA2._0.Model;

namespace SGRA2._0.Context
{
    public class PersonDBContext : DbContext    
    {
        public PersonDBContext(DbContextOptions<PersonDBContext> options) : base(options)
        { }
        public DbSet<Person> persons { get; set; }
        public DbSet<DocumentType> documentTypes { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Sale> sales {  get; set; } 
        public DbSet<Employee>  employees { get; set; }
        public DbSet<Suppliers>  suppliers { get; set; }
        public DbSet<Transaction>  transactions { get; set; }
        public DbSet<CollectWaste>  collectWastes { get; set; }
        public DbSet<WasteType>  wasteTypes { get; set; }
        public DbSet<Composter>  composters { get; set; }
        public DbSet<Waste>  wastes {  get; set; } 
        public DbSet<ChemicalComposition>  chemicalCompositions { get; set; }
        public DbSet<Time> times {  get; set; }   
        public DbSet<ProcessStage> processStages {  get; set; }  
        public DbSet<Flip> flips {  get; set; }  
        public DbSet<Temperature> temperatures {  get; set; } 
        public DbSet<FinalCompost> finalComposts {  get; set; } 
        public DbSet<RecordTime> recordTimes {  get; set; } 
        public DbSet<Level> levels {  get; set; }    
        public DbSet<Achievements> achievements {  get; set; } 
        public DbSet<Games> games {  get; set; } 
        public DbSet<AchievementsGames> achievementsGames {  get; set; }     
        public DbSet<Score> scores {  get; set; } 
        public DbSet<User> users {  get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;

            }
        }

    }
}
