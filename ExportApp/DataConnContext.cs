using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportApp
{
    public class PgConnContext : DbContext
    {

        public PgConnContext()
            : base("PgConnStr")
        { 

            
        }


        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ProjectContact> ProjectContacts { get; set; }

        public virtual DbSet<TechSubmittal> Submittals { get; set; }

        public virtual DbSet<SubmittalRevision> SubmittalRevisions { get; set; }
        
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<JobSite> JobSites { get; set; }
        public virtual DbSet<IrModelData> IrModelDatas { get; set; }
        public virtual DbSet<DocumentRevision> DocumentRevisions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { 

        }

    }
    
    

    [Table("res_partner",Schema="public")]
    public class Customer
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public string ExternalId
        {
            get { return "__export__.res_partner_" + Id.ToString(); }
        }
        [Column("name")]
        public string Name { get; set; }
        public bool IsaCompnay
        {
            get { return true; }
        }
        public string company_type
        {
            get { return "Company"; }
        }


    }





    [Table("tech_project_contact", Schema = "public")]
    public class ProjectContact
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public string ExternalId
        {
            get { return "__export__.tech_project_contact_" + Id.ToString(); }
        }
        [Column("name")]
        public string Name { get; set; }
        [Column("salutation")]
        public string Salutation { get; set; }
        public int job_site_id { get; set; }
        public string JobSiteExternalId
        {
            get { return "__export__.cic_job_site_" + job_site_id.ToString(); }
        }
        public string designation { get; set; }
        public string email { get; set; }
    }


    [Table("tech_submittal", Schema = "public")]
    public class TechSubmittal
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public string ExternalId
        {
            get { return "__export__.tech_submittal_" + Id.ToString(); }
        }
        [Column("name")]
        public string Name { get; set; }

        public int? submittal_common_count { get; set; }
        public int submittal_project_count { get; set; }
        public int job_site_id { get; set;}
        public string JobSiteExternalId
        {
            get { return "__export__.cic_job_site_" + job_site_id.ToString(); }
        }
        public int company_id { get; set; }
        //public int partner_id { get; set; }
        //public string CustomerExternalId
        //{
        //    get { return "__export__.res_partner_" + partner_id.ToString(); }
        //}

    }



    [Table("tech_submittal_revision", Schema = "public")]
    public class SubmittalRevision
    {

        [Key]
        [Column("id")]
        public int? Id { get; set; }
        [NotMapped]
        public string ExternalId
        {
            get { return "__export__.tech_submittal_revision_" + Id.ToString(); }
        }

        [Column("submittal_id")]
        public int SubmittalId {get;set;}
        [NotMapped]
        public string Submittal { get; set; }
        [NotMapped]
        public string SubmittalExternalId
        {
            get { return "__export__.tech_submittal_" + SubmittalId.ToString(); }
        }
        public int revision_number {get;set;}
        [Column("subject")]
        public string Subject {get;set;}
        [Column("submitted_by")]
        public int Submitted {get;set;}
        [NotMapped]
        public string SubmittedByExternalId
        {
            get { return "__export__.res_users_" + Submitted.ToString(); }
        }
        [Column("ref_no")]
        public string RefNo  {get;set;}
        [Column("submittal_date")]
        public DateTime SubmittalDate {get;set;}
        [Column("bbs_weight")]
        public decimal BBSWeight {get;set;}
        [Column("parent_id")]
        public int? ParentId {get;set;}
        [ForeignKey("ParentId")]
        public virtual SubmittalRevision ParentRev {get;set;}
        [NotMapped]
        public string ParentExternalId
        {
            get
            {
                if (ParentId.HasValue)
                {
                    return "__export__.tech_submittal_revision_" + ParentId.ToString();
                }
                else
                {
                    return "";
                }
            }

        }
        
        [Column("state")]
        public string State {get;set;}
        [Column("signed_by")]
        public int SignedBy {get;set;}
        [NotMapped]
        public string SignedByExternalId
        {
            get { return "__export__.res_users_" + SignedBy.ToString(); }
        }
        [Column("enclosures")]
        public string Enclosure { get; set; }
        [Column("job_site_contact")]
        public int? SiteContact { get; set; }
        [NotMapped]
        public string SiteContactExternalId
        {
            get { return "__export__.tech_project_contact_" + SiteContact.ToString(); }
        }
        
        //[Column("create_date")]
        [NotMapped]
        public DateTime CreateDate
        {
            get { return DateTime.Now; }
        }
        //[Column("write_date")]
        [NotMapped]
        public DateTime WriteDate
        {
            get { return DateTime.Now; }
        }
        //[Column("write_uid")]
        [NotMapped]
        public int WriteUserId
        {
            get { return 1;}
        }
        //[Column("create_uid")]
        [NotMapped]
        public int CreateUserId
        {
            get { return 1; }
        }


    }


    [Table("ir_model_data", Schema = "public")]
    public class IrModelData
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("model")]
        public string Model { get; set; }
        [Column("res_id")]
        public int NewId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("module")]
        public string Module { get; set; }
      

    }




    [Table("tech_submittal_revision_document", Schema = "public")]
    public class Document
    {

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [NotMapped]
        public string ExternalId
        {
            get { return "__export__.tech_submittal_revision_document_" + Id.ToString(); }
        }
        [Column("revision_id")]
        public int RevisionId { get; set; }
        [ForeignKey("RevisionId")]
        public virtual SubmittalRevision Revision { get; set; }
        public string RevisionExternalId
        {
            get { return "__export__.tech_submittal_revision_" + RevisionId.ToString(); }
        }
        [Column("document_status")]
        public string Status { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("document_type_id")]
        public int? DocumentTypeId { get; set; }
        [Column("description")]
        public string Description { get; set; }

    }


   
    public class DocumentBase
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [NotMapped]
        public string ExternalId
        {
            get { return "__export__.tech_submittal_revision_document_" + Id.ToString(); }
        }

        [Column("submittal_id")]
        public int SubmittalId { get; set; }
        
        [NotMapped]
        public string SubmittalExternalId
        {
            get { return "__export__.tech_submittal_" + SubmittalId.ToString(); }
        }

        [Column("document_type_id")]
        public int DocumentTypeId { get; set; }

    }



    [Table("tech_submittal_document_revision", Schema = "public")]
    public class DocumentRevision
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("revision_id")]
        public int RevisionId { get; set; }
        [NotMapped]
        public string RevisionExternalId
        {
            get { return "__export__.tech_submittal_revision_" + RevisionId.ToString(); }
        }
        [Column("document_status")]
        public string Status { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("rev_no")]
        public int RevNo { get; set; }
        [Column("created_by")]
        public int CreatedBy { get; set;}
        [Column("document_id")]
        public int DocumentId { get; set; }
        //[ForeignKey("DocumentId")]
        //public virtual DocumentBase DocumentBase { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [NotMapped]
        public string DocumentExternalId
        {
            get { return "__export__.tech_submittal_revision_document_" + DocumentId.ToString(); }
        }
    }


    [Table("cic_job_site", Schema = "public")]
    public class JobSite
    {
        [Column("id")]
        public int Id { get; set; }
        public string ExternalId
        {
            get { return "__export__.cic_job_site_" + Id.ToString(); }
        }
        [Column("coordinator_id")]
        public int CoOrdinatorId { get; set; }
        public string CoOrdinatorExternalId
        {
            get { return "__export__.res_users_" + CoOrdinatorId.ToString(); }
        }
        [Column("name")]
        public string name { get; set; }
        [Column("site_ref_no")]      
        public string SiteRef { get; set; }
        [Column("partner_id")]      
        public string CustomerId { get; set; }
        public string CustomerExternalId
        {
            get { return "__export__.res_partner_" + CustomerId.ToString(); }
        }

        public string telephone { get; set; }
        public string fax { get; set; }
        public string po_box { get; set; }

    }




        
}
