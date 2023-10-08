using SAPbobsCOM; //���� namespace �ͧ SAP DI API

namespace DemoInterfaceSAPB1
{
    public partial class Form1 : Form
    {
        private int ErrorCode; //��С�ȵ���� ErrorCode ����Ѻ�纤�� Error Code �ҡ SAP Business One
        private string ErrorMessage; //��С�ȵ���� ErrorMessage ����Ѻ�纤�� Error Description �ҡ SAP Business One
        private Company company; //��С�ȵ���� company �ͧ������ Company

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_TestLogin_Click(object sender, EventArgs e)
        {
            bool result = SAPConnect(); //���ҧ����� result ��С�˹���������ҡѺ���Ѿ��ҡ������¡���ʹ SAPConnect() ��觨Ф׹��� true ���������������� ��� false �������������������

            if (result) //��Ǩ�ͺ�����������������������
            {
                MessageBox.Show("Connected successfully!"); //�ҡ�����������������ʴ� 'Connected successfully!" � Message Box
            }
            else
            {
                string error = string.Format(@"Coonected unsuccessfully!, Error: {0} - {1}", ErrorCode, ErrorMessage); //�红�ͤ��� error

                MessageBox.Show(error); //�ʴ���ͤ��� Error �ó�����������������
            }
        }

        private bool SAPConnect()
        {
            company = new Company();

            //��˹���Ҿ������������Ѻ Instance company
            company.Server = "MY-NOTEBOOK\\MSSQLSERVER2019"; //��˹����� Server �¨��繷���� Database �ͧ Company ����ö����ҡ˹�� Login �ͧ SAP B1
            company.CompanyDB = "SBODemoAU"; //��˹����Ͱҹ�����Ţͧ Company ����ö����ҡ˹�� Login �ͧ SAP B1 ��������� 'Database Name'
            company.UserName = "manager"; //��˹����ͼ���� SAP B1
            company.Password = "1234"; //��˹����ʼ�ҹ SAP B1
            company.DbServerType = BoDataServerTypes.dst_MSSQL2019; //��˹��������ͧ�ҹ������

            int result = company.Connect(); //�ӡ��������������纤�Ҽ��Ѿ��㹵���� result

            if (result == 0) //��Ǩ�ͺ��Ҥ�Ҽ��Ѿ���� 0 ������� ������¶֧����������������
            {
                Console.WriteLine("Connected successfully!");

                return true;
            }
            else //㹡óդ�Ҽ��Ѿ������� 0 �ʴ��������������������
            {
                ErrorCode = company.GetLastErrorCode(); //�Ѻ���ʢ�ͼԴ��Ҵ����ش�ҡ�ͺ�硵� company ��ѧ�ҡ������¡���ʹ Connect()
                ErrorMessage = company.GetLastErrorDescription(); //�Ѻ��ͤ��������¢�ͼԴ��Ҵ����ش�ҡ�ͺ�硵� company ��ѧ�ҡ���¡���ʹ Connect()

                Console.WriteLine("Connected unsuccessfully!");

                return false;
            }
        }
    }
}