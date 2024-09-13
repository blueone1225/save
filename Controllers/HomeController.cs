using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyModel_CodeFirst.Models;
using System.Diagnostics;

namespace MyModel_CodeFirst.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GuestBookContext _context;

        public HomeController(ILogger<HomeController> logger, GuestBookContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //select top 10 * from Book where photo is null order by TimeStamp desc, GId desc
            var result = _context.Book.Where(b => b.Photo != null).OrderByDescending(b => b.TimeStamp).ThenByDescending(b => b.GId).Take(10);


            return View(await result.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}




//MyModel_CodeFirst�M�׶i��B�J

//1. �ϥ�Code First�إ�Model�θ�Ʈw

//1.1   �bModels��Ƨ��̫إ�Book��ReBook������O�����ҫ�
//1.1.1 �bModels��Ƨ��W���k����[�J�����O�A�ɦW���W��Book.cs�A���U�u�s�W�v�s
//1.1.2 �]�pBook���O���U�ݩʡA�]�A�W�١B��������Ψ���������ҳW�h����ܦW��(Display)
//1.1.3 �bModels��Ƨ��W���k����[�J�����O�A�ɦW���W��ReBook.cs�A���U�u�s�W�v�s
//1.1.4 �]�pReBook���O���U�ݩʡA�]�A�W�١B��������Ψ���������ҳW�h����ܦW��(Display)
//1.1.5 ���g������O�������p�ݩʰ������Ӹ�ƪ��������p


//1.2   �إ�DbContext���O
//      ���w�ˤU�C��ӮM��
//      (1)Microsoft.EntityFrameworkCore.SqlServer
//      (2)Microsoft.EntityFrameworkCore.Tools
//      ���PDB First�w�˪��M��@�ˡ�
//1.2.1 �bModels��Ƨ��W���k����[�J�����O�A�ɦW���W��GuestBookContext.cs�A���U�u�s�W�v�s
//1.2.2 ���gGuestBookContext���O�����e
//      (1)���~��DbContext���O
//      (2)���g�غc�l
//      (3)�y�z��Ʈw�̭�����ƪ�
//1.2.3 �bappsettings.json�����g��Ʈw�s�u�r��
//1.2.4 �bProgram.cs���H�̿�`�J���g�k���gŪ���s�u�r�ꪺ����
//      ���`�N�{������m�����n�bvar builder = WebApplication.CreateBuilder(args);�o�y����
//1.2.5 �b�M��޲z���D���x(�˵� > ��L���� > �M��޲z���D���x)�U���O
//      (1)Add-Migration InitialCreate
//      (2)Update-database
//      ����(1)�����uInitialCreate���O�ۭq���W�١A�Y���榨�\�|�ݨ�uBuild succeeded.�v��
//      ���t�~�|�ݨ�@��Migrations����Ƨ��Ψ��ɮ׳Q�إߦb�M�פ��A�̭�������Migration�����{��
//      ���Y��(1)�����O���榨�\�~������(2)�����O��
//      (3)��SSMS���d�ݬO�_�����\�إ߸�Ʈw�θ�ƪ�(�ثe��ƪ��S�����)



//1.3   �Ы�Initializer����إߪ�l(�ؤl)���(Seed Data)
//      �������ڭ̥i�H�b�Ыظ�Ʈw�ɴN�ЫشX����l����Ʀb�̭��H�Ѷ}�o�ɴ��դ��Ρ�����
//      �������Х��N��Ʈw�R���A�ñN�M�פ�Migrations��Ƨ��Τ��t�ɮ׾�ӧR��������

//1.3.1 �bModels��Ƨ��W���k����[�J�����O�A�ɦW���W��SeedData.cs�A���U�u�s�W�v�s
//1.3.2 ���gSeedData���O�����e
//      (1)���g�R�A��k Initialize(IServiceProvider serviceProvider)
//      (2)���gBook��ReBook��ƪ�����l��Ƶ{��
//      (3)���ggetFileBytes�A�\�ର�N�Ӥ��ন�G�i����
//1.3.3 �bProgram.cs���g�ҥ�Initializer���{��(�n�g�bvar app = builder.Build();����)
//      ���o��Initializer���@�άO�إߤ@�Ǫ�l��Ʀb��Ʈw���H�Q���աA�ҥH���@�w�n��Initializer��
//      ���`�N:��l��ƪ��Ӥ���bSeedSourcePhoto��Ƨ�����
//1.3.4 �ظm�M�סA�T�w�M�ק����ظm���\
//1.3.5 �A����M��޲z���D���x(�˵� > ��L���� > �M��޲z���D���x)�U���O
//      (1)Add-Migration InitialCreate
//      (2)Update-database
//1.3.4 ��SSMS���d�ݬO�_�����\�إ߸�Ʈw�θ�ƪ�(�ثe��ƪ��S�����)
//1.3.5 �b�s�����W������������H�إߪ�l���(�Y�S������L�����A��l��Ƥ��|�Q�إ�)
//1.3.6 �A����SSMS���d�ݸ�ƪ��O�_�����


//2. �إ߯d���O��x�޲z�\��

//2.1   �s�@�۰ʥͦ���Book��ƪ�CRUD
//2.1.1 �bControllers��Ƨ��W���k����[�J�����
//2.1.2 ��ܡu�ϥ�EntityFramework�����˵���MVC����v�����U�u�[�J�v�s
//2.1.3 �b��ܤ�����]�w�p�U
//      �ҫ����O: Book(MyModel_CodeFirst.Models)
//      ��Ƥ��e���O: GuestBookContext(MyModel_CodeFirst.Models)
//      �Ŀ� �����˵�
//      �Ŀ� �Ѧҫ��O�X�{���w
//      �Ŀ� �ϥΪ����t�m��
//      ����W�٨ϥιw�]�Y�i(BooksController)
//      ���U�u�s�W�v�s
//2.1.4 ����/Books/Index �i�����
//2.1.5 �ק�Index View�NPhoto��ImageType���BCreate�BEdit��Details�W�쵲����
//2.1.6 �̳ߦn�ۦ�ק虜��


//2.2   �վ�BooksController���e 
//2.2.1 ��gIndex Action�����e�A�N�d���̷s���±Ƨ�
//2.2.2 ����Details Action (��i�@�֧R�� Details.cshtml)
//2.2.3 ����Create Action (��i�@�֧R�� Create.cshtml)
//2.2.4 ����Edit Action (��i�@�֧R�� Edit.cshtml)


//2.3   �ק�Delete View���ƪ��覡
//2.3.1 �Ӧۤv�ߦn�ק�ƪ��覡(�o�̧ڭ̨ϥ�Bootstrap Card����)
//2.3.2 �bBooksController���W�[Ū���Ӥ�����k
//2.3.3 �bDelete View�[�J���o�Ӥ���HTML
//2.3.4 ����


//2.4   �ϥΡuViewComponent�v�ޥ���@�u�N�^�Яd�����e��ܩ�Delete View�v
//   �����椸�N�n����ViewComponent���ϥΤ覡��
//2.4.1 �b�M�פ��s�WViewComponents��Ƨ�(�M�פW���k����[�J���s�W��Ƨ�)�H��m�Ҧ���ViewComponent������
//2.4.2 �bViewComponents��Ƨ����إ�VCReBooks ViewComponent(�k����[�J�����O����J�ɦW���s�W)
//2.4.3 VCReBooks class�~��ViewComponent(�`�Nusing Microsoft.AspNetCore.Mvc;)
//2.4.4 ���gInvokeAsync()��k���o�^�Яd�����
//2.4.5 �b/Views/Shared�̫إ�Components��Ƨ��A�æbComponents��Ƨ����إ�VCReBooks��Ƨ�
//2.4.6 �b/Views/Shared/Components/VCReBooks�̫إ��˵�(�k����[�J���˵�����ܡuRazor�˵��v�����U�u�[�J�v�s)
//2.4.7 �b��ܤ�����]�w�p�U
//      �˵��W��: Default
//      �d��:Empty(�S���ҫ�)
//      ���Ŀ� �إߦ������˵�
//      ���Ŀ� �ϥΪ����t�m��
//   ���`�N�G��Ƨ���View���W�٤��O�ۭq���A�ӬO���w�]���W�١A�W�w�p�U�G��
//   /Views/Shared/Components/{ComponentName}/Default.cshtml
//   /Views/{ControllerName}/Components/{ComponentName}/Default.cshtml
//2.4.8 �bDefault View�W��[�J@model IEnumerable<MyModel_CodeFirst.Models.ReBook>
//2.4.9 �̳ߦn�s��Default View�ƪ��覡
//2.4.10 �s�gDelete View�A�[�JVCReBooks ViewComponent
//2.4.11 ����



//2.5   �s�@�d���R���\��
//2.5.1 �bDelete View�����R���s�W�[�J�T�{��ܤ��
//   ���`�N�I��Ӹ�ƪ����p�O�s�ʪ��A�D�d���Q�R����A�|�@�֧R���Ҧ��^�Х����d���A�H�ŦX�Ѧҧ���ʡ�
//2.5.2 �bBooksController���W�[�R���^�Яd��Action
//2.5.3 �b�bVCReBook ViewComponent��View��(Default.cshtml)�إߨC�h�^�Яd�����R���s
//2.5.4 ����


//2.7   Layout���B�z
//2.7.1 ��檺����
//2.7.2 �D�e��(����)���t�m
//2.7.3 �bShared��Ƨ����إߤ@�ӦW��_Layout2.cshtml�� View�����e�x�ϥΪ�Layout�ALayout����ۭq
//2.7.4 �N_ViewStart.cshtml�̹w�]��Layout�אּLayout2
//      ���o�ˤ@�өҦ���View�N�|�M��_Layout2.cshtml��
//      ���ɥR�G_ViewStart.cshtml�����w�w�]Layout�Ϊ��ɮס�



//3.  �s�@�d���O�e�x

//3.1   �s�@�۰ʥͦ���Book��ƪ�CRUD
//3.1.1 �bControllers��Ƨ��W���k����[�J�����
//3.1.2 ��ܡu�ϥ�EntityFramework�����˵���MVC����v�����U�u�[�J�v�s
//3.1.3 �b��ܤ�����]�w�p�U
//      �ҫ����O: Book(MyModel_CodeFirst.Models)
//      ��Ƥ��e���O: GuestBookContext(MyModel_CodeFirst.Models)
//      �Ŀ� �����˵�
//      �Ŀ� �Ѧҫ��O�X�{���w
//      �Ŀ� �ϥΪ����t�m��
//      ����W�٧אּPostBooksController
//      ���U�u�s�W�v�s
//3.1.4 �ק�PostBooksController�A����Edit�BDelete Action
//3.1.5 �R��Edit�BDelete View�ɮ�
//3.1.6 �ק�Index Action���g�k


//3.2   ��ܥ\��
//3.2.1 �ק�A�X�e�x�e�{��Index View
//3.2.2 �NPostBooksController��Details Action��W��Display(View�]�n��W�r)
//3.2.3 �ק�Index View��Details���W�쵲��Display
//3.2.4 �ק�Display View �ƪ��˦��A�ƪ��i�H�ӤH�ߦn�e�{
//      ���o�̨C�@���^�Яd���W���|���R�����\��s�A�o�ӱ��p�ä��X�z�A�b�᭱�|�B�z��
//      ���ƪ��i�H�ӤH�ߦn�e�{��


//3.3   �d���\��
//3.3.1 �ק�Create View�A�ק�W���ɮת�����
//3.3.2 �ק�Create View�A�N<form>�W�[ enctype="multipart/form-data" �ݩ�
//3.3.3 �[�J�e�ݮĪG�A�ϷӤ��i���w��
//3.3.4 �R��ImageType���
//3.3.5 �R��TimeStamp���
//3.3.6 �ק�Post Create Action�A�[�W�B�z�W�ǷӤ����\��
//3.3.7 ���կd���\��
//3.3.8 �bIndex View���[�J���W�ǷӤ����d������ܤ覡
//3.3.9 �bDisplay View���[�J���W�ǷӤ����d������ܤ覡
//3.3.10 �bIndex View���[�J�B�z�u�����檺�d���v��ܤ覡
//3.3.11 �bDisplay View���[�J�B�z�u�����檺�d���v��ܤ覡
//3.3.12 �N��x����Index�PDelete View �]�ɤW�����檺�d����ܤ覡�Υ��W�ǷӤ����d������ܤ覡



//3.4   �򥻪����~�B�z(Error Handle)
//3.4.1 ����:�G�N���{���o�ͨҥ~�ݬݵ��G
//3.4.2 �bPostBooksController�̼g�@�ӷ|�o�ͨҥ~��Action�p�U
//public IActionResult ExceptionTest()
//{
//    int a = 0;
//    int s = 100 / a;
//    return View();
//}
//3.4.3 �Q�έ쥻�w�]��Home Controller Error Handler�ӳB�z�ҥ~
//3.4.4 �ק� Program.cs�����{���X�A�N�O�_�b�}�o�Ҧ��U�����~�B�z�P�_���ѱ�
//3.4.5 ����:�A���G�N���{���o�ͨҥ~�ݬݵ��G
//3.4.6 �ק�Error.cshtml���e
//3.4.7 ����:�G�N���{���o��HttpNotFound(404)���~
//3.4.8 �bProgram.cs�����{���X���U�B�zHttpNotFound(404)���~��Error Handler
//3.4.7 ����:�A���G�N���{���o��HttpNotFound(404)���~
//      ���̰򥻪����~�B�z�A�N�O�����ϥΪ̬ݨ�t�ο��~�T����



//3.5   �^�Яd���\��
//3.5.1 �bControllers��Ƨ��W���k����[�J�����
//3.5.2 ��ܡu�ϥ�EntityFramework�����˵���MVC����v�����U�u�[�J�v�s
//3.5.3 �b��ܤ�����]�w�p�U
//      �ҫ����O: ReBook(MyModel_CodeFirst.Models)
//      ��Ƥ��e���O: GuestBookContext(MyModel_CodeFirst.Models)
//      �Ŀ� �����˵�
//      �Ŀ� �Ѧҫ��O�X�{���w
//      ���Ŀ� �ϥΪ����t�m��
//      ����W�٧אּRePostBooksController
//      ���U�u�s�W�v�s
//3.5.4 �ק�RePostBooksController�A�ȫO�dCreate Action�A�䥦�����R��
//3.5.5 �ȫO�dCreate View�ɮסA�䥦�����R��
//3.5.6 �ק� Create View
//      ���s�@�e��ݤ������^�Яd���\�ࡰ
//3.5.7 �bPostBooks\Display View���NRePostBooks\Create View�HAjax�覡Ū�J
//3.5.8 �t�XBoostrap Modal Component��ܥX�e��
//3.5.9 �ק�RePostBooksController����Create Action�A�Ϩ�Return JSON���
//3.5.10 �NRePostBooks\Create View��<form>�[�Wid
//3.5.11 �bPostBooks\Display View�����g������JavaScript�{���A�HAjax�覡����s�W�^�Яd��
//3.5.12 ���ծĪG


//3.6    �s�W�^�Яd����e�ݪ��B�z
//3.6.1  �N Modal�������M�Ũõ���Modal���
//3.6.2  ���sŪ���^�Яd����ViewComComponent,�Ϩ����̷ܳs���^�Яd��
//3.6.3  �bRePostBooksController���gGetRebookByViewComponent Action
//3.6.4  �HAjax�I�sGetRebookByViewComComponent Action
//3.6.5  ���ծĪG


//3.7    Bootstrap����
//       �Q��Bootstrap�̪��\��@����


//4.  ²���n�J�\��s�@

//4.1   �b��Ʈw�s�W�@��Login��ƪ�s��޲z�̱b���K�X
//4.1.1 �bModels��Ƨ��̫إ�Login���O�����ҫ�
//4.1.2 Models��Ƨ��W���k����[�J�����O�A�ɦW���W��Login.cs�A���U�u�s�W�v�s
//4.1.3 �]�pLogin���O���U�ݩʡA�]�A�W�١B��������Ψ���������ҳW�h����ܦW��(DisplayName)
//4.1.4 �ק�GuestBookContext���O�����e�A�[�J�y�z��Ʈw��Login����ƪ�
//4.1.5 �b�M��޲z���D���x(�˵� > ��L���� > �M��޲z���D���x)�U���O
//      (1)Add-Migration AddLoginTable
//      (2)Update-database
//4.1.6 ��SSMS���d�ݬO�_�����\�إ�Login��ƪ�(�ثe��ƪ��S�����)
//4.1.7 �bLogin��ƪ��إߤ@���b���K�X�����(admin, 12345678)



//4.2   �s�@Login�\��P�e��
//4.2.1 �bControllers��Ƨ��W���k����[�J�����
//4.2.2 ��ܡuMVC���-�ťաv�����U�u�[�J�v�s
//4.2.3 �ɦW���W���uLoginController�v�����U�u�s�W�v�s
//4.2.4 �إ�Get�PPost��Login Action
//4.2.5 �إ�Login View(Login Action�����k����s�W�˵���Razor�˵������U�u�[�J�v�s)
//      �b��ܤ�����]�w�p�U
//      �˵���: Login (�ϥιw�]�W��)
//      �d��: Create
//      �ҫ����O: Login(MyModel_CodeFirst.Models)
//      ��Ƥ��e���O: GuestBookContext(MyModel_CodeFirst.Models)
//      ���Ŀ� �إߦ������˵�
//      �Ŀ� �Ѧҫ��O�X�{���w
//      �Ŀ� �ϥΪ����t�m��
//4.2.6 �NViewData["Error"]�[�JLogin View
//4.2.7 �bProgram.cs�����U�αҥ�Session
//4.2.8 �bVCReBooks View Component���[�J���n�J�ݤ���R���s���P�_��
//4.2.9 ����


//4.3   �إ߶i�J��x�����O�n�J���A��@
//4.3.1 �b_Layout2.cshtml���[�J�n�J��x�����s
//4.3.2 �N/Views/Books�̩Ҧ�View��Layout���w��_Layout.cshtml(��x�Ϊ�Layout)
//4.3.3 �b_Layout.cshtml�[�J���n�J�h�N�����۰ʾɩ�/Home/Index���P�_��



//4.4   �n�X�\��PLayout�s��
//4.4.1 �bLogin Controller�[�JLogout Action
//4.4.2 �b_Layout.cshtml�[�J�n�X��x�����s
//      ���e��x��Layout�i�̷Ӧۤv���ߦn���e�{��