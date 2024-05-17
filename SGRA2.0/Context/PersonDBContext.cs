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
        public DbSet<User> users {  get; set; }
        public DbSet<PersonLogin> personLogins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //
            modelBuilder.Entity<Achievements>()
                .HasKey(e => e.IdAchievements);


            modelBuilder.Entity<ChemicalComposition>()
                .HasKey(e => e.IdChemicalComposition);

            modelBuilder.Entity<CollectWaste>()
                .HasKey(e => e.IdCollectWaste);

            modelBuilder.Entity<Composter>()
                .HasKey(e => e.IdComposter);

            modelBuilder.Entity<Customer>()
                .HasKey(e => e.IdCustomer);

            modelBuilder.Entity<DocumentType>()
                .HasKey(e => e.IdDocumentType);

            modelBuilder.Entity<Employee>()
                .HasKey(e => e.IdEmployee);

            modelBuilder.Entity<FinalCompost>()
                .HasKey(e => e.IdFinalCompost);

            modelBuilder.Entity<Flip>()
                .HasKey(e => e.IdFlip);

            modelBuilder.Entity<Games>()
                .HasKey(e => e.IdGames);

            modelBuilder.Entity<Level>()
                .HasKey(e => e.IdLevel);

            modelBuilder.Entity<Person>()
                .HasKey(e => e.IdPerson);

            modelBuilder.Entity<ProcessStage>()
                .HasKey(e => e.IdProcessStage);

            modelBuilder.Entity<RecordTime>()   
                .HasKey (e => e.IdRecordTime);

            modelBuilder.Entity<Sale>()
                .HasKey (e => e.IdSale);

            modelBuilder.Entity<Suppliers>()
                .HasKey (e =>e.IdSuppliers);    

            modelBuilder.Entity<Temperature>()  
                .HasKey (e => e.IdTemperature);

            modelBuilder.Entity<Time>()
                .HasKey (e => e.IdTime);

            modelBuilder.Entity<Transaction>()
                .HasKey (e => e.IdTransaction);

            modelBuilder.Entity<User>()
                .HasKey (e => e.IdUser);

            modelBuilder.Entity<Waste>()
                .HasKey (e =>e.IdWaste);

            modelBuilder.Entity<WasteType>()
                .HasKey (e =>e.IdWasteType);

            modelBuilder.Entity<PersonLogin>()
                .HasKey(e => e.IdLoginP);

            //Foreign Key
            modelBuilder.Entity<Waste>()
                .HasOne(e => e.WasteType).WithMany()
                .HasForeignKey(e => e.IdWasteType);

            modelBuilder.Entity<Transaction>()
                .HasOne(e => e.Suppliers).WithMany()
                .HasForeignKey (e => e.IdSuppliers);

            modelBuilder.Entity<Time>()
                .HasOne(e => e.Waste).WithMany()
                .HasForeignKey(e => e.IdWaste);
            modelBuilder.Entity<Time>()
                .HasOne(e => e.ProcessStage).WithMany()
                .HasForeignKey(e => e.IdProcessStage);

            modelBuilder.Entity<Temperature>()
                .HasOne(e => e.Waste).WithMany()
                .HasForeignKey(e => e.IdWaste);

            modelBuilder.Entity<Suppliers>()
                .HasOne(e => e.Person).WithMany()
                .HasForeignKey(e => e.IdPerson);
            modelBuilder.Entity<Suppliers>()
                .HasOne(e => e.WasteType).WithMany()
                .HasForeignKey(e => e.IdWasteType);
            
            modelBuilder.Entity<Sale>()
                .HasOne(e => e.Customer).WithMany()
                .HasForeignKey(e =>e.IdCustomer); 
            
            modelBuilder.Entity<RecordTime>()
                .HasOne(e => e.Level).WithMany()
                .HasForeignKey(e => e.IdLevel);

            modelBuilder.Entity<Person>()
                .HasOne(e => e.DocumentType).WithMany()
                .HasForeignKey(e => e.IdDocumentType);

            modelBuilder.Entity<Games>()
                .HasOne(e => e.Level).WithMany()
                .HasForeignKey(e => e.IdLevel);

            modelBuilder.Entity<Flip>()
                .HasOne(e => e.Waste).WithMany()
                .HasForeignKey(e => e.IdWaste);

            modelBuilder.Entity<FinalCompost>()
                .HasOne(e => e.Waste).WithMany()
                .HasForeignKey(e =>e.IdWaste);
            
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Person).WithMany()
                .HasForeignKey(e => e.IdPerson);

            modelBuilder.Entity<Customer>()
                .HasOne(e => e.Person).WithMany()
                .HasForeignKey(e =>e.IdPerson);

            modelBuilder.Entity<CollectWaste>()
                .HasOne(e => e.Suppliers).WithMany()
                .HasForeignKey(e => e.IdSuppliers);
            modelBuilder.Entity<CollectWaste>()
                .HasOne(e => e.Composter).WithMany()
                .HasForeignKey(e => e.IdComposter);

            modelBuilder.Entity<ChemicalComposition>()
                .HasOne(e => e.Waste).WithMany()
                .HasForeignKey(e => e.IdWaste);
            
            modelBuilder.Entity<Achievements>()
                .HasOne(e => e.User).WithMany()
                .HasForeignKey(e => e.IdUser);
            modelBuilder.Entity<Achievements>()
                .HasOne(e =>e.Games).WithMany()
                .HasForeignKey(e =>e.IdGames);

            modelBuilder.Entity<PersonLogin>()
                .HasOne(e => e.Person).WithMany()
                .HasForeignKey(e => e.IdPerson);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;

            }
        }

    }
}
