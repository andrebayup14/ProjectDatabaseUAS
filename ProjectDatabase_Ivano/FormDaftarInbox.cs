using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiBa_LIB;

namespace ProjectDatabase_Ivano
{
    public partial class FormDaftarInbox : Form
    {
        public List<Inbox> listInbox = new List<Inbox>();

        public List<Pengguna> listPengguna = new List<Pengguna>();

        Koneksi k;
        public FormDaftarInbox()
        {
            InitializeComponent();
        }

        public void FormDaftarInbox_Load(object sender, EventArgs e)
        {
            k = new Koneksi();
            listInbox = Inbox.BacaData("", "");
            if (listInbox.Count > 0)
            {
                dataGridViewInbox.DataSource = listInbox;
                if (dataGridViewInbox.ColumnCount == 6)
                {
                    DataGridViewButtonColumn bcol1 = new DataGridViewButtonColumn();
                    bcol1.HeaderText = "Aksi";
                    bcol1.Text = "Ubah Data";
                    bcol1.Name = "buttonUbahGrid";
                    bcol1.UseColumnTextForButtonValue = true;
                    dataGridViewInbox.Columns.Add(bcol1);

                    DataGridViewButtonColumn bcol2 = new DataGridViewButtonColumn();
                    bcol2.HeaderText = "Aksi";
                    bcol2.Text = "Hapus Data";
                    bcol2.Name = "buttonHapusGrid";
                    bcol2.UseColumnTextForButtonValue = true;
                    dataGridViewInbox.Columns.Add(bcol2);
                }
            }
            else
            {
                dataGridViewInbox.DataSource = null;
            }
        }

        private void ButtonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            FormTambahInbox frmTambahInbox = new FormTambahInbox();
            frmTambahInbox.Owner = this;
            frmTambahInbox.Show();
        }

        private void dataGridViewInbox_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewInbox.Columns["buttonUbahGrid"].Index && e.RowIndex >= 0)
            {
                FormUbahInbox frmUbahInbox = new FormUbahInbox();
                frmUbahInbox.Owner = this;
               
                listPengguna = Pengguna.BacaData("", "");
                frmUbahInbox.comboBoxPengguna.DataSource = listPengguna;
                frmUbahInbox.comboBoxPengguna.DisplayMember = "Nik";

                frmUbahInbox.comboBoxPengguna.Text = dataGridViewInbox.CurrentRow.Cells["pengguna"].Value.ToString();
                frmUbahInbox.textBoxPesan.Text = dataGridViewInbox.CurrentRow.Cells["pesan"].Value.ToString();
                frmUbahInbox.ShowDialog();
            }
            else if (e.ColumnIndex == dataGridViewInbox.Columns["buttonHapusGrid"].Index && e.RowIndex >= 0)
            {
                string nik = dataGridViewInbox.CurrentRow.Cells["pengguna"].Value.ToString();
                string nama = Pengguna.AmbilNamaLengkap(nik);
                int id_pesan = int.Parse(dataGridViewInbox.CurrentRow.Cells["id"].Value.ToString());
                string pesan= dataGridViewInbox.CurrentRow.Cells["pesan"].Value.ToString();
                DateTime tanggal_kirim = DateTime.Parse(dataGridViewInbox.CurrentRow.Cells["tanggal_kirim"].Value.ToString());
                string status = dataGridViewInbox.CurrentRow.Cells["status"].Value.ToString();

                DialogResult hasil = MessageBox.Show("Data yang akan dihapus adalah " +
                                                     "\nID Pengguna : " + nik + 
                                                     "\nNama : " + nama +
                                                     "\nPesan : " + pesan + 
                                                     "\nTanggal Kirim : " + tanggal_kirim.ToShortDateString() +
                                                     "\nStatus : " + status + 
                                                     "\n\nApakah anda yakin ingin menghapus data di atas?", 
                                                     "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (hasil == DialogResult.Yes)
                {
                    Inbox i = new Inbox(id_pesan);
                    Inbox.HapusData(i, k);
                    MessageBox.Show("Data berhasil dihapus.", "Informasi");
                    FormDaftarInbox_Load(buttonKeluar, e);
                }
            }
        }

        private void textBoxNilaiKriteria_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxKriteria.Text == "ID Pengguna")
            {
                listInbox = Inbox.BacaData("id_pengguna", textBoxNilaiKriteria.Text);
            }
            else if (comboBoxKriteria.Text == "ID Pesan")
            {
                listInbox = Inbox.BacaData("id_pesan", textBoxNilaiKriteria.Text);
            }
            else if (comboBoxKriteria.Text == "Pesan")
            {
                listInbox = Inbox.BacaData("pesan", textBoxNilaiKriteria.Text);
            }
            else if (comboBoxKriteria.Text == "Tanggal Kirim")
            {
                listInbox = Inbox.BacaData("tanggal_kirim", textBoxNilaiKriteria.Text);
            }
            else if (comboBoxKriteria.Text == "Status")
            {
                listInbox = Inbox.BacaData("status", textBoxNilaiKriteria.Text);
            }
            else if (comboBoxKriteria.Text == "Tanggal Perubahan")
            {
                listInbox = Inbox.BacaData("tgl_perubahan", textBoxNilaiKriteria.Text);
            }

            if (listInbox.Count > 0)
            {
                dataGridViewInbox.DataSource = listInbox;
            }
            else
            {
                dataGridViewInbox.DataSource = null;
            }
        }

        private void comboBoxKriteria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
