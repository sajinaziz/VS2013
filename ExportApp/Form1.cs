using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Transactions;


namespace ExportApp
{
    public partial class Form1 : Form
    {
        BackgroundWorker bg;
        BackgroundWorker bgDoc;
        BackgroundWorker bgDocRev;


        string OldDB = "CICON_NEW1";
        string NewDB = "CDS-TEST";

        public Form1()
        {

            InitializeComponent();
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            


           // using (var dbConn = new PgConnContext())
           // {
           //     var _submittal = dbConn.Submittals.OrderBy(x => x.Id).ToList();
           //     var subrevs  =dbConn.SubmittalRevisions.OrderBy(x=>x.Id).ToList();
           //     subrevs.ForEach(s=>s.Submittal = _submittal.Where(x=>x.Id == s.SubmittalId).First().Name);
           //     MessageBox.Show("Records Found : " + subrevs.Count.ToString());
           //     ExportCSV csvEx = new ExportCSV();
           //     csvEx.CreatingCsvFiles(subrevs, "Revisions");
           //     MessageBox.Show("Completed Export : " + subrevs.Count.ToString());
 
           //}
            
        }

        private void cmdContacts_Click(object sender, EventArgs e)
        {
            using (var dbConn = new PgConnContext())
            {
                var prjConts = dbConn.ProjectContacts.OrderBy(x => x.Id).ToList();
                MessageBox.Show("Records Found : " + prjConts.Count.ToString());
                ExportCSV csvEx = new ExportCSV();
                csvEx.CreatingCsvFiles(prjConts, "ProjectContacts");
                MessageBox.Show("Completed Export : " + prjConts.Count.ToString());

            }

        }

        private void cmdDocuments_Click(object sender, EventArgs e)
        {

            bgDoc = new BackgroundWorker();
            pgBar.Value = 0;
            bgDoc.DoWork += bgDoc_DoWork;
            bgDoc.RunWorkerAsync();
            bgDoc.ProgressChanged +=bgDoc_ProgressChanged;
            bgDoc.RunWorkerCompleted +=bgDoc_RunWorkerCompleted;
            bgDoc.WorkerReportsProgress = true; 

            }

            void bgDoc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
                MessageBox.Show("CSV Created !");
            }
            
            void bgDoc_ProgressChanged(object sender, ProgressChangedEventArgs e)
            {
                pgBar.Value = e.ProgressPercentage;
                lblStatusText.Text = e.UserState.ToString();
            }
            

            


            void bgDoc_DoWork(object sender, DoWorkEventArgs e)
            {

                    using (var dbConn = new PgConnContext())
                    {
                        List<DocumentBase> _documentBaseList = new List<DocumentBase>();
                        
                        DocumentBase _docBase;
                        
                        decimal _progress =0;
                        bgDoc.ReportProgress(1, "Loading Data....");
                        var Docs = dbConn.Documents.Include("Revision").OrderBy(x => x.Id).ToList();
                        dbConn.Database.Connection.ConnectionString = dbConn.Database.Connection.ConnectionString.Replace(OldDB, NewDB);
                        var subrevs = dbConn.SubmittalRevisions.OrderBy(x => x.Id).ToList();
                        int u = 0;
                        int _total_doc = Docs.Count;
                        foreach (var _doc in Docs)
                        {
                            u++;
                            _progress = (decimal)((decimal)u / (decimal)_total_doc) *100;
                            
                            var _subRev = subrevs.Where(r => r.RefNo == _doc.Revision.RefNo).SingleOrDefault();
                            
                            _docBase = new DocumentBase { Id = _doc.Id, Name = _doc.Name, SubmittalId = _subRev.SubmittalId };
                            if (_doc.DocumentTypeId.HasValue)
                            {
                                _docBase.DocumentTypeId = _doc.DocumentTypeId.Value;
                            }

                            _documentBaseList.Add(_docBase);

                          
                            if (_progress >= 99)
                                _progress = 99;

                            
                            bgDoc.ReportProgress((int)_progress, "Documents" + u.ToString() + "/" + _total_doc.ToString());

                        }

                        //List<DocumentBase> _dupDocList = new List<DocumentBase>();
                        //var duplicates =
                        //     _documentBaseList.GroupBy(g => new { g.SubmittalId, g.Name }).Where(grp=>grp.Count() >1).Select(g => g.First()).ToList();
                        bgDoc.ReportProgress((int)_progress, "Creating CSV Files....");
                        
                        ExportCSV csvEx = new ExportCSV();
                        csvEx.CreatingCsvFiles(_documentBaseList, "DocumentBase");
                        
                        //csvEx.CreatingCsvFiles(duplicates, "DuplicateBase");

                        bgDoc.ReportProgress(100, " CSV Created !..");
                        

                    }
                }




            void UpdateDocuemtnRevision()
            {
                
            }



        private void cmdJobSite_Click(object sender, EventArgs e)
        {
            using (var dbConn = new PgConnContext())
            {
                var _jobSites = dbConn.JobSites.OrderBy(x => x.Id).ToList();
                var _allCustomers = dbConn.Customers.OrderBy(x => x.Id).ToList();
                var _partnerIdinJobSite = _jobSites.Select(x => x.CustomerId).ToArray();
                var _customerInJobSite = _allCustomers.Where(c => _partnerIdinJobSite.Contains(c.Id.ToString())).ToList();
                MessageBox.Show("Records Found : " + _jobSites.Count.ToString());
                ExportCSV csvEx = new ExportCSV();
                csvEx.CreatingCsvFiles(_jobSites, "JobSites");
                csvEx.CreatingCsvFiles(_customerInJobSite, "Customers");
                MessageBox.Show("Completed Export : " + _jobSites.Count.ToString());

            }
        }
        
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            pgBar.Value = 0;

           bg=  new BackgroundWorker();
           bg.DoWork += bg_DoWork;
           bg.RunWorkerAsync();
           bg.ProgressChanged += bg_ProgressChanged;
           bg.RunWorkerCompleted += bg_RunWorkerCompleted;
           bg.WorkerReportsProgress = true; 

            
        }

        void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Saved In New DB !");
        }

        void bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.pgBar.Value = e.ProgressPercentage;
            lblStatusText.Text = e.UserState.ToString();

        }

        void bg_DoWork(object sender, DoWorkEventArgs e)
        {

            bg.ReportProgress(1, "Start...");
            List<SubmittalRevision> OldDbRevs = new List<SubmittalRevision>();
            using (var dbConn = new PgConnContext())
            {
                OldDbRevs = dbConn.SubmittalRevisions.OrderBy(x => x.Id).ToList();
                int _totalRec =  OldDbRevs.Count();
                bg.ReportProgress(0, "Records : " + _totalRec.ToString());
                List<SubmittalRevision> NewDbRevs = new List<SubmittalRevision>();
                dbConn.Database.Connection.ConnectionString = dbConn.Database.Connection.ConnectionString.Replace(OldDB, NewDB);
                SubmittalRevision NewDbRev;
                var _irModelData = dbConn.IrModelDatas.Where(x => x.Module == "__export__");
                int i = 0;     
                decimal _progress =0;

                foreach (var oldRev in OldDbRevs)
                {
                    i++;
                    bg.ReportProgress((int)_progress, "Records : " + i.ToString() + "/"  + _totalRec.ToString());
                    NewDbRev = new SubmittalRevision()
                    {
                        BBSWeight = oldRev.BBSWeight,
                        revision_number = oldRev.revision_number,
                        Enclosure = oldRev.Enclosure,
                        RefNo = oldRev.RefNo,
                        State = oldRev.State,
                        Subject = oldRev.Subject,
                        SubmittalDate = oldRev.SubmittalDate,
                    };

                    if (oldRev.SiteContact.HasValue)
                    {
                        string _siteContactCode = oldRev.SiteContactExternalId.Replace("__export__.", string.Empty);
                        NewDbRev.SiteContact = _irModelData.Where(x => x.Name == _siteContactCode).FirstOrDefault().NewId;
                    }
                    string _signed = oldRev.SignedByExternalId.Replace("__export__.", string.Empty);
                    NewDbRev.SignedBy = _irModelData.Where(x => x.Name == _signed).FirstOrDefault().NewId;
                    string _submittal = oldRev.SubmittalExternalId.Replace("__export__.", string.Empty);
                    NewDbRev.SubmittalId = _irModelData.Where(x => x.Name == _submittal).FirstOrDefault().NewId;
                    string _submitted = oldRev.SubmittedByExternalId.Replace("__export__.", string.Empty);
                    NewDbRev.Submitted = _irModelData.Where(x => x.Name == _submitted).FirstOrDefault().NewId;

                    NewDbRevs.Add(NewDbRev);
                    dbConn.SubmittalRevisions.Add(NewDbRev);
                    _progress = (decimal)((decimal)i / (decimal)_totalRec) *100;
                    if (_progress > 99)
                        _progress = 99;
                    bg.ReportProgress((int)_progress, "Records : " + i.ToString() + "/" + _totalRec.ToString());
                }


                bg.ReportProgress((int)_progress, "Inserting to DB : " + i.ToString() + "/" + _totalRec.ToString());
                dbConn.SaveChanges();
                bg.ReportProgress(100, "Updated Successfully !");

            }
        }

        private void cmdUpdateParent_Click(object sender, EventArgs e)
        {

            using (var dbConn = new PgConnContext())
            {             
                var _revWithParent = dbConn.SubmittalRevisions.Where(x => x.ParentId.HasValue).ToList();
                var _rev_parent_rel = _revWithParent.Select(x => new { rev= x.RefNo, parent = x.ParentRev.RefNo }).ToList() ;
                dbConn.Database.Connection.ConnectionString = dbConn.Database.Connection.ConnectionString.Replace(OldDB, NewDB);

                var _parentNames = _rev_parent_rel.Select(x=>x.parent).ToList();
                //var _newDbRevWithParent = dbConn.SubmittalRevisions.Where(x => x.ParentId.HasValue).ToList();
                var _newDbParentRevs = dbConn.SubmittalRevisions.Where(x=> _parentNames.Contains(x.RefNo)).ToList();


                foreach (var _rev_has_parent in _rev_parent_rel)
                {
                    var _rev = dbConn.SubmittalRevisions.Where(x => x.RefNo == _rev_has_parent.rev).FirstOrDefault();
                    _rev.ParentId = _newDbParentRevs.Where(x => x.RefNo == _rev_has_parent.parent).FirstOrDefault().Id;
                    
                }
                dbConn.SaveChanges();

                MessageBox.Show("Updated Scuccessfully !");

            }

        }

        private void cmdDocRev_Click(object sender, EventArgs e)
        {
            pgBar.Value = 0;
            bgDocRev = new BackgroundWorker();
            bgDocRev.DoWork += bgDocRev_DoWork;
            bgDocRev.ProgressChanged += bgDocRev_ProgressChanged;
            bgDocRev.RunWorkerCompleted += bgDocRev_RunWorkerCompleted;
            bgDocRev.WorkerReportsProgress = true;


            bgDocRev.RunWorkerAsync();
            

        }

        void bgDocRev_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Updated !");
        }

        void bgDocRev_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.pgBar.Value = e.ProgressPercentage;
            lblStatusText.Text = e.UserState.ToString();
        }

        void bgDocRev_DoWork(object sender, DoWorkEventArgs e)
        {


            DocumentRevision _docRev;

            using (var dbConn = new PgConnContext())
            {
                dbConn.Configuration.AutoDetectChangesEnabled = false;
                List<DocumentRevision> _docRevList = new List<DocumentRevision>();
                var Docs = dbConn.Documents.Include("Revision").OrderBy(x => x.Id).ToList();
                dbConn.Database.Connection.ConnectionString = dbConn.Database.Connection.ConnectionString.Replace(OldDB, NewDB);
                var subrevs = dbConn.SubmittalRevisions.OrderBy(x => x.Id).ToList();
                var irData  = dbConn.IrModelDatas.Where(x => x.Model == "tech.submittal.revision.document").ToList();
                int i = 0;
                int _totalRec = Docs.Count();
                decimal _progress = 0;
                foreach (var _doc in Docs)
                {
                    i++;
                    _progress = (decimal)((decimal)i / (decimal)_totalRec) * 100;

                    var _subRev = subrevs.Where(r => r.RefNo == _doc.Revision.RefNo).SingleOrDefault();
                    var _newDoc = irData.Where(x=> x.Name == _doc.ExternalId.Replace("__export__.", string.Empty)).SingleOrDefault();

                    _docRev = new DocumentRevision
                    {
                        Status = _doc.Status,
                        Description = _doc.Description,
                        RevisionId = _subRev.Id.Value,
                        CreatedBy = _subRev.Submitted,
                        RevNo = _subRev.revision_number,
                        Date = _subRev.SubmittalDate
                    };

                    if (_newDoc != null)
                    {
                        _docRev.DocumentId = _newDoc.NewId;
                    }

                    _docRevList.Add(_docRev);
                    //dbConn.DocumentRevisions.Add(_docRev);

                    if (_progress > 99)
                        _progress = 99;

                    bgDocRev.ReportProgress((int)_progress, "Updating Records : " + i.ToString() + "/" + _totalRec.ToString());

                }



                i = 0;
                _progress = 0;
                foreach (var d in _docRevList)
                {
                    i++;
                    _progress = (decimal)((decimal)i / (decimal)_totalRec) * 100;

                    dbConn.DocumentRevisions.Add(d);

                    bgDocRev.ReportProgress((int)_progress, "Saving Database : " + i.ToString() + "/" + _totalRec.ToString());
                }
                dbConn.SaveChanges();

                //ExportCSV csvEx = new ExportCSV();

                //csvEx.CreatingCsvFiles(_docRevisionList, "Documents Revision");

              

              

                bgDocRev.ReportProgress(100, "Updated Successfully !");
            }
        }
    }
}

