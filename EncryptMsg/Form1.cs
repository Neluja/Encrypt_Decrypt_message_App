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
    public partial class Form1 : Form
    {
        public string Pass;
        public string Msg;
        public Form1()
        {
            InitializeComponent();
        }
        string hash = "f0xle@rn";

        public string EncryptedText
        {
            set { txtF2Encrypt.Text = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            if (txtMsg.Text == "")
            {
                MessageBox.Show("First type the Message...");
                return;
            }
            if (txtPass.Text != "Neluja")
            {
                MessageBox.Show("Enter Password");
                return;
            }
            else
            {
                var newform = new Form2();
                Visible = false;

                byte[] data = UTF8Encoding.UTF8.GetBytes(txtMsg.Text);
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnEncrypt_MouseLeave(object sender, EventArgs e)
        {
           ;
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (txtPass.Text != "Neluja")
            {
                MessageBox.Show("Enter Password");
                return;
            }
            else
            {
                byte[] data = Convert.FromBase64String(txtF2Encrypt.Text);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateDecryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        txtF2Dencrypt.Text = UTF8Encoding.UTF8.GetString(results);

                    }

                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form3 back = new Form3();
            back.Show();
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
