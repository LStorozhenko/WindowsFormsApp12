using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Management;

namespace WindowsFormsApp12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            try
            {
                GetProcessorInfo();
                GetVideoCardInfo();
                GetDVDInfo();
                GetDiskDriveInfo();
                GetMotherboardInfo();
                GetNetworkAdapterInfo();
                GetBIOSInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при отриманні інформації: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetProcessorInfo()
        {
            List<string> processorInfo = GetHardwareInfo("Win32_Processor", "Name");
            OutputResult("Процесор:", processorInfo);
            OutputResult("Виробник:", GetHardwareInfo("Win32_Processor", "Manufacturer"));
            OutputResult("Опис:", GetHardwareInfo("Win32_Processor", "Description"));
            richTextBox1.AppendText("\n");
        }

        private void GetVideoCardInfo()
        {
            OutputResult("Відеокарта:", GetHardwareInfo("Win32_VideoController", "Name"));
            OutputResult("Відеопроцесор:", GetHardwareInfo("Win32_VideoController", "VideoProcessor"));
            OutputResult("Версія драйверу:", GetHardwareInfo("Win32_VideoController", "DriverVersion"));
            OutputResult("Об'єм пам'яті (в байтах):", GetHardwareInfo("Win32_VideoController", "AdapterRAM"));
            richTextBox1.AppendText("\n");
        }

        private void GetDVDInfo()
        {
            OutputResult("Назва DVD:", GetHardwareInfo("Win32_CDROMDrive", "Name"));
            OutputResult("Буква DVD:", GetHardwareInfo("Win32_CDROMDrive", "Drive"));
            richTextBox1.AppendText("\n");
        }

        private void GetDiskDriveInfo()
        {
            OutputResult("Жорсткий диск:", GetHardwareInfo("Win32_DiskDrive", "Caption"));
            OutputResult("Об'єм (в байтах):", GetHardwareInfo("Win32_DiskDrive", "Size"));
            richTextBox1.AppendText("\n");
        }

        private void GetMotherboardInfo()
        {
            OutputResult("Материнська плата:", GetHardwareInfo("Win32_BaseBoard", "Product"));
            richTextBox1.AppendText("\n");
        }

        private void GetNetworkAdapterInfo()
        {
            OutputResult("Мережевий адаптер:", GetHardwareInfo("Win32_NetworkAdapter", "Name"));
            richTextBox1.AppendText("\n");
        }

        private void GetBIOSInfo()
        {
            OutputResult("BIOS:", GetHardwareInfo("Win32_BIOS", "Manufacturer"));
            richTextBox1.AppendText("\n");
        }

        private List<string> GetHardwareInfo(string win32Class, string classItemField)
        {
            List<string> result = new List<string>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + win32Class);
            try
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    result.Add(obj[classItemField].ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Помилка при отриманні інформації: " + ex.Message);
            }
            return result;
        }

        private void OutputResult(string info, List<string> result)
        {
            richTextBox1.AppendText(info + "\n");
            for (int i = 0; i < result.Count; ++i)
                richTextBox1.AppendText(result[i] + "\n");
        }
    }
}
