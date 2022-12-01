﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DiBa_LIB
{
    public class Inbox
    {
        #region Data Members
        private Pengguna pengguna;
        private int id;
        private string pesan;
        private DateTime tanggal_kirim;
        private string status;
        private DateTime tanggal_perubahan;
        #endregion

        #region Constructors
        public Inbox(Pengguna pengguna, int id, string pesan, DateTime tanggal_kirim, string status, DateTime tanggal_perubahan)
        {
            this.Pengguna = pengguna;
            this.Id = id;
            this.Pesan = pesan;
            this.Tanggal_kirim = tanggal_kirim;
            this.Status = status;
            this.Tanggal_perubahan = tanggal_perubahan;
        }
        public Inbox(int id)
        {
            Id = id;
        }
        #endregion

        public Inbox(int id, string pesan, DateTime tanggal_kirim, string status, DateTime tanggal_perubahan)
        {
            this.id = id;
            this.pesan = pesan;
            this.tanggal_kirim = tanggal_kirim;
            this.status = status;
            this.tanggal_perubahan = tanggal_perubahan;
        }

        #region Properties
        public Pengguna Pengguna { get => pengguna; set => pengguna = value; }
        public int Id { get => id; set => id = value; }
        public string Pesan { get => pesan; set => pesan = value; }
        public DateTime Tanggal_kirim { get => tanggal_kirim; set => tanggal_kirim = value; }
        public string Status { get => status; set => status = value; }
        public DateTime Tanggal_perubahan { get => tanggal_perubahan; set => tanggal_perubahan = value; }
        #endregion

        #region Methods
        public static List<Inbox> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select i.id_pengguna, i.id_pesan, i.pesan, i.tanggal_kirim, i.status, i.tgl_perubahan from " +
                    "inbox i inner join pengguna p on p.nik = i.id_pengguna";
            }
            else
            { 
                sql = "select i.id_pengguna, i.id_pesan, i.pesan, i.tanggal_kirim, i.status, i.tgl_perubahan from " +
                    "inbox i inner join pengguna p on p.nik = i.id_pengguna where "+kriteria+" LIKE '%"+nilaiKriteria+"%'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Inbox> listInbox = new List<Inbox>();
            while (hasil.Read() == true)
            {
                Pengguna p = new Pengguna(hasil.GetValue(0).ToString(), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                                          hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(), hasil.GetValue(5).ToString(),
                                          hasil.GetValue(6).ToString(), hasil.GetValue(7).ToString(), DateTime.Parse(hasil.GetValue(8).ToString()),
                                          DateTime.Parse(hasil.GetValue(9).ToString()));
                Inbox i = new Inbox(p, int.Parse(hasil.GetString(10)), hasil.GetString(11), DateTime.Parse(hasil.GetString(12)),
                    hasil.GetString(13), DateTime.Parse(hasil.GetString(14)));
                listInbox.Add(i);
            }
            return listInbox;
        }

        public static void TambahData(Inbox i)
        {
            string sql = "insert into inbox (id_pesan, pesan, tanggal_kirim, status, tgl_perubahan) values ("+i.Id+", " +
                "'"+i.Pesan+"', '"+i.Tanggal_kirim+"', '"+i.Status+"', '"+i.Tanggal_perubahan+"')";
            Koneksi.JalankanPerintahDML(sql);
        }

        public static void UbahData(Inbox i)
        {
            string sql = "update inbox set id_pesan = "+i.Id+", pesan = '"+i.Pesan+"', tanggal_kirim = '"+
                i.Tanggal_kirim+"', status = '"+i.Status+"', tgl_perubahan = '"+i.Tanggal_perubahan+"'";
            Koneksi.JalankanPerintahDML(sql);
        }

        public static void HapusData(Inbox i)
        {
            string sql = "DELETE from inbox where id = " + i.Id;

            Koneksi.JalankanPerintahDML(sql);
        }
        #endregion
    }
}
