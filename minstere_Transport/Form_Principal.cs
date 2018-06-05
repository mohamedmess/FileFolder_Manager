using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Word = Microsoft.Office.Interop.Word;

namespace minstere_Transport
{
    public partial class Form_Principal : Form
    {
        public Form_Principal()
        {
            InitializeComponent();
            lblRefresh.Text = "";
            lblFiles.Text = "";
        }

        //--------------- Choose a File -----------------------------//
        private void button1_Click(object sender, EventArgs e)
        {
                
        }

        //--------------- Add Directory -----------------------------//
        private void button2_Click(object sender, EventArgs e)
        {
            lblDir.Text = "";
            lblRefresh.Text = "";
            folderBrowserDialog1.ShowDialog();
            //string dossierRacine = folderBrowserDialog1.RootFolder.ToString();
            string path = folderBrowserDialog1.SelectedPath; // read the full path
            string[] pathArr = path.Split('\\');
            string dirItem = pathArr[pathArr.Length - 1];

            if(listBoxDir.Items.Contains(path))
            { 
                lblDir.Text = "Le Dossier existe déjà";
                lblDir.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                listBoxDir.Items.Add(path);
                lblDir.Text = dirItem + " est ajouté avec succès "; // read only the directory selected
                lblDir.ForeColor = System.Drawing.Color.Green;
                
            }
        }
        //--------------- xxxxxxxxxxxxxxx -----------------------------//
        private void label5_Click(object sender, EventArgs e)
        {

        }

        //--------------- xxxxxxxxxxxxxxx -----------------------------//
        private void lblDir_Click(object sender, EventArgs e)
        {

        }

        //--------------- xxxxxxxxxxxxxxx -----------------------------//
        private void BtnQuit_Click(object sender, EventArgs e)
        {
            Form_login frm = new Form_login();
            frm.Show();
            this.Close();
        }

        //--------------- xxxxxxxxxxxxxxx -----------------------------//
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //--------------- Print Document -----------------------------//
        private void button4_Click(object sender, EventArgs e)
        {
            //printDocument1 print1 = new printDocument1();
        }

        //--------------- xxxxxxxxxxxxxxx -----------------------------//
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            lblRefresh.Text = "";
            Form_Help frm = new Form_Help();
            frm.Show();
        }

        //--------------- xxxxxxxxxxxxxxx -----------------------------//
        private void تقديمالمصلحةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblRefresh.Text = "";
            Form_Hwo frm = new Form_Hwo();
            frm.Show();
        }

        //--------------- Sauvegarder la Liste des Directories -----------------------------//
        private void button5_Click(object sender, EventArgs e)
        {
            lblDir.Text = "";
            lblRefresh.Text = "";
            List<Directoryy> list = new List<Directoryy>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Directoryy>));
            // Fill List From ListBox
            if (listBoxDir.Items.Count !=0)
            {
                list.Add(new Directoryy() { IdDir = 0, NameDir = listBoxDir.Items[0].ToString(), TypeDir = "directory" });
                int count0 = listBoxDir.Items[0].ToString().Split('\\').Length;

                for (int i=1; i< listBoxDir.Items.Count; i++)
                {
                    string[] pathArr = listBoxDir.Items[i].ToString().Split('\\');
                    //string dirItem = pathArr[pathArr.Length - 1];
       
                    if (pathArr.Length == count0)
                    {
                        Directoryy dir = new Directoryy() { IdDir = i, NameDir = listBoxDir.Items[i].ToString(), TypeDir = "directory" };
                        list.Add(dir);
                    }
                    else if(pathArr.Length == count0+1)
                    {
                        Directoryy dir = new Directoryy() { IdDir = i, NameDir = listBoxDir.Items[i].ToString(), TypeDir = "subdirectory" };
                        list.Add(dir);
                    }
                    
                }
                
            }         
            using(FileStream fs = new FileStream("C:\\Users\\Public\\Documents" + "\\Directories.xml", FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fs, list);
                lblDir.Text = "Sauvegarde réussi";
                lblDir.ForeColor = System.Drawing.Color.Green;
            }

            FillcomboBoxDir();

        }

        //--------------- Fill ComboBoxDir -----------------------------//
        // Fill ComboBoxDir from Directories.xml File
        public void FillcomboBoxDir()
        {
            //lblRefresh.Text = "";
            comboBoxDir.Items.Clear();
            comboBoxSubDir.Items.Clear();

            List<Directoryy> list = new List<Directoryy>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Directoryy>));
            using (FileStream fs = new FileStream("C:\\Users\\Public\\Documents" + "\\Directories.xml", FileMode.Open, FileAccess.Read))
            {
                list = serializer.Deserialize(fs) as List<Directoryy>;
            }
            // Extract Directory from list
            foreach (Directoryy listElement in list)
            {
                if(listElement.TypeDir == "directory")
                {
                    string[] pathArr = listElement.NameDir.Split('\\');
                    string element = pathArr[pathArr.Length - 1];
                    if(comboBoxDir.FindString(element) < 0) // fetch if comboBox contains element to add or not
                    {
                        comboBoxDir.Items.Add(element);
                    }
                }
                
            }
            
        }

        //--------------- Fill ListBox -----------------------------//
        // Fill ListBox from xml file
        public void FillListBoxDir()
        {
            lblRefresh.Text = "";
            lblDir.Text = "";
            listBoxDir.Items.Clear();
            List<Directoryy> list = new List<Directoryy>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Directoryy>));
            using (FileStream fs = new FileStream("C:\\Users\\Public\\Documents" + "\\Directories.xml", FileMode.Open, FileAccess.Read))
            {
                list = serializer.Deserialize(fs) as List<Directoryy>;
            }
            foreach(Directoryy element in list)
            {
                listBoxDir.Items.Add(element.NameDir);
            }
            /*lblDir.Text = "Mise à Jour réalisée avec succès";
            lblDir.ForeColor = System.Drawing.Color.Green;*/

        }
        // Fill DataGridView1 From XML File
        public void FillDataGridView1()
        {
            dataGridView1.Rows.Clear();
            List<Filess> list2 = new List<Filess>();
            List<Filess> list22 = new List<Filess>();
            XmlSerializer serializer2 = new XmlSerializer(typeof(List<Filess>));
            using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.OpenOrCreate, FileAccess.Read))
            {
                list2 = serializer2.Deserialize(fs2) as List<Filess>;
            }
            // list2 Sorting with Subdir ==> fill list22 that will contain data sorted buy subdirectory
            list22 = list2.OrderBy(o => o.SubDirFile).ToList();
            // Sort data in Files.xml
            using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.Create, FileAccess.Write))
            {
                serializer2.Serialize(fs2, list22);
            }

            // Fill dataGridView1 by Sorted element from list22
            if (list22.Count != 0)
            {
                foreach (Filess element in list22)
                {
                    dataGridView1.Rows.Add(element.NameFile, element.SubDirFile, new DataGridViewButtonCell(), element.RealizedFile, element.StateFile);
                }

            }
            else
            {

            }


        }

        //--------------- xxxxxxxxxxxxxxx -----------------------------//
        private void comboBoxDir_MouseClick(object sender, MouseEventArgs e)
        {
            
        }


        //--------------- xxxxxxxxxxxxxxx -----------------------------//
        // if select Dir fill combobox of subdir
        private void comboBoxDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRefresh.Text = "";
            
            comboBoxSubDir.Items.Clear();

            List<Directoryy> list = new List<Directoryy>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Directoryy>));
            using (FileStream fs = new FileStream("C:\\Users\\Public\\Documents" + "\\Directories.xml", FileMode.Open, FileAccess.Read))
            {
                list = serializer.Deserialize(fs) as List<Directoryy>;
            }
            foreach (Directoryy listElement in list)
            {
                if(listElement.TypeDir == "subdirectory")
                {
                    string[] pathArr = listElement.NameDir.Split('\\');
                    string element = pathArr[pathArr.Length - 1];
                    string elementParent = pathArr[pathArr.Length - 2];
                    string dirSelected = (String)comboBoxDir.SelectedItem;

                    
                    if (String.Equals(elementParent, dirSelected, StringComparison.Ordinal) )
                    {
                        comboBoxSubDir.Items.Add(element); // Fill ComboBoxSubDir
                    }
                    
                    
                }

            }

            

        }

        //--------------- Fill DataGridView1 -----------------------------//
        // if select SubDirectory  
        private void comboBoxSubDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRefresh.Text = "";
            dataGridView1.Rows.Clear();
            List<Filess> list = new List<Filess>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Filess>));
            using (FileStream fs = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.OpenOrCreate, FileAccess.Read))
            {
                list = serializer.Deserialize(fs) as List<Filess>;
            }

            if (list.Count != 0)
            {
                foreach (Filess element in list)
                {
                    string[] X = element.SubDirFile.Split('\\');
                    string DirName = X[X.Count() - 2];
                    string SubDirName = X[X.Count() - 1];
                    if (String.Equals(SubDirName, comboBoxSubDir.SelectedItem.ToString(),StringComparison.Ordinal) && String.Equals(comboBoxDir.SelectedItem.ToString(),DirName,StringComparison.Ordinal))
                    {
                        dataGridView1.Rows.Add(element.NameFile, element.SubDirFile, new DataGridViewButtonCell(), element.RealizedFile, element.StateFile);
                    }
                    
                }

            }
            else
            {

            }
        }
        
        //--------------- If I click on Button in DataGridView1 -----------------------------//
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblRefresh.Text = "";
            lblFiles.Text = "";
            
            List < Filess> list2 = new List<Filess>();
            XmlSerializer serializer2 = new XmlSerializer(typeof(List<Filess>));

            // If click on the button => change the value of column5 cell[realized file] and column 3 cell [realized ?]
            lblFiles.Text = "";
            if (e.ColumnIndex == 2)
            {
                openFileDialog1.Filter =
                    "Images (*.BMP;*.JPG;*.GIF;*PNG)|*.BMP;*.JPG;*.GIF;*.PNG|" +
                    "All files (*.*)|*.*";
                openFileDialog1.Title = "Fichiers Signés";
                openFileDialog1.ShowDialog();               
                string path = openFileDialog1.FileName;              
                string realizedFileName = Path.GetFileName(path);              
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = path;
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value = true;

                // open Files.xml in Read mode
                using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.Open, FileAccess.Read))
                {
                    list2 = serializer2.Deserialize(fs2) as List<Filess>;
                }
                // Find element in list2 with his filename and his subdir ==> modify same values in the located element in list2
                List<string> list_filename = new List<string>();
                List<string> list_subdir = new List<string>();
                for (int i = 0; i < list2.Count; i++)
                {
                    list_filename.Add(list2.ElementAt(i).NameFile);
                    list_subdir.Add(list2.ElementAt(i).SubDirFile);
                }
                for(int i=0; i < list_filename.Count; i++)
                {
                    if(String.Equals(list_filename.ElementAt(i), dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value.ToString(), StringComparison.Ordinal) && String.Equals(list_subdir.ElementAt(i), dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString(), StringComparison.Ordinal))
                    {
                        list2.ElementAt(i).StateFile = (bool)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value;
                        list2.ElementAt(i).RealizedFile = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString();
                    }
                }


                // open Files.xml in Write mode
                using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.Create, FileAccess.Write))
                {
                    serializer2.Serialize(fs2, list2);
                }
                lblFiles.Text = "Fichier Signé ajouté";
                lblFiles.ForeColor = System.Drawing.Color.Green;
            }

            else if(e.ColumnIndex == 3)
            {
                lblRefresh.Text = "";
                string imageFile = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();             
                ImageDisplay frm = new ImageDisplay();
                frm.Receiveparam(imageFile.ToString());
                frm.Show();
            }
        }
        

        //--------------- Update ListBox and ComboBoxDir -----------------------------//
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FillcomboBoxDir();
            FillListBoxDir();
            FillDataGridView1();
            lblFiles.Text = "";
            lblRefresh.Text = "Mise à Jour réalisée avec succès";
            lblRefresh.ForeColor = System.Drawing.Color.Green;
        }
        
        //--------------- Delete Directory -----------------------------//
        // Delete Directory or SubDirectory : if subdirectory ==> delete automatically
        private void button7_Click(object sender, EventArgs e)
        {
            lblDir.Text = "";
            
            if (MessageBox.Show("Voulez-vous supprimer les dossiers ?", "Suppression de dossiers", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                // Get the Selected Item in ListBox and get only the name of directory namedir
                string dirselected = listBoxDir.SelectedItem.ToString();
                string[] pathArr = dirselected.Split('\\');
                string namedir = pathArr[pathArr.Length - 1];

                //********** search if namedir exist in list *********
                // open File to read content and place that in list
                List<Directoryy> list = new List<Directoryy>();
                XmlSerializer serializer = new XmlSerializer(typeof(List<Directoryy>));

                List<Filess> list2 = new List<Filess>();
                XmlSerializer serializer2 = new XmlSerializer(typeof(List<Filess>));

                using (FileStream fs = new FileStream("C:\\Users\\Public\\Documents" + "\\Directories.xml", FileMode.Open, FileAccess.Read))
                {
                    list = serializer.Deserialize(fs) as List<Directoryy>;
                }
                // Fetch Selected Directory "dirselected"
                List<string> list_NameDir = new List<string>();
                List<string> list_TypeDir = new List<string>();
                for (int i = 0; i < list.Count; i++)
                {
                    list_NameDir.Add(list.ElementAt(i).NameDir);
                    list_TypeDir.Add(list.ElementAt(i).TypeDir);
                }
                if (list_NameDir.Contains(dirselected))
                {
                    int indexDir = list_NameDir.IndexOf(dirselected); // index of selected directory into list => into Directories.xml
                    if (String.Equals(list_TypeDir[indexDir], "directory", StringComparison.Ordinal))
                    {
                        //MessageBox.Show("selected directory: " + list_NameDir.ElementAt(indexDir) + " Type: " + list_TypeDir[indexDir]);
                        // ----- search in ListBox the subdirectories of selected directory --- //
                        List<string> list1 = new List<string>();
                        for (int i = 0; i < listBoxDir.Items.Count; i++)
                        {
                            string[] pathArr3 = listBoxDir.Items[i].ToString().Split('\\');
                            string dirItemparent = pathArr3[pathArr3.Length - 2];
                            if (String.Equals(namedir, dirItemparent, StringComparison.Ordinal))
                            {
                                list1.Add(listBoxDir.Items[i].ToString()); // Add all subdirectory related to selected directory in list1
                                //MessageBox.Show("sub-directory to be deleted: " + listBoxDir.Items[i].ToString());
                            }
                        }
                        //Remove files from Files.xml that his the same subdirectory to remove
                        // open Files.xml in Read mode
                        using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.Open, FileAccess.Read))
                        {
                            list2 = serializer2.Deserialize(fs2) as List<Filess>;
                        }
                        // Fill list_subdirectory from list2
                        List<string> list_subdirectory = new List<string>();
                        for(int i=0; i<list2.Count; i++)
                        {
                            list_subdirectory.Add(list2.ElementAt(i).SubDirFile);
                        }
                        // Loop into list_subdirectory and fetch if each element are the same value in list1
                        List<int> listindex = new List<int>();
                        for(int i=0; i<list_subdirectory.Count; i++)
                        {
                            if(list1.Contains(list_subdirectory[i]))
                            {
                                listindex.Add(i);
                                //MessageBox.Show("Name File: " +list2.ElementAt(i).NameFile);                              
                            }
                        }
                        List<Filess> listfileToDelete = new List<Filess>();
                        for(int i=0; i< listindex.Count; i++)
                        {
                            //MessageBox.Show("index File: " + listindex.ElementAt(i));
                            Filess filess = list2.ElementAt(i + listindex[0]);
                            listfileToDelete.Add(filess);
                            //MessageBox.Show("Added file: " + filess.NameFile);
                        }
                        foreach(Filess file in listfileToDelete)
                        {
                            list2.Remove(file);
                            //MessageBox.Show("files removed successfully ! ");
                        }
                        // open Files.xml in Write Mode
                        using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.Create, FileAccess.Write))
                        {
                            serializer2.Serialize(fs2, list2);
                        }
                        // remove selected directory and all related subdirectories from ListBoxDir
                        list1.Add(listBoxDir.SelectedItem.ToString());
                        foreach (string element1 in list1)
                        {
                            for (int i = 0; i < listBoxDir.Items.Count; i++)
                            {
                                if (String.Equals(element1, listBoxDir.Items[i].ToString(), StringComparison.Ordinal))
                                {
                                    listBoxDir.Items.Remove(listBoxDir.Items[i]);
                                }
                            }
                        }
                        //listBoxDir.Items.Remove(comboBoxDir.SelectedItem);
                        FillDataGridView1();
                        // -----------------------------------------------------//
                        lblDir.Text = "Le Dossier et leur fichiers ont été supprimé";
                        lblDir.ForeColor = System.Drawing.Color.Red;

                    }

                    else if(String.Equals(list_TypeDir[indexDir], "subdirectory", StringComparison.Ordinal))
                    {
                        // open Files.xml in Read mode
                        using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.Open, FileAccess.Read))
                        {
                            list2 = serializer2.Deserialize(fs2) as List<Filess>;
                        }
                        // Fill list_subdirectory from list2
                        List<string> list_subdirectory = new List<string>();
                        for (int i = 0; i < list2.Count; i++)
                        {
                            list_subdirectory.Add(list2.ElementAt(i).SubDirFile);
                        }
                        // Loop into list_subdirectory and fetch if each element are the same value of dirselected
                        List<int> listindex = new List<int>();
                        for (int i = 0; i < list_subdirectory.Count; i++)
                        {
                            if (String.Equals(list_subdirectory[i], dirselected, StringComparison.Ordinal))
                            {
                                listindex.Add(i);
                                //MessageBox.Show("File index= "+ i +" Name File: " + list2.ElementAt(i).NameFile);
                            }
                        }
                        List<Filess> listfileToDelete = new List<Filess>();
                        for (int i = 0 ; i < listindex.Count; i++)
                        {
                            //MessageBox.Show("index File: " + listindex.ElementAt(i));
                            Filess filess = list2.ElementAt(i+ listindex[0]);
                            listfileToDelete.Add(filess);
                            //MessageBox.Show("Added file: " + filess.NameFile);
                        }
                        foreach (Filess file in listfileToDelete)
                        {
                            list2.Remove(file);
                            //MessageBox.Show("files removed successfully ! ");
                        }
                        using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.Create, FileAccess.Write))
                        {
                            serializer2.Serialize(fs2, list2);
                        }
                        listBoxDir.Items.Remove(listBoxDir.SelectedItem);
                        //listBoxDir.Items.Remove(comboBoxDir.SelectedItem);
                        FillDataGridView1();
                        // -----------------------------------------------------//
                        lblDir.Text = "Le Sous dossier et leur fichiers ont été supprimé";
                        lblDir.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    MessageBox.Show("Pas de dossier à supprimer");
                }



            }
            else
            {

            }
            button5_Click(sender, e);


        }

        //--------------- Clear All ListBox Items -----------------------------//
        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous supprimer les dossiers ?", "Suppression de dossiers", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                List<Directoryy> list = new List<Directoryy>();
                XmlSerializer serializer = new XmlSerializer(typeof(List<Directoryy>));

                List<Filess> list2 = new List<Filess>();
                XmlSerializer serializer2 = new XmlSerializer(typeof(List<Filess>));

                using (FileStream fs = new FileStream("C:\\Users\\Public\\Documents" + "\\Directories.xml", FileMode.Open, FileAccess.Read))
                {
                    list = serializer.Deserialize(fs) as List<Directoryy>;
                }
                List<Directoryy> DirToDelete = new List<Directoryy>();
                for(int i=0; i<list.Count; i++)
                {
                    DirToDelete.Add(list.ElementAt(i));
                }
                foreach(Directoryy element in DirToDelete)
                {
                    list.Remove(element);
                }
                
                using (FileStream fs = new FileStream("C:\\Users\\Public\\Documents" + "\\Directories.xml", FileMode.Create, FileAccess.Write))
                {
                    serializer.Serialize(fs, list);
                }

                using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.Open, FileAccess.Read))
                {
                    list2 = serializer2.Deserialize(fs2) as List<Filess>;
                }
                List<Filess> FileToDelete = new List<Filess>();
                for (int i = 0; i < list2.Count; i++)
                {
                    FileToDelete.Add(list2.ElementAt(i));
                }
                foreach (Filess element in FileToDelete)
                {
                    list2.Remove(element);
                }
                using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.Create, FileAccess.Write))
                {
                    serializer2.Serialize(fs2, list2);
                }

                dataGridView1.Rows.Clear();
                listBoxDir.Items.Clear();

                lblDir.Text = "La liste est vidé";
                lblDir.ForeColor = System.Drawing.Color.Red;
            }
            else
            {

            }
            
        }

        //--------------- Add Files -----------------------------//
        private void btnAddFile_Click(object sender, EventArgs e)
        {
            lblFiles.Text = "";
            lblRefresh.Text = "";
            openFileDialog1.Reset();
            // Get the InitialDirectory to Open if ComboBoxDir is selected
            string selectedSubDir = (String)comboBoxSubDir.SelectedItem;
            string selectedDir = (String)comboBoxDir.SelectedItem;
            // open Directoryy.xml file To read the full subdirectory match selectedSubDir to use after in openFileDialog1
            List<Directoryy> list = new List<Directoryy>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Directoryy>));

            List<Filess> list2 = new List<Filess>();
            XmlSerializer serializer2 = new XmlSerializer(typeof(List<Filess>));
            using (FileStream fs = new FileStream("C:\\Users\\Public\\Documents" + "\\Directories.xml", FileMode.Open, FileAccess.Read))
            {
                list = serializer.Deserialize(fs) as List<Directoryy>;
            }
            foreach (Directoryy listElement in list)
            {
                string[] pathArr0 = listElement.NameDir.Split('\\');
                string element = pathArr0[pathArr0.Length - 1];
                string parentelement = pathArr0[pathArr0.Length - 2];
                if (listElement.TypeDir == "subdirectory" && String.Equals(selectedSubDir, element, StringComparison.Ordinal) && String.Equals(selectedDir, parentelement, StringComparison.Ordinal))
                {
                    openFileDialog1.InitialDirectory = listElement.NameDir; // Set Default Directory To OpenFileDialog
                }
            }
            openFileDialog1.Multiselect = false; // Don't Allow Dialog to select multi file
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName; // File Selected by openFileDialog1
            if(path != null)
            {
                string namefile = Path.GetFileName(path); // Path.GetFileNameWitoutExtension
                string[] pathArr = path.Split('\\');
                //string subdir = pathArr[pathArr.Length - 2]; // old value used
                string subdir = Path.GetDirectoryName(path); // full sub directory name
                if (dataGridView1.Rows.Count == 0)
                {
                    // dataGridView1 is Empty, You can Add without verification
                    dataGridView1.Rows.Add(namefile, subdir, new DataGridViewButtonCell(), "pas de Fichier Signé", false);
                    // Create Filess element to add in list2
                    string namefile2 = dataGridView1.Rows[0].Cells[0].Value.ToString();
                    string subdirfile = dataGridView1.Rows[0].Cells[1].Value.ToString();
                    bool realized = (bool)dataGridView1.Rows[0].Cells[4].Value;
                    string realizedfile = dataGridView1.Rows[0].Cells[3].Value.ToString();
                    Filess file2 = new Filess() { NameFile = namefile, SubDirFile = subdirfile, StateFile = realized, RealizedFile = realizedfile };
                    // open Files.xml => list2 => add new element to list2 ==> export to Files.xml
                    // open Files.xml in Read mode
                    using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.Open, FileAccess.Read))
                    {
                        list2 = serializer2.Deserialize(fs2) as List<Filess>;
                    }
                    // add element to list2
                    list2.Add(file2);
                    // export list2 updated to Files.xml
                    using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        serializer2.Serialize(fs2, list2);
                        
                    }

                    lblFiles.Text = "Fichier ajouté avec succès"; // read only the file selected
                    lblFiles.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    // You must verifying if the name of file exist or not
                    List<string> list_filename = new List<string>();
                    List<string> list_subdir = new List<string>();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        list_filename.Add(row.Cells[0].Value.ToString()); // list_filename contains all of the names in the Firt Column in dataGridView1
                        list_subdir.Add(row.Cells[1].Value.ToString());  // list_subdir contains all subdirectory related to filename in dataGridView1
                    }
                    // files from one subdirectory selected
                    if (list_subdir.TrueForAll(i => i.Equals(list.FirstOrDefault()))/* && String.Equals(list_subdir.First(),subdir, StringComparison.Ordinal)*/)
                    {
                        if(list_filename.Contains(namefile) == false)
                        {
                            
                            dataGridView1.Rows.Add(namefile, subdir, new DataGridViewButtonCell(), "pas de Fichier Signé", false);
                            // open File.xml in Read mode ==> create list2 ==> add new element to list2 ==> open Files.xml in Write mode
                            string namefile2 = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value.ToString();
                            string subdirfile = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value.ToString();
                            bool realized = (bool)dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value;
                            string realizedfile = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value.ToString();
                            Filess file2 = new Filess() { NameFile = namefile, SubDirFile = subdirfile, StateFile = realized, RealizedFile = realizedfile };
                            // open Files.xml => list2 => add new element to list2 ==> export to Files.xml
                            // open Files.xml in Read mode
                            using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.Open, FileAccess.Read))
                            {
                                list2 = serializer2.Deserialize(fs2) as List<Filess>;
                            }
                            // add element to list2
                            list2.Add(file2);
                            // export list2 updated to Files.xml
                            using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.OpenOrCreate, FileAccess.Write))
                            {
                                serializer2.Serialize(fs2, list2);
                            }

                            lblFiles.Text = "Fichier ajouté avec succès";
                            lblFiles.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblFiles.Text = "Le Fichier existe déjà";
                            lblFiles.ForeColor = System.Drawing.Color.Red;
                        }
    
                    }
                    // All File from all sub directory
                    else
                    {
                        if(list_filename.Contains(namefile)) // Verify if subdir equals to list_subdir element for the same index of list_filename
                        {
                            var index1 = list_filename.IndexOf(namefile);
                            if (String.Equals(list_subdir[index1],subdir,StringComparison.Ordinal))
                            {
                                lblFiles.Text = "Le Fichier existe dans le dossier " + subdir;
                                lblFiles.ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                dataGridView1.Rows.Add(namefile, subdir, new DataGridViewButtonCell(), "pas de Fichier Signé", false);
                                // open File.xml in Read mode ==> create list2 ==> add new element to list2 ==> open Files.xml in Write mode
                                string namefile2 = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value.ToString();
                                string subdirfile = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value.ToString();
                                bool realized = (bool)dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value;
                                string realizedfile = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value.ToString();
                                Filess file2 = new Filess() { NameFile = namefile, SubDirFile = subdirfile, StateFile = realized, RealizedFile = realizedfile };
                                // open Files.xml => list2 => add new element to list2 ==> export to Files.xml
                                // open Files.xml in Read mode
                                using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.Open, FileAccess.Read))
                                {
                                    list2 = serializer2.Deserialize(fs2) as List<Filess>;
                                }
                                // add element to list2
                                list2.Add(file2);
                                // export list2 updated to Files.xml
                                using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.OpenOrCreate, FileAccess.Write))
                                {
                                    serializer2.Serialize(fs2, list2);
                                }

                                lblFiles.Text = "Fichier ajouté avec succès";
                                lblFiles.ForeColor = System.Drawing.Color.Green;
                            }
                        }
                        // Add element directly in dataGridView1 and Files.xml
                        else
                        {
                            dataGridView1.Rows.Add(namefile, subdir, new DataGridViewButtonCell(), "pas de Fichier Signé", false);
                            // open File.xml in Read mode ==> create list2 ==> add new element to list2 ==> open Files.xml in Write mode
                            string namefile2 = dataGridView1.Rows[dataGridView1.Rows.Count-1].Cells[0].Value.ToString();
                            string subdirfile = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value.ToString();
                            bool realized = (bool)dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value;
                            string realizedfile = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value.ToString();
                            Filess file2 = new Filess() { NameFile = namefile, SubDirFile = subdirfile, StateFile = realized, RealizedFile = realizedfile };
                            // open Files.xml => list2 => add new element to list2 ==> export to Files.xml
                            // open Files.xml in Read mode
                            using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.Open, FileAccess.Read))
                            {
                                list2 = serializer2.Deserialize(fs2) as List<Filess>;
                            }
                            // add element to list2
                            list2.Add(file2);
                            // export list2 updated to Files.xml
                            using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.OpenOrCreate, FileAccess.Write))
                            {
                                serializer2.Serialize(fs2, list2);
                            }

                            lblFiles.Text = "Fichier ajouté avec succès";
                            lblFiles.ForeColor = System.Drawing.Color.Green;
                        }
                    }
                }


            }
            else
            {
                MessageBox.Show("Veuillez Choisir un Fichier", "Choix", MessageBoxButtons.OK); // To modify
            }
            
        }

        // Remove Selected Rows in dataGridView1
        private void button9_Click(object sender, EventArgs e)
        {
            lblFiles.Text = "";
            List<Filess> list2 = new List<Filess>();
            XmlSerializer serializer2 = new XmlSerializer(typeof(List<Filess>));
            List<DataGridViewRow> rowstoBeDeleted = new List<DataGridViewRow>();
            if (MessageBox.Show("Voulez-vous Supprimer des Fichiers ?", "Suppression de fichiers", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
            {
                
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        rowstoBeDeleted.Add(row);
                    }
                    
                    foreach (DataGridViewRow row in rowstoBeDeleted)
                    {
                        string namefile = row.Cells[0].Value.ToString();
                        string subdir   = row.Cells[1].Value.ToString();
                        string realisedfil = row.Cells[3].Value.ToString();
                        bool statefil = (bool)row.Cells[4].Value;
                        
                        // delete row from Files.xml
                        // open Files.xml in Read mode                      
                        using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.Open, FileAccess.Read))
                        {
                            list2 = serializer2.Deserialize(fs2) as List<Filess>;
                        }
                        
                        // find the Files object to be deleted from list2
                        //lblFiles.Text = list2.Count.ToString();
                        //*************************************************************************************
                        List<string> list_filename = new List<string>();
                        List<string> list_subdir = new List<string>();
                        for(int i = 0; i<list2.Count ; i++)
                        {
                            list_filename.Add(list2.ElementAt(i).NameFile);
                            list_subdir.Add(list2.ElementAt(i).SubDirFile);
                        }
                        if (list_filename.Contains(namefile))
                        {
                            var index2 = list_filename.IndexOf(namefile);
                            if (String.Equals(list_subdir[index2], subdir,StringComparison.Ordinal))
                            {
                                list2.RemoveAt(index2);
                            }
                        }
                        
                        //*************************************************************************************
                        //lblFiles.Text += list2.Count.ToString();
                        // open Files.xml in Write mode                      
                        using (FileStream fs2 = new FileStream("C:\\Users\\Public\\Documents" + "\\Files.xml", FileMode.Create, FileAccess.Write))
                        {
                            serializer2.Serialize(fs2, list2);
                        }
                        // delete row from dataGridView1
                        dataGridView1.Rows.Remove(row);
                        
                    }
                    
                    lblFiles.Text += "La Suppression est effectuée";
                    lblFiles.ForeColor = System.Drawing.Color.Red;

                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                }
                
            }
            else
            {
                
            }
            
            
        }

        // Open Word Document
        private void button10_Click(object sender, EventArgs e)
        {
            lblFiles.Text = "";
            lblRefresh.Text = "";
            
            // Create the application
            Word.Application objWord = new Word.Application();
            objWord.Visible = true;
            objWord.WindowState = Word.WdWindowState.wdWindowStateNormal;
            string wordToSave;
            /*
            if(comboBoxSubDir.SelectedItem !=null)
            {
                
            }
            */
            // Create the Document
            Word.Document objDoc = objWord.Documents.Add();
            // Add Paragraph
            //Word.Paragraph objPara;
            //objPara = objDoc.Paragraphs.Add();
            //objPara.Range.Text = "Hello Transport !";

            //objDoc.SaveAs("");
            //objDoc.Close();
            //objWord.Quit();



        }

        private void lblFiles_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_Principal_Load(object sender, EventArgs e)
        {

        }
    }
}
