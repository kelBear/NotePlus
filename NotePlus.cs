using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace WindowsFormsApplication1
{
    public partial class NotePlus : Form
    {

        private int TabCount = 0;

        public NotePlus()
        {
            InitializeComponent();
        }

        #region Methods

            #region Tabs

            private void AddTab()   
            {
                RichTextBox Body = new RichTextBox();

                Body.Name = "Body";
                Body.Dock = DockStyle.Fill;
                Body.ContextMenuStrip = contextMenuStrip1;

                TabPage NewPage = new TabPage();
                TabCount += 1;

                string DocumentText = "Document " + TabCount;
                NewPage.Name = DocumentText;
                NewPage.Text = DocumentText;
                NewPage.Controls.Add(Body);

                tabControl1.TabPages.Add(NewPage);
            }

            private void RemoveTab()
            {

                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                TabCount--;

                if (tabControl1.TabPages.Count < 1)
                {
                    AddTab();
                }
            }

            private void RemoveAllTabs()
            {
                foreach (TabPage Page in tabControl1.TabPages)
                {
                    DialogResult savePrompt;
                    savePrompt = MessageBox.Show("Do you want to save this tab?", "Save Prompt", MessageBoxButtons.YesNoCancel);
                    if (savePrompt == DialogResult.Yes)
                        Save();
                    else if (savePrompt == DialogResult.No)
                    {
                        tabControl1.TabPages.Remove(Page);
                        TabCount--;
                    }
                }

                AddTab();
            }

            private void RemoveAllTabsButThis()
            {
                foreach (TabPage Page in tabControl1.TabPages)
                {
                    if (Page.Name != tabControl1.SelectedTab.Name)
                    {
                        DialogResult savePrompt;
                        savePrompt = MessageBox.Show("Do you want to save this tab?", "Save Prompt", MessageBoxButtons.YesNoCancel);
                        if (savePrompt == DialogResult.Yes)
                            Save();
                        else if (savePrompt == DialogResult.No)
                        {
                            tabControl1.TabPages.Remove(Page);
                            TabCount--;
                        }
                    }
                }
            }

            #endregion

            #region SaveAndOpen

                private void Save()
                {
                    saveFileDialog1.FileName = tabControl1.SelectedTab.Name;
                    saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    saveFileDialog1.Filter = "Text Files|*.txt|VB Files|*.vb|C# Files|*.cs|All Files|*.*";
                    saveFileDialog1.Title = "Save";

                    if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (saveFileDialog1.FileName.Length > 0)
                        {
                            GetCurrentDocument.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                        }
                    }
                }

                private void SaveAs()
                {
                    saveFileDialog1.FileName = tabControl1.SelectedTab.Name;
                    saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    saveFileDialog1.Filter = "Text Files|*.txt|VB Files|*.vb|C# Files|*.cs|All Files|*.*";
                    saveFileDialog1.Title = "Save As";

                    if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (saveFileDialog1.FileName.Length > 0)
                        {
                            GetCurrentDocument.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                        }
                    }
                }

               private void Open()
               {
                   openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                   openFileDialog1.Filter = "Text Files|*.txt|VB Files|*.vb|C# Files|*.cs|All Files|*.*";

                   if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                   {
                       if (openFileDialog1.FileName.Length > 9)
                       {
                           GetCurrentDocument.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                       }
                   }
               }
    
            #endregion

        #endregion

        #region Properties

               private RichTextBox GetCurrentDocument
               {
                   get { 
                       return (RichTextBox)tabControl1.SelectedTab.Controls["Body"]; 
                   }
               }

        #endregion

        #region TextFunctions

               private void Undo()
               {
                   GetCurrentDocument.Undo();
               }

               private void Redo()
               {
                   GetCurrentDocument.Redo();
               }
               private void Cut()
               {
                   GetCurrentDocument.Cut();
               }

               private void Copy()
               {
                   GetCurrentDocument.Copy();
               }

               private void Paste()
               {
                   GetCurrentDocument.Paste();
               }

               private void SelectAll()
               {
                   GetCurrentDocument.SelectAll();
               }

        #endregion

        #region General

               private void GetFontCollection()
               {
                   InstalledFontCollection InsFonts = new InstalledFontCollection();

                   foreach (FontFamily item in InsFonts.Families)
                   {
                       toolStripComboBox1.Items.Add(item.Name);
                   }
                   toolStripComboBox1.SelectedIndex = 0;
               }

               private void PopulateFontSizes()
               {
                   for (int i = 1; i <= 75; i++)
                   {
                       toolStripComboBox2.Items.Add(i);
                   }

                   toolStripComboBox2.SelectedIndex = 11;
               }

        #endregion

               private void undoToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   Undo();
               }

               private void redoToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   Redo();
               }

               private void cutToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   Cut();
               }

               private void copyToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   Copy();
               }

               private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   Paste();
               }

               private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   SelectAll();
               }

               private void openToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   Open();
               }

               private void saveToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   Save();
               }

               private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   SaveAs();
               }

               private void toolStripTextBox1_Click(object sender, EventArgs e)
               {
                   Undo();
               }

               private void toolStripTextBox2_Click(object sender, EventArgs e)
               {
                   Redo();
               }

               private void toolStripTextBox3_Click(object sender, EventArgs e)
               {
                   Cut();
               }

               private void toolStripTextBox4_Click(object sender, EventArgs e)
               {
                   Copy();
               }

               private void toolStripTextBox5_Click(object sender, EventArgs e)
               {
                   Paste();
               }

               private void toolStripTextBox6_Click(object sender, EventArgs e)
               {
                   Save();
               }

               private void toolStripTextBox7_Click(object sender, EventArgs e)
               {
                   DialogResult savePrompt;
                   savePrompt = MessageBox.Show("Do you want to save this tab?", "Save Prompt", MessageBoxButtons.YesNoCancel);
                   if (savePrompt == DialogResult.Yes)
                       Save();
                   else if (savePrompt == DialogResult.No)
                       RemoveTab();  
               }

               private void toolStripTextBox8_Click(object sender, EventArgs e)
               {
                   RemoveAllTabs();
               }

               private void toolStripTextBox9_Click(object sender, EventArgs e)
               {
                   RemoveAllTabsButThis();
               }

               private void openToolStripButton1_Click(object sender, EventArgs e)
               {
                   Open();
               }

               private void saveToolStripButton1_Click(object sender, EventArgs e)
               {
                   Save();
               }

               private void cutToolStripButton1_Click(object sender, EventArgs e)
               {
                   Cut();
               }

               private void copyToolStripButton1_Click(object sender, EventArgs e)
               {
                   Copy();
               }

               private void pasteToolStripButton1_Click(object sender, EventArgs e)
               {
                   Paste();
               }

               private void RemoveTabToolStripButton_Click(object sender, EventArgs e)
               {
                   DialogResult savePrompt;
                   savePrompt = MessageBox.Show("Do you want to save this tab?", "Save Prompt", MessageBoxButtons.YesNoCancel);
                   if (savePrompt == DialogResult.Yes)
                       Save();
                   else if (savePrompt == DialogResult.No)
                       RemoveTab();                
               }

               private void BoldButton_Click(object sender, EventArgs e)
               {
                   Font BoldFont = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold);
                   Font RegularFont = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);

                   if (GetCurrentDocument.SelectionFont.Bold)
                   {
                       GetCurrentDocument.SelectionFont = RegularFont;
                   }
                   else
                   {
                       GetCurrentDocument.SelectionFont = BoldFont;
                   }
               }

               private void ItalicButton_Click(object sender, EventArgs e)
               {
                   Font ItalicFont = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic);
                   Font RegularFont = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);

                   if (GetCurrentDocument.SelectionFont.Italic)
                   {
                       GetCurrentDocument.SelectionFont = RegularFont;
                   }
                   else
                   {
                       GetCurrentDocument.SelectionFont = ItalicFont;
                   }
               }

               private void UnderlineButton_Click(object sender, EventArgs e)
               {
                   Font UnderlineFont = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline);
                   Font RegularFont = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);

                   if (GetCurrentDocument.SelectionFont.Underline)
                   {
                       GetCurrentDocument.SelectionFont = RegularFont;
                   }
                   else
                   {
                       GetCurrentDocument.SelectionFont = UnderlineFont;
                   }
               }

               private void StrikeButton_Click(object sender, EventArgs e)
               {
                   Font StrikeFont = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout);
                   Font RegularFont = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);

                   if (GetCurrentDocument.SelectionFont.Strikeout)
                   {
                       GetCurrentDocument.SelectionFont = RegularFont;
                   }
                   else
                   {
                       GetCurrentDocument.SelectionFont = StrikeFont;
                   }
               }

               private void UpperButton_Click(object sender, EventArgs e)
               {
                   GetCurrentDocument.SelectedText = GetCurrentDocument.SelectedText.ToUpper();
               }

               private void LowerButton_Click(object sender, EventArgs e)
               {
                   GetCurrentDocument.SelectedText = GetCurrentDocument.SelectedText.ToLower();
               }

               private void IncSizeButton_Click(object sender, EventArgs e)
               {
                   float NewFontSize = GetCurrentDocument.SelectionFont.SizeInPoints + 2;

                   Font NewSize = new Font(GetCurrentDocument.SelectionFont.Name, NewFontSize, GetCurrentDocument.SelectionFont.Style);

                   GetCurrentDocument.SelectionFont = NewSize;
               }

               private void DecSizeButton_Click(object sender, EventArgs e)
               {
                   if (GetCurrentDocument.SelectionFont.SizeInPoints > 2)
                   {
                       float NewFontSize = GetCurrentDocument.SelectionFont.SizeInPoints - 2;

                       Font NewSize = new Font(GetCurrentDocument.SelectionFont.Name, NewFontSize, GetCurrentDocument.SelectionFont.Style);

                       GetCurrentDocument.SelectionFont = NewSize;
                   }
               }

               private void ForeColourButton_Click(object sender, EventArgs e)
               {
                   if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                   {
                       GetCurrentDocument.SelectionColor = colorDialog1.Color;
                   }
               }

               private void HlGreen_Click(object sender, EventArgs e)
               {
                   GetCurrentDocument.SelectionBackColor = Color.LightGreen;
               }

               private void HlOrange_Click(object sender, EventArgs e)
               {
                   GetCurrentDocument.SelectionBackColor = Color.Orange;
               }

               private void HiYellow_Click(object sender, EventArgs e)
               {
                   GetCurrentDocument.SelectionBackColor = Color.Yellow;
               }

               private void HlPink_Click(object sender, EventArgs e)
               {
                   GetCurrentDocument.SelectionBackColor = Color.Magenta;
               }

               private void HlBlue_Click(object sender, EventArgs e)
               {
                   GetCurrentDocument.SelectionBackColor = Color.LightCyan;
               }

               private void toolStripComboBox1_Click(object sender, EventArgs e)
               {
                   Font NewFont = new Font(toolStripComboBox1.SelectedItem.ToString(), GetCurrentDocument.SelectionFont.Size, GetCurrentDocument.SelectionFont.Style);

                   GetCurrentDocument.SelectionFont = NewFont;
               }

               private void toolStripComboBox2_Click(object sender, EventArgs e)
               {
                   float NewSize;

                   float.TryParse(toolStripComboBox2.SelectedItem.ToString(), out NewSize);

                   Font NewFont = new Font(GetCurrentDocument.SelectionFont.Name, NewSize, GetCurrentDocument.SelectionFont.Style);

                   GetCurrentDocument.SelectionFont = NewFont;
               }

               private void timer1_Tick(object sender, EventArgs e)
               {
                   if (GetCurrentDocument.Text.Length > 0)
                   {
                       toolStripStatusLabel1.Text = GetCurrentDocument.Text.Length.ToString();
                   }
               }

               private void NotePlus_Load(object sender, EventArgs e)
               {
                   AddTab();
                   GetFontCollection();
                   PopulateFontSizes();
               }

               private void newToolStripButton1_Click(object sender, EventArgs e)
               {
                   AddTab();
               }

               private void newToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   AddTab();
               }

               private void exitToolStripMenuItem_Click(object sender, EventArgs e)
               {
                   DialogResult savePrompt;
                   savePrompt = MessageBox.Show("Do you want to save this tab?", "Save Prompt", MessageBoxButtons.YesNoCancel);
                   if (savePrompt == DialogResult.Yes)
                       Save();
                   else if (savePrompt == DialogResult.No)
                       this.Close();
               }

    }
}
