using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Text;
using static Program;

#nullable disable

public class Program
{
    public static List<string> TeliaeHeaders = new List<string>(new string[] { 
        "Nom du remettant", 
        "Nom du destinataire", 
        "Numéro de B.L.", 
        "Code transporteur",
        "Code produit",
        "Code barre (PCI)",
        "Colis vrac",
        "Palettes Totales",
        "Volume",
        "Poids",
        "Date de départ (création du bordereau)",
        "Ref. unique d'expédition",
        "URL de tracking à l'expédition",
        "Libellé du statut (NM)",
        "Code situation",
        "Code Justification (NM)",
        "Date statut (NM)",
        "Heure statut (NM)",
        "Remarques (NM)",
        "URL Recepissé (NM)",
        "Date heure émission EDI",
        "Code EDI type de conditionnement",
        "Ref. RFF",
        "Code IATA",
        "Récépissé",
        "DonnAce textuel 1",
        "URL de tracking transporteur A  UM",
        "id UM"
    });

    public static void Main(string[] args)
    {
        List<TeliaeTracking> _TeliaeTrackingList = new List<TeliaeTracking>();
        
        // Get filenames for all files in target directory/folder
        var files = Directory.EnumerateFiles("./", "*.csv");

        // read data from all files and save it on dataTable
        foreach (string file in files)
        {
            DataTable tmpTb = new DataTable();
            using (TextFieldParser parser = new TextFieldParser(file, System.Text.Encoding.GetEncoding("iso-8859-1")))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                string[] headers = parser.ReadLine().Split(';');
                foreach (string header in headers)
                {
                    tmpTb.Columns.Add(header, typeof(string));
                    Console.WriteLine(header);
                }

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    tmpTb.Rows.Add(fields);
                }

                foreach(DataRow row in tmpTb.Rows)
                {
                    TeliaeTracking _teliae = new TeliaeTracking();
                    if (tmpTb.Columns.Contains("Nom du remettant"))
                        _teliae.NomRemettant = (string)row["Nom du remettant"];
                    if (tmpTb.Columns.Contains("Nom du destinataire"))
                        _teliae.NomDestinataire = (string)row["Nom du destinataire"];
                    if (tmpTb.Columns.Contains("Numéro de B.L."))
                        _teliae.NumeroBL = (string)row["Numéro de B.L."];
                    if (tmpTb.Columns.Contains("Code transporteur"))
                        _teliae.CodeTransporteur = (string)row["Code transporteur"];
                    if (tmpTb.Columns.Contains("Code produit"))
                        _teliae.CodeProduit = (string)row["Code produit"];
                    if (tmpTb.Columns.Contains("Code barre (PCI)"))
                        _teliae.CodeBarrePCI = (string)row["Code barre (PCI)"];
                    if (tmpTb.Columns.Contains("Colis vrac"))
                        _teliae.ColisVrac = (string)row["Colis vrac"];
                    if (tmpTb.Columns.Contains("Palettes Totales"))
                        _teliae.NbPalettes = (string)row["Palettes Totales"];
                    if (tmpTb.Columns.Contains("Volume"))
                        _teliae.Volume = (string)row["Volume"];
                    if (tmpTb.Columns.Contains("Poids"))
                        _teliae.Poids = (string)row["Poids"];
                    if (tmpTb.Columns.Contains("Date de départ (création du bordereau)"))
                        _teliae.DateDepart = (string)row["Date de départ (création du bordereau)"];
                    if (tmpTb.Columns.Contains("Ref. unique d'expédition"))
                        _teliae.RefUniqueExpedition = (string)row["Ref. unique d'expédition"];
                    if (tmpTb.Columns.Contains("URL de tracking à l'expédition"))
                        _teliae.URLTrackingExpedition = (string)row["URL de tracking à l'expédition"];
                    if (tmpTb.Columns.Contains("Libellé du statut (NM)"))
                        _teliae.LibelStatut = (string)row["Libellé du statut (NM)"];
                    if (tmpTb.Columns.Contains("Code situation"))
                        _teliae.CodeSituation = (string)row["Code situation"];
                    if (tmpTb.Columns.Contains("Code Justification (NM)"))
                        _teliae.CodeJustificationNM = (string)row["Code Justification (NM)"];
                    if (tmpTb.Columns.Contains("Date statut (NM)"))
                        _teliae.DateStatut = (string)row["Date statut (NM)"];
                    if (tmpTb.Columns.Contains("Heure statut (NM)"))
                        _teliae.HeureStatutNM = (string)row["Heure statut (NM)"];
                    if (tmpTb.Columns.Contains("Remarques (NM)"))
                        _teliae.RemarquesNM = (string)row["Remarques (NM)"];
                    if (tmpTb.Columns.Contains("URL Recepissé (NM)"))
                        _teliae.URLRecepisse = (string)row["URL Recepissé (NM)"];
                    if (tmpTb.Columns.Contains("Date heure émission EDI"))
                        _teliae.DateHeureEDI = (string)row["Date heure émission EDI"];
                    if (tmpTb.Columns.Contains("Code EDI type de conditionnement"))
                        _teliae.CodeEDIType = (string)row["Code EDI type de conditionnement"];
                    if (tmpTb.Columns.Contains("Ref. RFF"))
                        _teliae.RefRFF = (string)row["Ref. RFF"];
                    if (tmpTb.Columns.Contains("Code IATA"))
                        _teliae.CodeIATA = (string)row["Code IATA"];
                    if (tmpTb.Columns.Contains("Récépissé"))
                        _teliae.Recepisse = (string)row["Récépissé"];
                    if (tmpTb.Columns.Contains("DonnAce textuel 1"))
                        _teliae.DonnAce = (string)row["DonnAce textuel 1"];
                    if (tmpTb.Columns.Contains("URL de tracking transporteur A  UM"))
                        _teliae.URLTrackingTransporteur = (string)row["URL de tracking transporteur A  UM"];
                    if (tmpTb.Columns.Contains("id UM"))
                        _teliae.IdUM = (string)row["id UM"];

                    _TeliaeTrackingList.Add(_teliae);
                }
            }
        }

        // write dataTable to CSV file
        //StringBuilder sb = new StringBuilder();
        //DateTime dateTime = DateTime.Now;

        //IEnumerable<string> columnNames = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
        //sb.AppendLine(string.Join(";", columnNames));

        //foreach (DataRow row in dataTable.Rows)
        //{
        //    IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
        //    sb.AppendLine(string.Join(";", fields));

        //    TeliaeTracking _TeliaeTracking = new TeliaeTracking();

        //    _TeliaeTrackingList.Add(_TeliaeTracking);
        //}

        foreach (TeliaeTracking _teliae in _TeliaeTrackingList)
        {
            Console.WriteLine(_teliae.NomRemettant + "/" + _teliae.NomDestinataire);
        }

        //File.WriteAllText($"Tracking{dateTime.Year}{dateTime.Month}{dateTime.Day}_{dateTime.Hour}{dateTime.Minute}{dateTime.Second}.csv", sb.ToString());
    }

    public class TeliaeTracking
    {
        public string NCommandeOrigine { get; set; }
        public string CodeTransporteur { get; set; }
        public string NomTransporteur { get; set; }
        public string ServiceTransport { get; set; }
        public string LibelleServiceTransport { get; set; }
        public string NTracking { get; set; }
        public string LienTracking { get; set; }
        public string CodeRemettant { get; set; }
        public string DateExpedition { get; set; }
        public string DonneesTextuelles1 { get; set; } // MerchantKey

        public string NomRemettant { get; set; }
        public string NomDestinataire { get; set; }
        public string NumeroBL { get; set; }
        public string CodeProduit { get; set; }
        public string CodeBarrePCI { get; set; }
        public string ColisVrac { get; set; }
        public string NbPalettes { get; set; }
        public string Volume { get; set; }
        public string Poids { get; set; }
        public string DateDepart { get; set; }
        public string RefUniqueExpedition { get; set; }
        public string URLTrackingExpedition { get; set; }
        public string URLTrackingTransporteur { get; set; }
        public string LibelStatut { get; set; }
        public string CodeSituation { get; set; }
        public string CodeJustificationNM { get; set; }
        public string DateStatut { get; set; }
        public string HeureStatutNM { get; set; }
        public string RemarquesNM { get; set; }
        public string URLRecepisse { get; set; }
        public string DateHeureEDI { get; set; }
        public string CodeEDIType { get; set; }
        public string RefRFF { get; set; }
        public string CodeIATA { get; set; }
        public string Recepisse { get; set; }
        public string DonnAce { get; set; }
        public string IdUM { get; set; }
    }
}
