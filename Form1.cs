namespace ConvertingImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show("The selected file cannot be opened", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Title = "Save image as...";
                sfd.OverwritePrompt = true;
                sfd.CheckPathExists = true;
                sfd.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                sfd.ShowHelp = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox2.Image.Save(sfd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Unable to save image", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap input = new Bitmap(pictureBox1.Image);
                Bitmap output = new Bitmap(input.Width, input.Height);

                for (int j = 0; j < input.Height; j++)
                {
                    for (int i = 0; i < input.Width; i++)
                    {
                        UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());

                        float R = (float)((pixel & 0x00FF0000) >> 16);
                        float G = (float)((pixel & 0x0000FF00) >> 8);
                        float B = (float)(pixel & 0x000000FF);
                        R = G = B = (R + G + B) / 3.0f;

                        UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);
                        output.SetPixel(i, j, Color.FromArgb((int)newPixel));
                    }
                }

                pictureBox2.Image = output;
            }
        }
    }
}