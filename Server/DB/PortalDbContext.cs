

using CandidatePortal.Shared.Entities;
using Microsoft.EntityFrameworkCore;


namespace CandidatePortal.Server.DB
{
    public class PortalDbContext : DbContext
    {
        public PortalDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortalDbContext).Assembly);
        }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillLevel> SkillLevels { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<EducationLevel> EducationLevel { get; set; }
        public DbSet<CertificateType> CertificateTypes { get; set; }
        public DbSet<PersonalTitle> PersonalTitles { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateEducation> CandidateEducations { get; set; }
        public DbSet<CandidateSkill> CandidateSkills { get; set; }
        public DbSet<CandidateCertificateType> CandidateCertificateTypes { get; set; }
        public DbSet<CandidateExperience> CandidateExperiences { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Application> Applications { get; set; }

        public DbSet<ApplicationCommunication> ApplicationCommunications { get; set; }
        public DbSet<ApplicationCommunicationDocument> ApplicationCommunicationDocuments { get; set; }
        public DbSet<ApplicationChecklist> ApplicationChecklists { get; set; }
        public DbSet<ApplicationChecklistDocument> ApplicationChecklistDocuments { get; set; }

        public DbSet<ApplicationQuestionnaire> ApplicationQuestionnaire { get; set; }
        public DbSet<ApplicationQuestionnaireLine> ApplicationQuestionnaireLine { get; set; }
        public DbSet<ApplicationQuestionnaireLineAnswer> ApplicationQuestionnaireLineAnswer { get; set; }
    }
}
