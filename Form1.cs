using SAPbobsCOM; //เพิ่ม namespace ของ SAP DI API

namespace DemoInterfaceSAPB1
{
    public partial class Form1 : Form
    {
        private int ErrorCode; //ประกาศตัวแปร ErrorCode สำหรับเก็บค่า Error Code จาก SAP Business One
        private string ErrorMessage; //ประกาศตัวแปร ErrorMessage สำหรับเก็บค่า Error Description จาก SAP Business One
        private Company company; //ประกาศตัวแปร company ของประเภท Company

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_TestLogin_Click(object sender, EventArgs e)
        {
            bool result = SAPConnect(); //สร้างตัวแปร result และกำหนดค่าให้เท่ากับผลลัพธ์จากการเรียกเมธอด SAPConnect() ซึ่งจะคืนค่า true ถ้าเชื่อมต่อสำเร็จ และ false ถ้าเชื่อมต่อไม่สำเร็จ

            if (result) //ตรวจสอบว่าเชื่อมต่อสำเร็จหรือไม่
            {
                MessageBox.Show("Connected successfully!"); //หากเชื่อมต่อสำเร็จให้แสดง 'Connected successfully!" ใน Message Box
            }
            else
            {
                string error = string.Format(@"Coonected unsuccessfully!, Error: {0} - {1}", ErrorCode, ErrorMessage); //เก็บข้อความ error

                MessageBox.Show(error); //แสดงข้อความ Error กรณีเชื่อมต่อไม่สำเร็จ
            }
        }

        private bool SAPConnect()
        {
            company = new Company();

            //กำหนดค่าพารามิเตอร์ให้กับ Instance company
            company.Server = "MY-NOTEBOOK\\MSSQLSERVER2019"; //กำหนดชื่อ Server โดยจะเป็นที่เก็บ Database ของ Company สามารถดูได้จากหน้า Login ของ SAP B1
            company.CompanyDB = "SBODemoAU"; //กำหนดชื่อฐานข้อมูลของ Company สามารถดูได้จากหน้า Login ของ SAP B1 ที่คอลัมน์ 'Database Name'
            company.UserName = "manager"; //กำหนดชื่อผู้ใช้ SAP B1
            company.Password = "1234"; //กำหนดรหัสผ่าน SAP B1
            company.DbServerType = BoDataServerTypes.dst_MSSQL2019; //กำหนดประเภทของฐานข้อมูล

            int result = company.Connect(); //ทำการเชื่อมต่อและเก็บค่าผลลัพธ์ในตัวแปร result

            if (result == 0) //ตรวจสอบว่าค่าผลลัพธ์เป็น 0 หรือไม่ ซึ่งหมายถึงการเชื่อมต่อสำเร็จ
            {
                Console.WriteLine("Connected successfully!");

                return true;
            }
            else //ในกรณีค่าผลลัพธ์ไม่เป็น 0 แสดงว่าเชื่อมต่อไม่สำเร็จ
            {
                ErrorCode = company.GetLastErrorCode(); //รับรหัสข้อผิดพลาดล่าสุดจากออบเจ็กต์ company หลังจากการเรียกเมธอด Connect()
                ErrorMessage = company.GetLastErrorDescription(); //รับข้อความบรรยายข้อผิดพลาดล่าสุดจากออบเจ็กต์ company หลังจากเรียกเมธอด Connect()

                Console.WriteLine("Connected unsuccessfully!");

                return false;
            }
        }
    }
}
