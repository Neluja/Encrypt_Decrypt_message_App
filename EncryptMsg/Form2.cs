using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptMsg
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }
        string hash = "f0xle@rn";

        public string EncryptedText
        {
            set { txtF2Encrypt.Text = value; }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtF2Encrypt_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (txtF2Pass.Text != "Neluja")
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 back = new Form1();
            back.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (txtF2Pass.Text != "Neluja")
            {
                MessageBox.Show("First type the Public Key...");
                return;
            }
            //if (txtmsg.Text != "")
            //{
              //  MessageBox.Show("Enter the Message...");
                //return;
            //}
            else
            {
                var newform = new Form1();
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

        private void txtF2Pass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
