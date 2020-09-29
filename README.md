# Web-Notes
## 第一周
### MVC基础编程

1. 新建一个MVC项目
   1. 选择ASP.NET Web应用程序(.NET Framework)
   2. 设置项目名称和存储位置
   3. 选择MVC模式，可以添加身份验证（一般选择个人用户账户）
   4. 初次运行，需要信任IIS Express SSL，然后不安装证书
2. MVC项目目录结构
   1. 引用：支持应用系统运行所需的类库（如.dll文件）
   2. App_Data：存放私有数据，如数据库文件。
   3. App_Start：存放项目的核心配置，如路由配置等。
   4. Content：存放CSS、图像等内容。
   5. Controllers：存放Controller类，处理URL请求。
   6. Models：存放与业务相关的实体类数据模型。
   7. Scripts：存放JavaScript类库文件和其他.js文件。
   8. Views：存放视图文件（按控制器名称分组）。默认首页视图：Views/Home/Index.cshtml，由Controllers/HomeController.cs控制。
   9. Views/Shared：主要存放布局文件(如_Layout.cshtml)、自定义视图或公共视图。
   10. Views/_ViewStart.cshtml：通常用于指定视图默认的布局页。
   11. Global.asax：提供全局可用代码。
   12. Web.config：应用系统的全局配置文件。（注意Views下也可有局部配置文件）
3.  MVC模式概念：
    1.  模型Model：表示应用数据的类
    2.  控制器Controller：处理浏览器的传入请求，检索模型数据，将响应返回给浏览器
    3.  视图View：显示结果的用户界面
4.  添加控制器
    1.  右键Controllers文件夹
    2.  选择添加控制器
    3.  选择 MVC5控制器-空
    4.  命名为xxxController（后缀为Controller）
5.  控制器代码
    1.  默认代码  
    ```cs
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
    }
    ```