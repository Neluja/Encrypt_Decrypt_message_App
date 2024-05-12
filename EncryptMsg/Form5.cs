using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptMsg
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        string hash = "f0xle@rn";
        public string EncryptedText
        {
            set { txtbox1.Text = value; }
        }
        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtprivate.Text != "4")
            {
                MessageBox.Show("Enter Password");
                return;
            }
            else
            {
                byte[] data = Convert.FromBase64String(txtbox1.Text);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateDecryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        txtbox2.Text = UTF8Encoding.UTF8.GetString(results);

                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtpublic.Text != "1")
            {
                MessageBox.Show("Enter Public Key");
                return;
            }
            
            else
            {

                var newform = new Form4();
                Visible = false;

                byte[] data = UTF8Encoding.UTF8.GetBytes(txtmsg.Text);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider()
                    {
                        Key = keys,
                        Mode = CipherMode.ECB,
                        Padding = PaddingMode.PKCS7
                    })
                    {
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        string encryptedText = Convert.ToBase64String(results, 0, results.Length);

                        // Pass encrypted text to Form2
                        newform.EncryptedText = encryptedText;

                        newform.Show();
                    }



                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtbox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtbox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form4 back = new Form4();
            back.Show();
        }
    }
}
