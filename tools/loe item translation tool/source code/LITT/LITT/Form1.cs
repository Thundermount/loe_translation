using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace LITT
{
    public partial class Form1 : Form
    {
        XmlDocument doc;
        string filename;
        List<XmlNode> ItemList;
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doc = new XmlDocument();
            ItemList = new List<XmlNode>();
            openFileDialog1.ShowDialog();
            filename = openFileDialog1.FileName;
            try
            {
                LoadXml();
                comboBox1.Enabled = true;
                button1.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка загрузки файла.");
            }
        }
        void LoadXml()
        {
            using (StreamReader sr = new StreamReader(filename, true))
            {
                doc.Load(sr);
            }
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("*");
            comboBox1.Items.Clear();
            foreach (XmlNode node in nodes)
            {
                string name = node.SelectSingleNode("Name").InnerText;
                string descr = node.SelectSingleNode("Description").InnerText;
                ItemList.Add(node);
                comboBox1.Items.Add(name);

            }
        }
        void SaveData()
        {
            ItemList[comboBox1.SelectedIndex].SelectSingleNode("Name").InnerText = nameBox2.Text;
            ItemList[comboBox1.SelectedIndex].SelectSingleNode("Description").InnerText = DescrBox2.Text.Replace("\n","");
        }
        void SaveXML()
        {
            doc.Save(filename);
            LoadXml();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            nameBox1.Text = ItemList[comboBox1.SelectedIndex].SelectSingleNode("Name").InnerText;
            DescrBox1.Text = ItemList[comboBox1.SelectedIndex].SelectSingleNode("Description").InnerText;
            nameBox2.Text = "";
            DescrBox2.Text = "";
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveXML();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка записи в файл! Возможно файл заблокирован другим процессом или не существует.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(nameBox2.Text == String.Empty || DescrBox2.Text == String.Empty)
            {
                MessageBox.Show("Осторожно! Вы попытались записать предмет с пустым названием или описанием.");
                return;
            }
            SaveData();
        }
    }
}
